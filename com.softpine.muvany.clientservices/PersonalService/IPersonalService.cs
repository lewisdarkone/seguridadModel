


using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.PersonalResponse;

namespace com.softpine.muvany.clientservices;

public interface IPersonalService
{
    Task<ProfileResponse> GetProfile();
    Task<UpdateProfileResponse> UpdateProfile(UpdateUserProfileRequest updateProfileRequest);
    Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest changePasswordRequest);
    Task<GetPermissionsResponse?> GetPermissions();
}
