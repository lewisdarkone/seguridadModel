using System.Diagnostics.CodeAnalysis;
using System.Xml;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.RoleClaims;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class CreateEditEndpointState : EndpointBaseState
{
    public EditCreateEndpointForm EditForm;
    public EditCreateEndpointFormValidator Validator = new();
    public IEnumerable<PermisosDto>? Permisos;

    public string[] Verbos = new string[] { "GET","POST","PUT","DELETE"};

    public PermisosDto SelectedPermiso { get; set; } = new();
    public string Selected { get; set; } = string.Empty;


    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };



    [Parameter]
    [AllowNull]
    public EndpointsDto? ItemToEdit { get; set; }

    [Parameter]
    public bool DialogVisible { get; set; }

    [Parameter]
    public EventCallback<bool> DataUpdate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        
    }

    protected override async Task OnParametersSetAsync()
    {
        EditForm = new();
        if ( ItemToEdit != null )
        {
            IsUpdate = true;
            EditForm = Mapper.Map<EditCreateEndpointForm>(ItemToEdit);
        }
        else
        {
            IsUpdate = false;
        }

        StaticValues.IsLoading = true;
        await GetPermisos();
        StaticValues.IsLoading = false;
        StateHasChanged();
    }   
   
    private async Task GetPermisos()
    {
        Permisos = new List<PermisosDto>();
        var res = await PermisosService.GetPermisos("PageSize=500");

        if ( res.Data != null )
        {
            Permisos = res.Data.Data.ToList();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error Obteniendo permisos", res.Exception.messages[0]);
        }
    }

    public void OnSelectVerbo(string verbo)
    {
        EditForm.HttpVerbo = verbo;
        StateHasChanged();
    }
    public void OnSelectPermiso(PermisosDto permiso)
    {
        EditForm.Permiso = permiso;
        StateHasChanged();
    }
    public async Task ConfirmSave()
    {
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarErrorCamposRequeridos();
            return;
        }
        ShowConfirmDialog = true;
        StateHasChanged();
    }
    public async Task CancelConfirmSave()
    {
       ShowConfirmDialog = false;
       await DataUpdate.InvokeAsync(false);
       StateHasChanged();
    }

    public async Task OnSave()
    {
        StaticValues.IsLoading = true;
        if ( IsUpdate )
        {
            await Update();
        }
        else
            await Create();

        ShowConfirmDialog = false;
        StaticValues.IsLoading = false;
        await DataUpdate.InvokeAsync(true);

    }
    public async Task Create()
    {
        
        StaticValues.IsLoading = true;
        var createRequest = Mapper.Map<CreateEndpointRequest>(EditForm);


        var res = await Service.CreateEndpoints(createRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            EditForm.Id = res.Data.Data.Id;
            await UpdatePermiso();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Operación fallida", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
    }

    public async Task Update()
    {
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarErrorCamposRequeridos();
            return;
        }
        var updateRequest = Mapper.Map<UpdateEndpointRequest>(EditForm);

        var res = await Service.UpdateEndpoints(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            await UpdatePermiso();

        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Operación fallida", res.Exception.messages[0]);
        }
    }

    public async Task UpdatePermiso()
    {
        if ( EditForm.Permiso == null )
            return;

        var updateRequest = new CreateOrUpdateEndpointPermisoRequest() { EndpointId = (int)EditForm.Id, PermisoId = EditForm.Permiso.Id , Estado = true };

        var res = await Service.AssignEndpointPermiso(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();

        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Operación fallida", res.Exception.messages[0]);
        }
    }

    public async Task Cancel()
    {
        await DataUpdate.InvokeAsync(false);
        DialogVisible = false;
        StateHasChanged();
    }
   
}
