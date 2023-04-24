using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.States.User;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class ValidateLoginCodeState : UsersBaseState
{



    public ValidateCodeForm validateCodeForm;
    public ValidateCodeFormValidator validateCodeFormValidator = new ValidateCodeFormValidator();

    protected override async Task OnInitializedAsync()
    {
        ForgotPass = false;
        validateCodeForm = new();
        validateCodeForm.Email = await LocalStorageService.GetItemAsync<string>(LocalStorageValueKey.USEREMAIL);
        validateCodeForm.NombreCompleto = await LocalStorageService.GetItemAsync<string>(LocalStorageValueKey.NOMBRECOMPLETO);
    }


    public void OnCancel()
    {
        NavigationManager.NavigateTo(NavigationManager.BaseUri);
        StateHasChanged();
    }

    public async Task ValidateCode()
    {
        StaticValues.IsLoading = true;


        await Form.Validate();

        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarError("Campos requeridos", "Algunos campos son requeridos o no tienen el formato correcto");
            StaticValues.IsLoading = false;
            return;
        }


        var tokenResponse = await TokenService.GetByCodeToken(new TokenByCodeRequest(validateCodeForm.Email, validateCodeForm.Code.ToString()));

        if ( tokenResponse.Code == 200 )
        {
            await LocalStorageService.SetItemAsync(LocalStorageValueKey.USERTOKEN, tokenResponse.Data.Data.Token);

            var userDetails = await GetUserProfile();
            if ( userDetails != null )
                await LocalStorageService.SetItemAsync(LocalStorageValueKey.USERID, userDetails.Id);

            NavigationManager.NavigateTo(StaticValues.HomePage, true);

        }
        else if ( tokenResponse.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error iniciando sesión", tokenResponse.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;

    }

}
