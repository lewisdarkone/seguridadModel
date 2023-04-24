namespace com.softpine.muvany.models.ResponseModels.RoleResponse
{
    public class CreateRoleResponse :BaseResponse
    {
        public BasicResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
