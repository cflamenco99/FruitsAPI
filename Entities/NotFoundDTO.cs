using System.Net;

namespace Entities
{
    public class NotFoundDTO
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}