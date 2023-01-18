using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimekeeperAPI.Common.Utils;

namespace TimekeeperAPI.Business.Services.User
{
    /// <summary>
    ///  tạo interface (chức năng) của một người dùng 
    /// </summary>
    public interface IUserHandler
    {
        Task<Response> GetAllUsers(UserQueryModel userQuery);
        Task<Response> GetUserById(Guid id);
        Task<Response> GetTimeScheet(Guid id);
        Task<Response> GetTimeScheetById(Guid id);
        Task<Response> CreateUser(UserCreateUpdateModel param);
        Task<Response> UpdateUserById(Guid id, UserCreateUpdateModel param);
        Task<Response> DeleteUserById(Guid id);
        Task<Response> CheckIn(Guid id, IEnumerable<TaskCreateModel> param);
        Task<Response> CheckOut(Guid id, IEnumerable<TaskUpdateModel> param);
        Task<Response> DeleteTask(Guid id);
    }
}
