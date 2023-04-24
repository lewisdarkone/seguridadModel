using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.models.ResponseModels.RecursoResponse
{
    public class DeleteRecursoResponse : BaseResponse
    {
        public BasicResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
