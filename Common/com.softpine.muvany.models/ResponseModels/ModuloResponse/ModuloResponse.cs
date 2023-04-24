using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.models.ResponseModels.ModuloResponse
{
    public class ModuloResponse : BaseResponse
    {
        public bool done { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
