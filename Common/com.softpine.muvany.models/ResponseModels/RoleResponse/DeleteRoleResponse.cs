using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.models.ResponseModels.RoleResponse
{
    public class DeleteRoleResponse : BaseResponse
    {
        public BasicResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
