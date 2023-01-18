using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimekeeperAPI.Common.Utils;

namespace TimekeeperAPI.Business.Services.Auth
{
    /// <summary>
    /// tạo interface (chức năng) phân quyền
    /// </summary>
    public interface IAuthHandler
    {
        Task<Response> Authenticate(AuthUser userCred);
    }
}
