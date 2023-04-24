using AutoMapper;
using Blazored.LocalStorage;
using com.softpine.muvany.clientservices;
using com.softpine.muvany.component.Tools;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace com.softpine.muvany.component.States;

public class ModulosBaseState : ComponentBase
{
    [Inject]
    public IModuloService Service { get; set; }

    [Inject]
    public ILocalStorageService LocalStorageService { get; set; }

    [Inject]
    public SnackBarComponent SnackBarComponent { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IMapper Mapper { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }

    public MudForm Form = new MudForm();

}
