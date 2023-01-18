using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TimekeeperAPI.Business.Services.Auth;
using TimekeeperAPI.Business.Services.User;
using TimekeeperAPI.Data.Data.Entities;

namespace TimekeeperAPI.AutoMapper
{
    /// <summary>
    /// lớp tạo ra để tự động map giữa hai lớp có cùng thuộc tính
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperConfig()
        {
            //User
            CreateMap<tk_User, UserViewModel>().ForMember(dest => dest.Timesheets, x => x.MapFrom(src => src.Tk_Timesheets));
            CreateMap<UserCreateUpdateModel, tk_User>();

            //check
            CreateMap<tk_Timesheet, TimeScheetViewModel>().ForMember(dest => dest.Tasks, x => x.MapFrom(src => src.Tk_Tasks));
            CreateMap<tk_Task, TaskCreateModel>();
            CreateMap<tk_Task, TaskUpdateModel>();
            CreateMap<tk_Task, TaskViewModel>();

            //account
            CreateMap<tk_User, AuthUser>();
        }
    }
}
