using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.models.ResponseModels
{
    public class ExceptionResponse
    {
        public string[]? messages { get; set; }
        public string? source { get; set; }
        public string? exception { get; set; }
        public string? errorId { get; set; }
        public string? supportMessage { get; set; }
        public int? statusCode { get; set; }
    }
}
