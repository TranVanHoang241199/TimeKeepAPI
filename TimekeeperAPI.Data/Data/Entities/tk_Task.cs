using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimekeeperAPI.Data.Data.Entities
{
    /// <summary>
    /// tạo table entitie Task
    /// tạo thuộc tính cho các đối tượng Tash
    /// gồm các thông tin của task việc trong một ngày
    /// 9 cột
    /// </summary>
    [Table("Task")]
    public class tk_Task
    {
        [Key]
        public Guid Id { get; set; }

        // tiêu đề
        public string Title { get; set; }

        // nội dung  
        public string Content { get; set; }

        // ghi chú (hoàn thành mới được ghi chú)
        public string Note { get; set; }

        /// <summary>
        /// kiểm tra task tạo đúng thời gian không
        /// ON TIME: tạo task đúng thời gian
        /// LATE: tạo muộn giờ làm
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// Phân biệt task
        /// PLANNED: tạo task khi bắt đầu checkin
        /// OUTSTANDING: tạo task khi checkout
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// tạng thái hoàn thành
        /// UNFINISHED: chưa hoàn thành
        /// COMPLETE: Hoàn thành task
        /// </summary>
        public string CompletedStatus { get; set; }

        // thời gian tạo task
        public DateTime TimeTask { get; set; }

        /// <summary>
        /// khóa ngoại của kết nối với bản thời quản lý nhiệm vụ đã hoàn thành trong ngày
        /// </summary>
        [ForeignKey("Tk_Timesheets")]
        public Guid Tk_TimesheetsId { get; set; }
        public virtual tk_Timesheet Tk_Timesheets { get; set; }
    }
}
