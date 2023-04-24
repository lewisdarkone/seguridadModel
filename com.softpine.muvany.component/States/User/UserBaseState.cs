using AutoMapper;
using Blazored.LocalStorage;
using com.softpine.muvany.clientservices;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace com.softpine.muvany.component.States.User;

public class UsersBaseState : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; }

    [Inject]
    public IPersonalService PersonalService { get; set; }

    [Inject]
    public ITokenService TokenService { get; set; }

    [Inject]
    public ILocalStorageService LocalStorageService { get; set; }
    [Inject]
    public IMapper Mapper { get; set; }

    [Inject]
    public SnackBarComponent SnackBarComponent { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public MudForm Form = new MudForm();
    public LoginForm NewLoginForm = new LoginForm();
    public bool ForgotPass { get; set; }

    public bool Success { get; set; }
    public bool Recuerdame { get; set; }
    public bool ShowForgotDialog;
    public string[] Errors = { };
    public LoginFormValidator Validator = new LoginFormValidator();

    public async Task<UserDetailsDto?> GetUserProfile()
    {
        var res = await PersonalService.GetProfile();
        if (res.Code == 200 )
        {
            return res.Data!.Data;
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error Obteniendo modulos", res.Exception.messages[0]);
        }

        return null;
    }
}
