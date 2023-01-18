using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimekeeperAPI.Data.Data.Entities
{
    /// <summary>
    /// tạo table entitie User
    /// tạo thuộc tính cho các đối tượng Use
    /// gồm thông tin của người sử dụng
    /// 6 cột
    /// </summary>
    [Table("User")]
    public class tk_User
    {
        [Key]
        public Guid Id { get; set; }

        // tên user
        public string Name { get; set; }

        // số điện thoại
        public string Phone { get; set; }

        // mật khẩu
        public string Password { get; set; }

        // thời gian đăng nhập mới nhất
        public DateTime LastLogin { get; set; }

        // quyền (use - admin)
        public string Role { get; set; }

        // danh sách lịch sử timesheet( checkin - checkout)
        public ICollection<tk_Timesheet> Tk_Timesheets { get; set; }
    }
}