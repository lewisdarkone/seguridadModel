

using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.component.States.User;

public class RegisterUserState : UsersBaseState
{
    public RegisterUserForm RegisterUserForm;
    public RegisterUserRequest RegisterUserRequest;
    public RegisterUserValidator Validator = new RegisterUserValidator();

    protected override void OnInitialized()
    {        
        RegisterUserForm = new();
    }

    public void GoToReset()
    {
        NavigationManager.NavigateTo(NavigationManager.BaseUri + "resetpassword");
    }
    public async Task Registrar()
    {
        StaticValues.IsLoading = true;
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarError("Campos requeridos", "Algunos campos son requeridos o no tienen el formato correcto");
            StaticValues.IsLoading = false;
            return;
        }
        RegisterUserRequest = new();

        RegisterUserRequest.FullName = $"{RegisterUserForm.FirstName} {RegisterUserForm.LastName}";
        RegisterUserRequest.Email = RegisterUserForm.Email;

        var res = await UserService.RegisterUser(RegisterUserRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfo("Cuenta Creada", "Por favor colocar el codigo recibido en su correo para activar su cuenta.");
            GoToReset();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error iniciando sesión", res.Exception.messages[0]);
        }

        StaticValues.IsLoading = false;
    }
}
