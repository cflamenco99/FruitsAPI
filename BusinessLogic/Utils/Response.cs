namespace BusinessLogic.Utils
{
    public sealed class Response<TType>
    {
        public bool Ok { get; set; }
        public bool NotFoundData { get; set; }
        public string Message { get; set; }

        public TType Data { get; set; } = default;


        public static Response<TType> Success()
        {
            return Success(string.Empty);
        }

        public static Response<TType> Success(string message)
        {
            return new Response<TType>
            {
                Ok = true,
                Message = message
            };
        }

        public static Response<TType> Success(TType data)
        {
            return Success(data, "");
        }

        public static Response<TType> Success(TType data, string message)
        {
            return new Response<TType>
            {
                Ok = true,
                Message = message,
                Data = data
            };
        }

        public static Response<TType> Fault(string message = "", TType data = default(TType), bool notFound = false)
        {
            return new Response<TType>
            {
                Ok = false,
                Message = message,
                Data = data,
                NotFoundData = notFound
            };
        }
    }
}