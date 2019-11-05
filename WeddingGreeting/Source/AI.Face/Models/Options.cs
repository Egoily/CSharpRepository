using System;
using System.Collections.Generic;

namespace Baidu.AI.Face.Models
{
    public abstract class BaseOption
    {
        public abstract Dictionary<string, object> Options { get; }
    }

    public class DetectFaceOption : BaseOption
    {
        public override Dictionary<string, object> Options => throw new NotImplementedException();
    }

    public class SearchFaceOption : BaseOption
    {
        /// <summary>
        /// 图片质量控制
        /// </summary>
        public virtual QualityControlType Quality_Control { get; set; }

        /// <summary>
        /// 活体检测控制
        /// </summary>
        public virtual LivenessControlType Liveness_Control { get; set; }

        public virtual string User_Id { get; set; }
        public virtual int? Max_User_Num { get; set; }

        public override Dictionary<string, object> Options
        {
            get
            {
                Dictionary<string, object> options = new Dictionary<string, object>();

                options.Add("quality_control", Quality_Control.ToString());

                options.Add("liveness_control", Liveness_Control.ToString());

                if (!string.IsNullOrEmpty(User_Id))
                {
                    options.Add("user_id", User_Id);
                }
                if (Max_User_Num.HasValue)
                {
                    options.Add("max_user_num", Max_User_Num);
                }
                return options;
            }
        }
    }

    public class FaceOption : BaseOption
    {
        /// <summary>
        /// 图片质量控制
        /// </summary>
        public virtual QualityControlType Quality_Control { get; set; }

        /// <summary>
        /// 活体检测控制
        /// </summary>
        public virtual LivenessControlType Liveness_Control { get; set; }

        public virtual string User_Info { get; set; }

        public override Dictionary<string, object> Options
        {
            get
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(User_Info))
                {
                    options.Add("user_info", User_Info);
                }

                options.Add("quality_control", Quality_Control.ToString());

                options.Add("liveness_control", Liveness_Control.ToString());

                return options;
            }
        }
    }

    public class GetActionOption : BaseOption
    {
        public virtual int? Start { get; set; }
        public virtual int? Length { get; set; }

        public override Dictionary<string, object> Options
        {
            get
            {
                Dictionary<string, object> options = new Dictionary<string, object>();

                if (Start.HasValue)
                {
                    options.Add("start", Start);
                }

                if (Length.HasValue)
                {
                    options.Add("length", Length);
                }
                return options;
            }
        }
    }

    public class CopyUserOption : BaseOption
    {
        public virtual string Src_Group_Id { get; set; }
        public virtual string Dst_Group_Id { get; set; }

        public override Dictionary<string, object> Options
        {
            get
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(Src_Group_Id))
                {
                    options.Add("src_group_id", Src_Group_Id);
                }
                if (!string.IsNullOrEmpty(Dst_Group_Id))
                {
                    options.Add("dst_group_id", Dst_Group_Id);
                }
                return options;
            }
        }
    }
}