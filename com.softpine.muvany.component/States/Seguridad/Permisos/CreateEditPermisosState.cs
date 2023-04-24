using System.Diagnostics.CodeAnalysis;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class CreateEditPermisosState : PermisosBaseState
{
    public EditCreatePermisosForm EditForm;
    public EditCreatePermisosFormValidator Validator = new();
    public ICollection<RecursosDto>? recursos;
    public ICollection<AccionesDto>? Acciones;

    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };



    [Parameter]
    [AllowNull]
    public PermisosDto? ItemToEdit { get; set; }

    [Parameter]
    public bool DialogVisible { get; set; }

    [Parameter]
    public EventCallback<bool> DataUpdate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAcciones();
        await GetRecursos();
    }

    protected override void OnParametersSet()
    {
        EditForm = new();

        if ( ItemToEdit != null )
        {
            IsUpdate = true;
            EditForm.Id = ItemToEdit.Id;
            EditForm.EsBasico = (int)ItemToEdit.EsBasico == 1 ? true : false;

            if ( recursos != null && recursos.Count > 0 )
                EditForm.Recurso = recursos.First(x => x.Id == ItemToEdit.IdRecurso);

            if ( Acciones != null && Acciones.Count > 0 )
                EditForm.Accion = Acciones.First(x => x.Id == ItemToEdit.IdAccion);

        }
        else
        {
            IsUpdate = false;
        }
    }

    public void OnSelectItemAccion(AccionesDto? item)
    {

        EditForm.Accion = item;
        StateHasChanged();
    }

    public void OnSelectItemRecurso(RecursosDto? item)
    {

        EditForm.Recurso = item;
        StateHasChanged();
    }

    public void ConfirmSave()
    {
        ShowConfirmDialog = true;
        StateHasChanged();
    }
    public void CancelConfirmSave()
    {
        ShowConfirmDialog = false;
        StateHasChanged();
    }

    public async Task OnSave()
    {
        if ( IsUpdate )
            await Update();
        else
            await Create();

        ShowConfirmDialog = false;
        await DataUpdate.InvokeAsync(true);
    }
    public async Task Create()
    {
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarErrorCamposRequeridos();
            return;
        }
        StaticValues.IsLoading = true;
        var createRequest = new CreateOrUpdatePermisoRequest()
        {
            IdAccion = EditForm.Accion.Id,
            IdRecurso = EditForm.Recurso.Id,
            EsBasico = EditForm.EsBasico ? 1 : 0
        };


        var res = await Service.CreatePermiso(createRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            ShowConfirmDialog = false;
            await DataUpdate.InvokeAsync(true);
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
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
        StaticValues.IsLoading = true;
        var updateRequest = new CreateOrUpdatePermisoRequest()
        {
            Id = (int)EditForm.Id,
            IdAccion = EditForm.Accion.Id,
            IdRecurso = EditForm.Recurso.Id,
            EsBasico = EditForm.EsBasico ? 1 : 0
        };

        var res = await Service.UpdatePermiso(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            ShowConfirmDialog = false;
            StateHasChanged();

        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Operación fallida", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
    }

    public async Task Cancel()
    {
        await DataUpdate.InvokeAsync(false);
        DialogVisible = false;
    }

    private async Task GetAcciones()
    {
        StaticValues.IsLoading = true;
        query = $"PageSize=100";
        Acciones = new List<AccionesDto>();
        var res = await AccionesService.GetAcciones(query);

        if ( res.Data != null && res.Data.Data != null )
        {
            Acciones = res.Data.Data.ToList();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error obteniendo módulos", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
    }

    private async Task GetRecursos()
    {
        StaticValues.IsLoading = true;
        query = $"PageSize=100";
        recursos = new List<RecursosDto>();
        var res = await RecursosService.Get(query);

        if ( res.Data != null && res.Data.Data != null )
        {
            recursos = res.Data.Data.ToList();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error obteniendo módulos", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
    }
}
