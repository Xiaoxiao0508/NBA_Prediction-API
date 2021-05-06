using System;

namespace NBA_API.Models
{
    // The code is a generic class that takes in a Model of type T.
    //  We did that so any Models that require pagination can use it.
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int Pages { get; set; }

        public Response()
        {

        }

        public Response(T data)
        {
            Data = data;
        }

        public Response(T data, int pagesCount)
        {
            Data = data;
            Succeeded = true;
            Message = string.Empty;
            Pages = pagesCount;
        }
    }
}
