﻿
namespace com.softpine.muvany.models.ResponseModels.UserResponses;

public class UpdateRoleResponse : BaseResponse
{
    
    public BasicResponse? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }

}
