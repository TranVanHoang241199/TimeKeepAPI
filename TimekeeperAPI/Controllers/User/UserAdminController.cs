using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimekeeperAPI.Business.Services.User;
using TimekeeperAPI.Common.Utils;

namespace TimekeeperAPI.Controllers.User
{
    /// <summary>
    /// controller admin
    /// </summary>
    [Authorize(Roles = "admin"), Route("api/v1/admin/users")]
    [ApiExplorerSettings(GroupName = "User Management")]
    [ApiController]
    public class UserAdminController : ControllerBase
    {
        private readonly IUserHandler _userHandler;

        public UserAdminController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        #region CRUD
        /// <summary>
        /// lấy tất của các User
        /// </summary>
        /// <param name="param">nhập thông tin cần tìm kiếm</param>
        /// <response code="200">Thành công</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseList<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilter([FromQuery] UserQueryModel param)
        {
            var result = await _userHandler.GetAllUsers(param);

            return Helper.TransformData(result);
        }


        /// <summary>
        /// lấy một user cụ thể
        /// </summary>
        /// <param name="id">nhập id user</param>
        /// <response code="200">Thành công</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseObject<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _userHandler.GetUserById(id);

            return Helper.TransformData(result);
        }

        /// <summary>
        /// tạo mới một user
        /// </summary>
        /// <param name="model">Nhập thông tin cần tạo</param>
        /// <response code="200">Thành công</response>
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateUpdateModel model)
        {
            return Helper.TransformData(await _userHandler.CreateUser(model));
        }

        /// <summary>
        /// chỉnh sửa một user
        /// </summary>
        /// <param name="id">nhập id user</param>
        /// <param name="model">Enter information to edit</param>
        /// <response code="200">Thành công</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserCreateUpdateModel model)
        {
            var searchResults = await _userHandler.UpdateUserById(id, model);

            return Helper.TransformData(searchResults);
        }

        /// <summary>
        /// xóa một User
        /// </summary>
        /// <param name="id">nhập id user</param>
        /// <response code="200">Thành công</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userHandler.DeleteUserById(id);

            return Helper.TransformData(result);
        }
        #endregion CRUD
    }
}
