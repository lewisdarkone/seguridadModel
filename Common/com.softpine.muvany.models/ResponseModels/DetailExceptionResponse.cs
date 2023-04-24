using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.models.ResponseModels
{
    public class DetailExceptionResponse
    {
        public string? type { get; set; }
        public string? title { get; set; }
        public int status { get; set; }
        public string? detail { get; set; }
        public string? instance { get; set; }
        public ExtensionsPermisos? extensions { get; set; }
        public ErrorsPermisos? errors { get; set; }
        public string? additionalProp1 { get; set; }
        public string? additionalProp2 { get; set; }
        public string? additionalProp3 { get; set; }

    }

    public class ExtensionsPermisos
    {
        public string? additionalProp1 { get; set; }
        public string? additionalProp2 { get; set; }
        public string? additionalProp3 { get; set; }

    }
    public class ErrorsPermisos
    {
        public string[]? additionalProp1 { get; set; }
        public string[]? additionalProp2 { get; set; }
        public string[]? additionalProp3 { get; set; }
    }
}

