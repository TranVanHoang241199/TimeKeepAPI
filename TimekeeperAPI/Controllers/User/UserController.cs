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
    /// controller user
    /// </summary>
    [Route("api/v1/users")]
    [ApiExplorerSettings(GroupName = "User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userHandler;

        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        #region CRUD
        /// <summary>
        /// hiển thị tất cả timeScheet + task
        /// </summary>
        /// <param name="id">Nhập id User.</param>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet(), Route("check-time-scheets/{id}")]
        [ProducesResponseType(typeof(ResponseList<TimeScheetViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTimeScheets(Guid id)
        {
            var result = await _userHandler.GetTimeScheet(id);

            return Helper.TransformData(result);
        }

        /// <summary>
        /// Hiển thị một timescheet cụ thể + task
        /// </summary>
        /// <param name="id">Nhập id TimeScheet</param>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet(), Route("check-time-scheet/{id}")]
        [ProducesResponseType(typeof(ResponseObject<TimeScheetViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTimeScheet(Guid id)
        {
            var result = await _userHandler.GetTimeScheetById(id);

            return Helper.TransformData(result);
        }
        /// <summary>
        /// checkin
        /// </summary>
        /// <param name="id">Nhập id User</param>
        /// <param name="model">nhập danh sách task mới</param>
        /// <response code="200">Thành công</response>
        [Authorize(Roles = "user"), HttpPost(), Route("check-in/{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckIn(Guid id, [FromBody] IEnumerable<TaskCreateModel> model)
        {
            return Helper.TransformData(await _userHandler.CheckIn(id, model));
        }

        /// <summary>
        /// checkout
        /// </summary>
        /// <param name="id">Nhập id timeCheet</param>
        /// <param name="model">xét duyệt task</param>
        /// <response code="200">Thành công</response>
        [Authorize(Roles = "user"), HttpPut(), Route("check-out/{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckOut(Guid id, [FromBody] IEnumerable<TaskUpdateModel> model)
        {
            var searchResults = await _userHandler.CheckOut(id, model);

            return Helper.TransformData(searchResults);
        }

        /// <summary>
        /// xóa một task
        /// </summary>
        /// <param name="id">Nhập id task</param>
        /// <response code="200">Thành công</response>
        [Authorize(Roles = "user"), HttpDelete(), Route("delete-task/{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var result = await _userHandler.DeleteTask(id);

            return Helper.TransformData(result);
        }


        #endregion CRUD
    }
}
