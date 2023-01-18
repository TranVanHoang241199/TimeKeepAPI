using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimekeeperAPI.Data.Data.Entities
{
    /// <summary>
    /// tạo table entitie TimeSheet 
    /// tạo thuộc tính cho các đối tượng timeSheet
    /// gồm các thông tin của một ngày làm việc
    /// 9 cột
    /// </summary>
    [Table("Timesheet")]
    public class tk_Timesheet
    {
        [Key]
        public Guid Id { get; set; }

        // thời gian checkin
        public DateTime TimeCheckin { get; set; }

        // thời gian checkout
        public DateTime? TimeCheckout { get; set; }

        // thời gian hoàn thành
        public string WorkingTime { get; set; }

        // tổng số task tạo khi checkin
        public int TaskPlannedCount { get; set; }

        // tổng số task đã hoàn thành
        public int CompletePlannedCount { get; set; }

        // tổng số task làm thêm khi checkout
        public int OutStandingCount { get; set; }

        // phần tram hoan thanh task
        public double CompletionRate { get; set; }

        // danh sách task của một timesheet
        public ICollection<tk_Task> Tk_Tasks { get; set; }

        // khóa ngoại
        [ForeignKey("Tk_Users")]
        public Guid Tk_UsersId { get; set; }
        public virtual tk_User Tk_Users { get; set; }
    }
}
