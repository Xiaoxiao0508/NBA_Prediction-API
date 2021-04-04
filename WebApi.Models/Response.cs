using System;

namespace WebApi.Models
{
    // The code is a generic class that takes in a Model of type T.
    //  We did that so any Models that require pagination can use it.
    public class Response<T>
    {
        public Response()
    {
    }
    public Response(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        Errors = null;
        Data = data;
    }
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
    public string Message { get; set; }
    }
}
