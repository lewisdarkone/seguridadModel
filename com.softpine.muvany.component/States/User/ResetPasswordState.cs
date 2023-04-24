

using System.Diagnostics.CodeAnalysis;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace com.softpine.muvany.component.States.User;

public class ResetPasswordState : UsersBaseState
{
    public ResetPasswordForm ResetForm;
    public ResetPasswordFormValidator Validator = new ResetPasswordFormValidator();    

    protected override void OnInitialized()
    {
        ResetForm = new();
    }
    public void OnForgotAction()
    {
        ShowForgotDialog = false;
        StateHasChanged();
    }
    public void OnCancel()
    {
        NavigationManager.NavigateTo("/");
    }
    public void ForgotPassword()
    {
        ShowForgotDialog = true;
        StateHasChanged();
    }
    public async Task ResetPassword()
    {
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarError("Campos requeridos", "Algunos campos son requeridos o no tienen el formato correcto");
            return;
        }
        StaticValues.IsLoading = true;

        var resetPasswordRequest = new ResetPasswordRequest() 
        {
            Email = ResetForm.Email,
            Password = ResetForm.Password,
            ConfirmPassword = ResetForm.ConfirmPassword,
            CodigoValidacion = ResetForm.ValidationCode
        
        };

        var res = await UserService.ResetPassword(resetPasswordRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            NavigationManager.NavigateTo("/", true);

        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error iniciando sesión", res.Exception.messages[0]);
        }

        StaticValues.IsLoading = false;
    }
}
