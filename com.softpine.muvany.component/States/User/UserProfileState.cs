

using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace com.softpine.muvany.component.States.User;

public class UserProfileState : UsersBaseState
{
    public UserDetailsDto? UserDetailsDto;
    public UserProfileForm UserProfileForm;
    public UpdateUserProfileRequest updateRequest;
    public UserProfileFormValidator Validator = new UserProfileFormValidator();

    protected override async Task OnInitializedAsync()
    {
        UserProfileForm = new();
        await GetProfile();
    }

    public void GoToReset()
    {
        NavigationManager.NavigateTo(NavigationManager.BaseUri + "resetpassword");
    }
    public void OnCancel()
    {
        NavigationManager.NavigateTo(StaticValues.HomePage);
        StateHasChanged();
    }
    public async Task GetProfile()
    {
        StaticValues.IsLoading = true;
        StaticValues.ActualPage = 1;

        UserDetailsDto = await GetUserProfile();

        if ( UserDetailsDto != null )
        {
            UserProfileForm = Mapper.Map<UserProfileForm>(UserDetailsDto);
            UserProfileForm.Id = await LocalStorageService.GetItemAsync<string>(LocalStorageValueKey.USERID);
        }
        
        StaticValues.IsLoading = false;
        StateHasChanged();
    }
    public async Task UpdateProfile()
    {
        StaticValues.IsLoading = true;
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarError("Campos requeridos", "Algunos campos son requeridos o no tienen el formato correcto");
            StaticValues.IsLoading = false;
            return;
        }
        updateRequest = Mapper.Map<UpdateUserProfileRequest>(UserProfileForm);
        var res = await PersonalService.UpdateProfile(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            await GetProfile();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error actualizando perfil", res.Exception.messages[0]);
        }

        StaticValues.IsLoading = false;
    }
}
