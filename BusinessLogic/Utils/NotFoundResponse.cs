using Entities;
using System.Net;

namespace BusinessLogic.Utils
{
    public static class NotFoundResponse
    {
        public static NotFoundDTO Get(string message)
        {
            return new NotFoundDTO
            {
                Date = DateTime.UtcNow,
                Message = message,
                Status = HttpStatusCode.NotFound
            };
        }
    }
}
