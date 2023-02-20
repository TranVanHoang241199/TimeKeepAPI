using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimekeeperAPI.Common.Utils;
using TimekeeperAPI.Data.Data.DbContexts;
using TimekeeperAPI.Data.Data.Entities;

namespace TimekeeperAPI.Business.Services.User
{
    /// <summary>
    /// lớp xử lý phương thức của user
    /// </summary>
    public class UserHandler : IUserHandler
    {
        private readonly TkDbContext _context;
        private readonly IMapper _mapper;

        public UserHandler(TkDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// lấy ra danh sách Users
        /// có tìm kiếm theo tên, số điện thoại và id
        /// có phân trang
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        public async Task<Response> GetAllUsers(UserQueryModel userQuery)
        {
            try
            {
                var collection = _context.Tk_Users.Include(u => u.Tk_Timesheets).ThenInclude(t => t.Tk_Tasks) as IQueryable<tk_User>;

                if (!string.IsNullOrWhiteSpace(userQuery.SearchQuery))
                {
                    var searchQuery = userQuery.SearchQuery.Trim();
                    collection = collection.Where(c => c.Name.Contains(searchQuery) || c.Phone.Contains(searchQuery));
                }

                if (userQuery.id != Guid.Empty)
                {
                    collection = collection.Where(c => c.Id == userQuery.id);
                }

                // phân trang
                collection = collection.Skip(userQuery.PageSize * (userQuery.PageNumber - 1))
                    .Take(userQuery.PageSize);

                var result = await collection.ToListAsync();

                var contactToReturn = _mapper.Map<IEnumerable<tk_User>, IEnumerable<UserViewModel>>(result);

                return new ResponseObject<IEnumerable<UserViewModel>>(contactToReturn, "get success");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("userQuery: {@userQuery}", userQuery);
                return new ResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// lấy ra một User dựa vào id user
        /// </summary>
        /// <param name="id">user</param>
        /// <returns></returns>
        public async Task<Response> GetUserById(Guid id)
        {
            try
            {
                var userToGet = await _context.Tk_Users.Include(u => u.Tk_Timesheets).ThenInclude(t => t.Tk_Tasks).FirstOrDefaultAsync(u => u.Id == id);

                if (userToGet == null)
                    return new Response(HttpStatusCode.NotFound, "Not Found");

                var contactToReturn = _mapper.Map<tk_User, UserViewModel>(userToGet);

                return new ResponseObject<UserViewModel>(contactToReturn, "get success");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Id: {@Id}", id);
                return new ResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// tạo một user
        /// </summary>
        /// <param name="param">thông tin của user</param>
        /// <returns></returns>
        public async Task<Response> CreateUser(UserCreateUpdateModel param)
        {
            try
            {
                var userToCreate = new tk_User();
                userToCreate.Id = Guid.NewGuid();
                userToCreate.Name = param.Name;
                userToCreate.Phone = param.Phone;
                userToCreate.Password = param.Password;
                userToCreate.Role = param.Role;

                await _context.Tk_Users.AddAsync(userToCreate);

                var status = await _context.SaveChangesAsync();

                if (status <= 0)
                    return new ResponseError(HttpStatusCode.NotFound, "update failed");

                var data = _mapper.Map<UserViewModel>(userToCreate);

                return new ResponseObject<UserViewModel>(data, "create success.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Param: {@Param}", param);
                return new Response<UserViewModel>
                {
                    Data = null,
                    Message = ex.Message,
                    Code = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// cập nhật thông tin user bằng id user
        /// </summary>
        /// <param name="id">user</param>
        /// <param name="param">thông tin user</param>
        /// <returns></returns>
        public async Task<Response> UpdateUserById(Guid id, UserCreateUpdateModel param)
        {
            try
            {
                var userToUpdate = await _context.Tk_Users.Include(u => u.Tk_Timesheets).FirstOrDefaultAsync(u => u.Id == id);

                if (userToUpdate == null)
                    return new ResponseError(HttpStatusCode.NotFound, "Check id again");

                userToUpdate.Id = id;
                userToUpdate.Name = param.Name;
                userToUpdate.Phone = param.Phone;
                userToUpdate.Password = param.Password;
                userToUpdate.Role = param.Role;

                var status = await _context.SaveChangesAsync();

                if (status <= 0)
                    return new ResponseError(HttpStatusCode.NotFound, "update failed.");

                var data = _mapper.Map<tk_User, UserViewModel>(userToUpdate);

                return new ResponseObject<UserViewModel>(data, "edit successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Param: {@Param}, Id: {@Id}", param, id);
                return new Response<UserViewModel>
                {
                    Data = null,
                    Message = ex.Message,
                    Code = HttpStatusCode.InternalServerError

                };
            }
        }

        /// <summary>
        /// checkIn thời gian làm việc trong ngày
        /// </summary>
        /// <param name="id">user</param>
        /// <param name="param">thông tin timeScheet</param>
        /// <returns></returns>
        public async Task<Response> CheckIn(Guid id, IEnumerable<TaskCreateModel> param)
        {
            try
            {
                var userId = await _context.Tk_Users.FirstOrDefaultAsync(u => u.Id == id);

                if (userId == null)
                    return new Response(HttpStatusCode.NotFound, "User Not Found");

                // tao moi mot timecheet de luu thong tin
                var newTimeCheet = new tk_Timesheet();
                newTimeCheet.TimeCheckin = DateTime.Now;
                newTimeCheet.Id = Guid.NewGuid();
                newTimeCheet.Tk_UsersId = userId.Id;

                // ta task cong viec
                newTimeCheet.Tk_Tasks = new List<tk_Task>();
                foreach (var task in param)
                {
                    var newTask = new tk_Task();
                    newTask.Id = Guid.NewGuid();
                    newTask.Tk_TimesheetsId = newTimeCheet.Id;
                    newTask.Title = task.Title;
                    newTask.Content = task.Content;
                    newTask.TimeTask = DateTime.Now;
                    newTask.Type = "PLANNED";
                    newTask.CompletedStatus = "UNFINISHED";

                    if (DateTime.Now.Hour > 9)
                    {
                        newTask.CreationTime = "LATE";
                    }
                    else
                    {
                        newTask.CreationTime = "ON TIME";
                    }

                    newTimeCheet.Tk_Tasks.Add(newTask);
                }
                // cap nhat so luong task PLANNED
                newTimeCheet.TaskPlannedCount = newTimeCheet.Tk_Tasks.Where(t => t.Type.Equals("PLANNED")).Count();

                await _context.Tk_Timesheets.AddAsync(newTimeCheet);

                var status = await _context.SaveChangesAsync();

                if (status <= 0)
                    return new ResponseError(HttpStatusCode.NotFound, "save failed");

                var data = _mapper.Map<TimeScheetViewModel>(newTimeCheet);
                return new ResponseObject<TimeScheetViewModel>(data, "create success.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Param: {@Param}, Id: {@Id}", param, id);
                return new Response<TimeScheetViewModel>
                {
                    Data = null,
                    Message = ex.Message,
                    Code = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// lấy ra một timeScheet dựa vào id timeScheet
        /// </summary>
        /// <param name="id">timesheet</param>
        /// <returns></returns>
        public async Task<Response> GetTimeScheetById(Guid id)
        {
            try
            {
                var TimeScheetToGet = await _context.Tk_Timesheets.Include(u => u.Tk_Tasks).Where(u => u.Id == id).FirstOrDefaultAsync();

                if (TimeScheetToGet == null)
                    return new Response(HttpStatusCode.NotFound, "TimeScheet Not Found");

                var timeScheetToReturn = _mapper.Map<tk_Timesheet, TimeScheetViewModel>(TimeScheetToGet);

                return new ResponseObject<TimeScheetViewModel>(timeScheetToReturn, "Get successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Id: {@Id}", id);
                return new ResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// lấy ra danh sách timeScheet dựa vào id user
        /// </summary>
        /// <param name="id">user</param>
        /// <returns></returns>
        public async Task<Response> GetTimeScheet(Guid id)
        {
            try
            {
                var userToGet = await _context.Tk_Users.Include(u => u.Tk_Timesheets).Where(u => u.Id == id).FirstOrDefaultAsync();

                if (userToGet == null)
                    return new Response(HttpStatusCode.NotFound, "User Not Found");

                var TimeScheetToGet = await _context.Tk_Timesheets.Include(u => u.Tk_Tasks).Where(u => u.Tk_UsersId == userToGet.Id).ToListAsync();

                var timeScheetToReturn = _mapper.Map<IEnumerable<tk_Timesheet>, IEnumerable<TimeScheetViewModel>>(TimeScheetToGet);

                return new ResponseObject<IEnumerable<TimeScheetViewModel>>(timeScheetToReturn, "Get successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Id: {@Id}", id);
                return new ResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// CheckOut thời gian làm việc (timeScheet)
        /// </summary>
        /// <param name="id">timeScheet</param>
        /// <param name="param">thông tin task</param>
        /// <returns></returns>
        public async Task<Response> CheckOut(Guid id, IEnumerable<TaskUpdateModel> param)
        {
            try
            {
                // lấy ra, kiem tra
                var timeScheet = await _context.Tk_Timesheets.Where(t => t.Id == id).FirstOrDefaultAsync();

                if (timeScheet == null)
                    return new Response(HttpStatusCode.NotFound, "Not found");

                // cong theem 8h de kiem tra
                var timeToCompare = timeScheet.TimeCheckin.AddHours(8);

                /*
                 * > 0 thời gian làm việc lơn hơn 8 tiếng 
                 * < 0 thoi gian lam viec be hon 8 tieng
                 * = 0 thoi gian lam ban 8 tieng
                 */
                if (DateTime.Compare(timeToCompare, DateTime.Now) < 0)
                    return new Response(HttpStatusCode.NotFound, "Not time");

                // update Task
                foreach (var task in param)
                {
                    var tasktoUpdate = await _context.Tk_Tasks.FirstOrDefaultAsync(a => a.Tk_TimesheetsId == timeScheet.Id && a.Title == task.Title);

                    bool isTask = false;

                    // kiem tra task ton tai hay khong neu khong thi new create
                    if (tasktoUpdate == null)
                    {
                        tasktoUpdate = new tk_Task();
                        tasktoUpdate.Id = Guid.NewGuid();
                        tasktoUpdate.Type = "OUTSTANDING";
                        tasktoUpdate.CreationTime = "ON TIME";
                        tasktoUpdate.TimeTask = DateTime.Now;
                        isTask = true;
                    }

                    // update du lieu cho task
                    tasktoUpdate.Title = task.Title;
                    tasktoUpdate.Content = task.Content;
                    tasktoUpdate.CompletedStatus = task.CompletedStatus;
                    tasktoUpdate.Tk_TimesheetsId = timeScheet.Id;

                    // kiem tra neu khong hoan thanh thi cho phep ghi chu
                    if (!tasktoUpdate.CompletedStatus.Equals("COMPLETE"))
                        tasktoUpdate.Note = task.Note;

                    if (isTask)
                        _context.Tk_Tasks.Add(tasktoUpdate);
                }

                // update timeScheet
                timeScheet.TimeCheckout = DateTime.Now;
                timeScheet.CompletePlannedCount = timeScheet.Tk_Tasks.Where(t => t.CompletedStatus.Equals("COMPLETE")).Count();
                timeScheet.OutStandingCount = timeScheet.Tk_Tasks.Where(t => t.Type.Equals("OUTSTANDING")).Count();

                // timeScheet.WorkingTime = DateUtil.HandleHours(DateTime.Now, timeScheet.TimeCheckin);
                timeScheet.WorkingTime = DateTime.Now.Subtract(timeScheet.TimeCheckin).ToString("h'h 'm'm 's's'");

                // tinh phan tram
                double sumTask = timeScheet.TaskPlannedCount + timeScheet.OutStandingCount;
                timeScheet.CompletionRate = timeScheet.CompletePlannedCount / sumTask;

                await _context.SaveChangesAsync();

                var data = _mapper.Map<tk_Timesheet, TimeScheetViewModel>(timeScheet);

                return new ResponseObject<TimeScheetViewModel>(data, "update successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("Param: {@Param}, Id: {@Id}", param, id);
                return new Response<TimeScheetViewModel>
                {
                    Data = null,
                    Message = ex.Message,
                    Code = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// xóa một user dựa vào id user
        /// </summary>
        /// <param name="id">user</param>
        /// <returns></returns>
        public async Task<Response> DeleteUserById(Guid id)
        {
            try
            {
                var userToDelete = await _context.Tk_Users.Include(u => u.Tk_Timesheets).FirstOrDefaultAsync(u => u.Id == id);

                if (userToDelete == null)
                    return new ResponseError(HttpStatusCode.NotFound, "Not Found.");

                _context.Tk_Users.Remove(userToDelete);

                var status = await _context.SaveChangesAsync();

                if (status <= 0)
                    return new ResponseError(HttpStatusCode.NotFound, "delete Save failed.");

                return new ResponseDelete(HttpStatusCode.OK, "successful delete.", id, userToDelete.Name);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong.");
                return new ResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);

            }

        }

        /// <summary>
        /// Xóa một task dựa vào id task
        /// </summary>
        /// <param name="id">task</param>
        /// <returns></returns>
        public async Task<Response> DeleteTask(Guid id)
        {
            try
            {
                // delete task
                var taskToDelete = await _context.Tk_Tasks.FirstOrDefaultAsync(t => t.Id == id);


                if (taskToDelete == null)
                    return new ResponseError(HttpStatusCode.NotFound, "Not Found.");

                var saveId = taskToDelete.Tk_TimesheetsId;

                _context.Remove(taskToDelete);
                var statusDelete = await _context.SaveChangesAsync();

                if (statusDelete <= 0)
                    return new ResponseError(HttpStatusCode.NotFound, "delete failed.");

                // update timeScheet
                var timeScheet = await _context.Tk_Timesheets.Include(t => t.Tk_Tasks).FirstOrDefaultAsync(t => t.Id == saveId);

                timeScheet.TaskPlannedCount = timeScheet.Tk_Tasks.Where(t => t.Type.Equals("PLANNED")).Count();
                timeScheet.CompletePlannedCount = timeScheet.Tk_Tasks.Where(t => t.CompletedStatus.Equals("COMPLETE")).Count();
                timeScheet.OutStandingCount = timeScheet.Tk_Tasks.Where(t => t.Type.Equals("OUTSTANDING")).Count();

                // tính phần trăm
                double sumTask = timeScheet.TaskPlannedCount + timeScheet.OutStandingCount;
                timeScheet.CompletionRate = timeScheet.CompletePlannedCount / sumTask;

                var statusUpdate = await _context.SaveChangesAsync();

                if (statusUpdate <= 0)
                    return new ResponseError(HttpStatusCode.NotFound, "Update TimeCheet failed.");

                return new ResponseDelete(HttpStatusCode.OK, "successful delete.", id, taskToDelete.Title);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong.");
                Log.Error("Id: {@Id}", id);
                return new ResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);

            }
        }
    }
}
