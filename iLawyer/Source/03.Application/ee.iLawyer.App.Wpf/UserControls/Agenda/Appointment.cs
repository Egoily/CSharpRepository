﻿using ee.iLawyer.Ops.Contact;
using System;

namespace ee.iLawyer.App.Wpf.UserControls.Agenda
{
    public class Appointment
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual int AppointmentId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Subject { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime? RemindTime { get; set; }

        public RemindTimeType RemindTimeType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }


        public Appointment()
        {
            StartTime = DateTime.Now;
            CreatedTime = DateTime.Now;
        }
    }
}
