using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TimekeeperAPI.Common.Utils
{
    /// <summary>
    ///     Return object
    /// </summary>
    public class Response
    {
        [JsonConstructor]
        public Response(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public Response(string message)
        {
            Message = message;
        }

        public Response()
        {
        }

        /// <summary>
        /// Error code returned
        /// </summary>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = "Success";

        /// <summary>
        /// Time requested
        /// </summary>
        public long TotalTime { get; set; } = 0;
    }

    /// <summary>
    /// Returns the type of object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        [JsonConstructor]
        public Response(T data)
        {
            Data = data;
            Code = HttpStatusCode.OK;
        }

        public Response(HttpStatusCode code, T data)
        {
            Data = data;
            Message = "OK";
        }

        public Response(HttpStatusCode code, T data, string message)
        {
            Code = code;
            Data = data;
            Message = message;
        }

        public Response()
        {

        }

        /// <summary>
        ///     Returned data
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    ///     Trả về dạng mảng
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseList<T> : Response
    {
        [JsonConstructor]
        public ResponseList(List<T> data)
        {
            Data = data;
        }

        public ResponseList()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public List<T> Data { get; set; }
    }

    public class ResponsePagination<T> : Response
    {
        [JsonConstructor]
        public ResponsePagination(Pagination<T> data)
        {
            Data = data;
        }

        /// <summary>
        ///     List of data returned
        /// </summary>
        public Pagination<T> Data { get; set; }
    }

    /// <summary>
    /// Pagination object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T>
    {
        public Pagination()
        {
            Size = 20;
            Page = 1;
            Content = new List<T>();
        }

        /// <summary>
        ///Current page position
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///The total number of pages in the whole system
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        ///Number of records per page
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///Number of records returned
        /// </summary>
        public int NumberOfElements { get; set; }

        /// <summary>
        ///The total number of records searchable
        /// </summary>
        public int TotalElements { get; set; }

        /// <summary>
        ///List of data returned
        /// </summary>
        public List<T> Content { get; set; }
    }

    /// <summary>
    ///     Trả về Lỗi
    /// </summary>
    public class ResponseError : Response
    {
        [JsonConstructor]
        public ResponseError(HttpStatusCode code, string message, List<Dictionary<string, string>> errorDetail = null) : base(
            code,
            message)
        {
            ErrorDetail = errorDetail;
        }

        public List<Dictionary<string, string>> ErrorDetail { get; set; }
    }

    /// <summary>
    ///     Trả về dạng đối tượng
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseObject<T> : Response
    {
        [JsonConstructor]
        public ResponseObject(T data)
        {
            Data = data;
        }

        public ResponseObject(T data, string message)
        {
            Data = data;
            Message = message;
        }

        public ResponseObject(T data, string message, HttpStatusCode code)
        {
            Code = code;
            Data = data;
            Message = message;
        }

        /// <summary>
        ///     Dữ liệu trả về
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả cập nhật dữ liệu
    /// </summary>
    public class ResponseUpdate : Response
    {
        [JsonConstructor]
        public ResponseUpdate(Guid id)
        {
            Data = new ResponseUpdateModel { Id = id };
        }

        public ResponseUpdate(Guid id, string message) : base(message)
        {
            Data = new ResponseUpdateModel { Id = id };
        }

        public ResponseUpdate(HttpStatusCode code, string message, Guid id) : base(code, message)
        {
            Data = new ResponseUpdateModel { Id = id };
        }

        public ResponseUpdate()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public ResponseUpdateModel Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả cập nhật nhiều dữ liệu
    /// </summary>
    public class ResponseUpdateMulti : Response
    {
        [JsonConstructor]
        public ResponseUpdateMulti(List<ResponseUpdate> data)
        {
            Data = data;
        }

        public ResponseUpdateMulti()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public List<ResponseUpdate> Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả xóa dữ liệu
    /// </summary>
    public class ResponseDelete : Response
    {
        [JsonConstructor]
        public ResponseDelete(Guid id, string name)
        {
            Data = new ResponseDeleteModel { Id = id, Name = name };
        }

        public ResponseDelete(HttpStatusCode code, string message, Guid id, string name) : base(code, message)
        {
            Data = new ResponseDeleteModel { Id = id, Name = name };
        }

        public ResponseDelete()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public ResponseDeleteModel Data { get; set; }
    }

    /// <summary>
    ///     Trả về kết quả xóa nhiều dữ liệu
    /// </summary>
    public class ResponseDeleteMulti : Response
    {
        [JsonConstructor]
        public ResponseDeleteMulti(List<ResponseDelete> data)
        {
            Data = data;
        }

        public ResponseDeleteMulti()
        {
        }

        /// <summary>
        ///     Danh sách dữ liệu trả về
        /// </summary>
        public List<ResponseDelete> Data { get; set; }
    }

    /// <summary>
    ///     Đối tượng kết quả cập nhật
    /// </summary>
    public class ResponseUpdateModel
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    ///     Đối tượng kết quả xóa
    /// </summary>
    public class ResponseDeleteModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}