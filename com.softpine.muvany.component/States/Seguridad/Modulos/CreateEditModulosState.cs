using System.Diagnostics.CodeAnalysis;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Request.RequestsIdentity;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class CreateEditModulosState : ModulosBaseState
{
    public EditCreateModulosForm EditForm;
    public EditCreateModulosFormValidator Validator = new();
    public ICollection<ModulosDto>? Modulos;
    public ModulosDto ModuloNull = new ModulosDto() { Id=0,Nombre="Sin Modulo Padre"};
    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };
    


    [Parameter]
    [AllowNull]
    public ModulosDto? ItemToEdit { get; set; }

    [Parameter]
    public bool DialogVisible { get; set; }

    [Parameter]
    public EventCallback<bool> DataUpdate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetModulos();
    }

    protected override void OnParametersSet()
    {
        if ( ItemToEdit != null )
        {
            IsUpdate = true;
            
            EditForm = Mapper.Map<EditCreateModulosForm>(ItemToEdit);
            
            if(Modulos != null)
                EditForm.ModuloPadreNav = Modulos.FirstOrDefault(x => x.Id == ItemToEdit.ModuloPadre);
        }
        else
        {
            EditForm = new();
            IsUpdate = false;
        }
    }

    public void OnSelectItem(ModulosDto? item)
    {

        EditForm.ModuloPadreNav = item;
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
        var createRequest = Mapper.Map<CreateModulosRequest>(EditForm);
        createRequest.ModuloPadre = EditForm.ModuloPadreNav != null ? EditForm.ModuloPadreNav.Id : null;


        var res = await Service.Add(createRequest);

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
        var updateRequest = Mapper.Map<UpdateModulosRequest>(EditForm);
        updateRequest.ModuloPadre = EditForm.ModuloPadreNav != null ? EditForm.ModuloPadreNav.Id : null;
        updateRequest.Estado = (bool)EditForm.Estado ? 1 : 0;

        var res = await Service.Update(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            ShowConfirmDialog = false;
            StateHasChanged();
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

    public void Cancel()
    {
        
        DialogVisible = false;
    }

    private async Task GetModulos()
    {
        StaticValues.IsLoading = true;
        query = $"PageSize=100";
        Modulos = new List<ModulosDto>();
        var res = await Service.GetAll(query);

        if ( res.Data != null && res.Data.Data != null )
        {
            Modulos = res.Data.Data.ToList();
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
