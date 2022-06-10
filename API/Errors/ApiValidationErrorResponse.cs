using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base((int)HttpStatusCode.BadRequest)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}