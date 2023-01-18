using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimekeeperAPI.Business.Services.Auth;
using TimekeeperAPI.Common.Utils;

namespace TimekeeperAPI.Controllers.Auth
{
    /// <summary>
    /// controller phân quyền
    /// </summary>
    [Route("api/v1/auth")]
    [ApiExplorerSettings(GroupName = "Authorization")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _accountHandler;

        public AuthController(IAuthHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        /// <summary>
        /// đăng nhập
        /// </summary>
        /// <param name="param">Nhập tài khoản, mật khẩu User</param>
        /// <response code="200">Thành công</response>
        [AllowAnonymous, HttpPost(), Route("LoginUser")]
        [ProducesResponseType(typeof(ResponseList<UserAccountViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] AuthUser param)
        {
            var toKen = await _accountHandler.Authenticate(param);

            if (toKen == null)
                return Unauthorized();

            return Helper.TransformData(toKen);
        }
    }
}
