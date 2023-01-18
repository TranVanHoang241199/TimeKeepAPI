using System;
using System.Collections.Generic;
using System.Text;

namespace TimekeeperAPI.Business.Services.User
{
    /// <summary>
    /// model hiển thị user + danh sách timecheet
    /// </summary>
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime LastLogin { get; set; }
        public ICollection<TimeScheetViewModel> Timesheets { get; set; }
    }

    /// <summary>
    /// model tạo mới một use
    /// </summary>
    public class UserCreateUpdateModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    /// <summary>
    /// model hiển thị timecheet + danh sách task
    /// </summary>
    public class TimeScheetViewModel
    {
        public Guid Id { get; set; }
        public DateTime TimeCheckin { get; set; }
        public DateTime? TimeCheckout { get; set; }
        public string WorkingTime { get; set; }
        public int TaskPlannedCount { get; set; }
        public int CompletePlannedCount { get; set; }
        public int OutStandingCount { get; set; }
        public double CompletionRate { get; set; }
        public ICollection<TaskViewModel> Tasks { get; set; }
    }

    /// <summary>
    /// model tạo mới task
    /// </summary>
    public class TaskCreateModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    /// <summary>
    /// model chỉnh sửa task
    /// </summary>
    public class TaskUpdateModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string CompletedStatus { get; set; }
    }

    /// <summary>
    /// model hiển thị task
    /// </summary>
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string CreationTime { get; set; }
        public string Type { get; set; }
        public string CompletedStatus { get; set; }
        public DateTime TimeTask { get; set; }
    }

    /// <summary>
    /// model hiển thị use có tìm kiếm phân trang
    /// </summary>
    public class UserQueryModel
    {
        public string SearchQuery { get; set; }
        public Guid id { get; set; }

        // paging
        const int maxPageSize = 1;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value < maxPageSize) ? maxPageSize : value;
        }

    }

}
