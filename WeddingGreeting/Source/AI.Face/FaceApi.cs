using Baidu.AI.Face.Models;
using ee.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Baidu.AI.Face
{
    public static class FaceApi
    {
        static private Baidu.Aip.Face.Face client = new Baidu.Aip.Face.Face(Configuration.API_KEY, Configuration.SECRET_KEY);

        private static string ImageType = "BASE64";

        public static BaseResponse AddGroup(string groupId)
        {
            return Execute(client.GroupAdd(groupId));
        }

        public static BaseResponse CopyUser(string groupId, CopyUserOption option = null)
        {
            return Execute(client.UserCopy(groupId, option?.Options));
        }

        public static BaseResponse DeleteGroup(string groupId)
        {
            return Execute(client.GroupDelete(groupId));
        }

        public static BaseResponse DeleteUser(string userId, string groupId)
        {
            return Execute(client.UserDelete(groupId, userId));
        }

        public static BaseResponse FaceDelete(string userId, string groupId)
        {
            return Execute(client.UserDelete(groupId, userId));
        }

        /// <summary>
        /// 人脸检测
        /// 检测请求图片中的人脸，返回人脸位置、72个关键点坐标、及人脸相关属性信息。检测响应速度，与图片中人脸数量相关，人脸数量较多时响应时间会有些许延长。
        /// </summary>
        /// <param name="ImageFilePath">图片文件路径</param>
        /// <param name="FaceDetectOptions">默认只返回人脸框、概率和旋转角度。</param>
        /// <param name="max_face_num">检测人脸数量，默认为1</param>
        /// <returns>
        /// 字段	是否必选	类型	说明
        /// log_id	number	是	日志id
        /// result_num number  是 人脸数目
        ///result array   是 人脸属性对象的集合
        ///+age number  否 年龄。face_fields包含age时返回
        ///+beauty number  否 美丑打分，范围0-100，越大表示越美。face_fields包含beauty时返回
        ///+location object 是   人脸在图片中的位置
        ///++left number  是 人脸区域离左边界的距离
        ///++top number  是 人脸区域离上边界的距离
        ///++width number  是 人脸区域的宽度
        ///++height number  是 人脸区域的高度
        ///+face_probability number  是 人脸置信度，范围0-1
        ///+rotation_angle number  是 人脸框相对于竖直方向的顺时针旋转角，[-180,180]
        ///+yaw number  是 三维旋转之左右旋转角[-90(左), 90(右)]
        ///+pitch number  是 三维旋转之俯仰角度[-90(上), 90(下)]
        ///+roll number  是 平面内旋转角[-180(逆时针), 180(顺时针)]
        ///+expression number  否 表情，0，不笑；1，微笑；2，大笑。face_fields包含expression时返回
        ///+expression_probability number  否 表情置信度，范围0 ~1。face_fields包含expression时返回
        ///+faceshape array   否 脸型置信度。face_fields包含faceshape时返回
        ///++type string 是   脸型：square/triangle/oval/heart/round
        ///++probability number  是 置信度：0~1
        ///+gender string 否   male、female。face_fields包含gender时返回
        ///+gender_probability number  否 性别置信度，范围0 ~1。face_fields包含gender时返回
        ///+glasses number  否 是否带眼镜，0-无眼镜，1-普通眼镜，2-墨镜。face_fields包含glasses时返回
        ///+glasses_probability number  否 眼镜置信度，范围0 ~1。face_fields包含glasses时返回
        ///+landmark array   否	4个关键点位置，左眼中心、右眼中心、鼻尖、嘴中心。face_fields包含landmark时返回
        ///++x number  否 x坐标
        ///++y number  否 y坐标
        ///+landmark72 array   否	72个特征点位置，示例图 。face_fields包含landmark时返回
        ///++x number  否 x坐标
        ///++y number  否 y坐标
        ///+race string 否   yellow、white、black、arabs。face_fields包含race时返回
        ///+race_probability number  否 人种置信度，范围0 ~1。face_fields包含race时返回
        ///+qualities object 否   人脸质量信息。face_fields包含qualities时返回
        ///++occlusion object 是   人脸各部分遮挡的概率,[0, 1],0表示完整，1表示不完整
        ///+++left_eye number  是 左眼
        ///+++right_eye number  是 右眼
        ///+++nose number  是 鼻子
        ///+++mouth number  是 嘴
        ///+++left_cheek number  是 左脸颊
        ///+++right_cheek number  是 右脸颊
        ///+++chin number  是 下巴
        ///++blur number  是 人脸模糊程度，[0, 1]。0表示清晰，1表示模糊
        ///++illumination number  是 取值范围在[0, 255], 表示脸部区域的光照程度
        ///++completeness number  是 人脸完整度，0或1, 0为人脸溢出图像边界，1为人脸都在图像边界内
        ///++type object 是   真实人脸/卡通人脸置信度
        ///+++human number  是 真实人脸置信度，[0, 1]
        ///+++cartoon number  是 卡通人脸置信度，[0, 1]
        ///
        ///
        ///可通过人脸检测接口，基于以下字段和对应阈值，进行质量检测的判断，以保证人脸质量符合后续业务操作要求。
        ///
        ///遮挡范围
        ///occlusion（0~1），0为无遮挡，1是完全遮挡含有多个具体子字段，表示脸部多个部位通常用作判断头发、墨镜、口罩等遮挡
        ///left_eye : 0.6, #左眼被遮挡的阈值
        ///right_eye : 0.6, #右眼被遮挡的阈值
        ///nose : 0.7, #鼻子被遮挡的阈值
        ///mouth : 0.7, #嘴巴被遮挡的阈值
        ///left_check : 0.8, #左脸颊被遮挡的阈值
        ///right_check : 0.8, #右脸颊被遮挡的阈值
        ///chin_contour : 0.6, #下巴被遮挡阈值
        ///
        ///模糊度范围
        ///Blur（0~1），0是最清晰，1是最模糊
        ///小于0.7
        ///
        ///光照范围
        ///illumination（0~255）脸部光照的灰度值，0表示光照不好以及对应客户端SDK中，YUV的Y分量
        ///大于40
        ///
        ///姿态角度
        ///Pitch：三维旋转之俯仰角度[-90(上), 90(下)]
        ///Roll：平面内旋转角[-180(逆时针), 180(顺时针)]
        ///Yaw：三维旋转之左右旋转角[-90(左), 90(右)]
        ///分别小于20度
        ///
        ///人脸完整度
        ///completeness（0~1），0代表完整，1代表不完整
        ///小于0.4
        ///
        ///人脸大小
        ///人脸部分的大小 建议长宽像素值范围：80*80~200*200
        ///人脸部分不小于100*100像素
        /// </returns>
        public static BaseResponse FaceDetect(byte[] imageByte, List<FaceDetectOptions> FaceDetectOptions, int max_face_num = 1)
        {
            if (imageByte.Length > 1024 * 1024 * 10)
                throw new Exception("图片大小必须小于10Mb");

            var imageBase64 = Convert.ToBase64String(imageByte);
            string face_fields_options = "";
            foreach (var item in FaceDetectOptions)
            {
                face_fields_options += "," + item.ToString();
            }

            if (face_fields_options.Length != 0)
                face_fields_options = face_fields_options.Remove(0, 1);

            var options = new Dictionary<string, object>()
            {
                {"face_fields", face_fields_options},
                {"max_face_num",max_face_num }
            };
            return Execute(client.Detect(imageBase64, ImageType, options));
        }

        public static BaseResponse FaceDetect(Bitmap image, List<FaceDetectOptions> FaceDetectOptions, int max_face_num = 1)
        {
            return FaceDetect(image.ToBytes(), FaceDetectOptions, max_face_num);
        }

        public static BaseResponse<FaceRegisterResult> FaceRegister(byte[] imageByte, string groupId, string userId, FaceOption option = null)
        {
            if (imageByte.Length > 1024 * 1024 * 10)
                throw new Exception("图片大小必须小于10Mb");

            var imageBase64 = Convert.ToBase64String(imageByte);
            return Execute<FaceRegisterResult>(client.UserAdd(imageBase64, ImageType, groupId, userId, option?.Options));
        }

        public static BaseResponse<FaceRegisterResult> FaceRegister(Bitmap image, string groupId, string userId, FaceOption option = null)
        {
            var imageByte = image.ToBytes();
            return FaceRegister(imageByte, groupId, userId, option);
        }

        public static BaseResponse<FaceRegisterResult> FaceSaveOrUpdate(Bitmap image, string groupId, string userId, FaceOption option = null)
        {
            var user = GetUserInfo(userId, groupId);
            if (user != null && user.error_code == 0 && (user.result?.user_list?.Any() ?? false))
            {
                return FaceUpdate(image, groupId, userId, option);
            }
            else
            {
                return FaceRegister(image, groupId, userId, option);
            }
        }

        public static BaseResponse<FaceSearchResult> FaceSearch(byte[] imageByte, string[] groupIds, SearchFaceOption option = null)
        {
            if (imageByte.Length > 1024 * 1024 * 10)
                throw new Exception("图片大小必须小于10Mb");
            var imageBase64 = Convert.ToBase64String(imageByte);
            return Execute<FaceSearchResult>(client.Search(imageBase64, ImageType, string.Join(",", groupIds), option?.Options));
        }

        public static BaseResponse<FaceSearchResult> FaceSearch(Bitmap image, string[] groupIds, SearchFaceOption option = null)
        {
            var imageByte = image.ToBytes();
            return FaceSearch(imageByte, groupIds, option);
        }

        public static BaseResponse<FaceRegisterResult> FaceUpdate(byte[] imageByte, string groupId, string userId, FaceOption option = null)
        {
            if (imageByte.Length > 1024 * 1024 * 10)
                throw new Exception("图片大小必须小于10Mb");
            var imageBase64 = Convert.ToBase64String(imageByte);
            return Execute<FaceRegisterResult>(client.UserUpdate(imageBase64, ImageType, groupId, userId, option?.Options));
        }

        public static BaseResponse<FaceRegisterResult> FaceUpdate(Bitmap image, string groupId, string userId, FaceOption option = null)
        {
            return FaceUpdate(image.ToBytes(), groupId, userId, option);
        }

        /// <summary>
        /// 获取一个用户的全部人脸列表。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static BaseResponse<GetUserResult> GetFacesByUser(string userId, string groupId)
        {
            return Execute<GetUserResult>(client.FaceGetlist(userId, groupId));
        }

        public static BaseResponse<GetGroupsResult> GetGroups(GetActionOption option = null)
        {
            return Execute<GetGroupsResult>(client.GroupGetlist(option?.Options));
        }

        /// <summary>
        /// 用户信息查询
        ///
        /// 用于查询人脸库中某用户的详细信息。
        /// </summary>
        /// <param name="uid">用户id（由数字、字母、下划线组成），长度限制128B</param>
        /// <returns>
        /// 字段	是否必选	类型	说明
        /// log_id	是	number	请求标识码，随机数，唯一
        ///result 是   array 结果数组
        ///+uid 是   string 匹配到的用户id
        ///+user_info 是   string 注册时的用户信息
        ///+groups 是   array 用户所属组列表
        /// </returns>
        public static BaseResponse<GetUserResult> GetUserInfo(string userId, string groupId)
        {
            return Execute<GetUserResult>(client.UserGet(userId, groupId));
        }

        /// <summary>
        /// 查询指定用户组中的用户列表。
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static BaseResponse<GetUsersResult> GetUsers(string groupId, GetActionOption option = null)
        {
            return Execute<GetUsersResult>(client.GroupGetusers(groupId, option?.Options));
        }

        private static BaseResponse<T> Execute<T>(JObject result)
        {
            string json = JsonConvert.SerializeObject(result);
            return JsonConvert.DeserializeObject<BaseResponse<T>>(json);
        }

        private static BaseResponse Execute(JObject result)
        {
            return Execute<object>(result) as BaseResponse;
        }
    }
}