using ee.Utilities;

namespace Baidu.AI.Face.Models
{
    /// <summary>
    /// 人脸检测 参数可枚举类型
    /// </summary>
    public enum FaceDetectOptions
    {
        [EnumDescription("年龄")]
        age,

        [EnumDescription("颜值")]
        beauty,

        [EnumDescription("表情")]
        expression,

        [EnumDescription("脸型")]
        faceshape,

        [EnumDescription("性别")]
        gender,

        [EnumDescription("是否眼镜")]
        glasses,

        [EnumDescription("眼鼻耳坐标")]
        landmark,

        [EnumDescription("人种")]
        race,

        [EnumDescription("人脸质量")]
        qualities
    }

    public enum FaceType
    {
        /// <summary>
        /// 表示生活照：通常为手机、相机拍摄的人像图片、或从网络获取的人像图片等
        /// </summary>
        LIVE,

        /// <summary>
        /// 表示身份证芯片照：二代身份证内置芯片中的人像照片
        /// </summary>
        IDCARD,

        /// <summary>
        /// 表示带水印证件照：一般为带水印的小图，如公安网小图
        /// </summary>
        WATERMARK,

        /// <summary>
        /// 表示证件照片：如拍摄的身份证、工卡、护照、学生证等证件图片
        /// </summary>
        CERT
    }

    /// <summary>
    /// 图片质量控制
    /// </summary>
    public enum QualityControlType
    {
        /// <summary>
        /// 不进行控制
        /// </summary>
        NONE,

        /// <summary>
        /// 较低的质量要求
        /// </summary>
        LOW,

        /// <summary>
        /// 一般的质量要求
        /// </summary>
        NORMAL,

        /// <summary>
        /// 较高的质量要求
        /// </summary>
        HIGH,
    }

    /// <summary>
    /// 活体检测控制
    /// </summary>
    public enum LivenessControlType
    {
        /// <summary>
        ///  不进行控制
        /// </summary>
        NONE,

        /// <summary>
        /// 较低的活体要求(高通过率 低攻击拒绝率)
        /// </summary>
        LOW,

        /// <summary>
        /// 一般的活体要求(平衡的攻击拒绝率, 通过率)
        /// </summary>
        NORMAL,

        /// <summary>
        ///  较高的活体要求(高攻击拒绝率 低通过率)
        /// </summary>
        HIGH,
    }
}