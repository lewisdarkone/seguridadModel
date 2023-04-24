using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.States.User;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class LoginState : UsersBaseState
{
    



    protected override async Task OnInitializedAsync()
    {
        ForgotPass = false;
        await CheckRecuerdame();
    }

    private async Task CheckRecuerdame()
    {
        Recuerdame = await LocalStorageService.GetItemAsync<bool>(LocalStorageValueKey.RECORDARUSUARIO);
        if ( Recuerdame )
        {
            var usrEmail = await LocalStorageService.GetItemAsync<string>(LocalStorageValueKey.USEREMAIL);
            var pwd = await LocalStorageService.GetItemAsync<string>(LocalStorageValueKey.PASSWORD);

            NewLoginForm = new LoginForm() { Email = usrEmail, Contrasena = pwd };
            await Login(false);
        }
        else
        {
            await LocalStorageService.ClearAsync();
        }
    }

    public void ForgotPassword()
    {
        ShowForgotDialog = true;
        StateHasChanged();
    }

    public void OnForgotAction()
    {
        ShowForgotDialog = false;
        StateHasChanged();
    }

    public async Task Login(bool validate = true)
    {
        StaticValues.IsLoading = true;

        if (validate )
        {
            await Form.Validate();

            if ( !Form.IsValid )
            {
                SnackBarComponent.ShowSnackBarError("Campos requeridos", "Algunos campos son requeridos o no tienen el formato correcto");
                StaticValues.IsLoading = false;
                return;
            }
        }        

        var tokenResponse = await TokenService.Login(new TokenRequest(NewLoginForm.Email, NewLoginForm.Contrasena));

        if ( tokenResponse.Code == 200 )
        {
            

            await LocalStorageService.SetItemAsync(LocalStorageValueKey.USERTOKEN, tokenResponse.Data.Data.Token);
            await LocalStorageService.SetItemAsync(LocalStorageValueKey.USEREMAIL, tokenResponse.Data.Data.Email);
            await LocalStorageService.SetItemAsync(LocalStorageValueKey.NOMBRECOMPLETO, tokenResponse.Data.Data.NombreCompleto);

            if ( string.IsNullOrEmpty(tokenResponse.Data.Data.Token) )
            {
                NavigationManager.NavigateTo("/usuario/validatecode");
                StaticValues.IsLoading = false;
                return;
            }

            var userDetails = await GetUserProfile();
            if ( userDetails != null )             
                await LocalStorageService.SetItemAsync(LocalStorageValueKey.USERID, userDetails.Id);

            

            if ( Recuerdame )
            {
                await LocalStorageService.SetItemAsync(LocalStorageValueKey.RECORDARUSUARIO, true);
                await LocalStorageService.SetItemAsync(LocalStorageValueKey.PASSWORD, NewLoginForm.Contrasena);
            }

            NavigationManager.NavigateTo(StaticValues.HomePage, true);

        } else if ( tokenResponse.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error iniciando sesión", tokenResponse.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;

    }

    public async Task GoRegister()
    {
        NavigationManager.NavigateTo("/RegisterUser", true);
    }
}
