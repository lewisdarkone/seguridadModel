
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.UserResponses;

namespace com.softpine.muvany.clientservices
{
    /// <summary>
    /// Endpoints: Users, UsersDomain
    /// </summary>
    public interface IUserService
    {
        Task<GetUsersResponse?> GetUsers(string query);
        Task<GetUserResponse?> GetUserbyId(string id);
        Task<GetRolesResponse?> GetUserRoles(string userId);
        Task<UpdateRoleResponse?> UpdateUserRoles(string userId, UserRolesRequest userRoles);
        Task<CreateUserResponse?> CreateUser(CreateUserRequest userRequest);
        Task<UpdateUserResponse?> UpdateUser(UpdateUserRequest updateUserRequest);
        Task<ChangeUserStatusResponse?> ChangeUserStatus(ToggleUserStatusRequest chageUserStatusRequest);
        Task<ConfirmEmailResponse?> ConfirmUserEmail(string query);
        Task<ForgotPasswordResponse?> ForgotPassword(string email);
        Task<ResetPasswordResponse?> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<GetUserDomainResponse?> GetUserDomain(string email);
        Task<UpdateResponse> RegisterUser(RegisterUserRequest request);
    }
}
