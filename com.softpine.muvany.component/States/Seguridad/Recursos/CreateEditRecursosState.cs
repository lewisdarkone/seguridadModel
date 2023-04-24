using System.Diagnostics.CodeAnalysis;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class CreateEditRecursosState : RecursosBaseState
{
    public EditCreateRecursosForm EditForm;
    public EditCreateRecursosFormValidator Validator = new();
    public ICollection<ModulosDto>? Modulos;
    public ModulosDto ModuloNull = new ModulosDto() { Id=0,Nombre="Sin Modulo Padre"};
    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };
    


    [Parameter]
    [AllowNull]
    public RecursosDto? RecursoToEdit { get; set; }

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
        if ( RecursoToEdit != null )
        {
            IsUpdate = true;
            EditForm = Mapper.Map<EditCreateRecursosForm>(RecursoToEdit);
            
            if(Modulos != null)
                EditForm.Modulo = Modulos.First(x => x.Id == RecursoToEdit.IdModulo);
        }
        else
        {
            EditForm = new();
        }
    }

    public void OnSelectItem(ModulosDto? item)
    {

        EditForm.Modulo = item;
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
        var createRequest = Mapper.Map<CreateRecursosRequest>(EditForm);
        createRequest.IdModulo = EditForm.Modulo.Id != 0 ? EditForm.Modulo.Id : null;
        createRequest.EsMenuConfiguracion = EditForm.EsMenuConfiguracion ? 1 : 0;

        var res = await RecursoService.Create(createRequest);

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
        var updateRequest = Mapper.Map<UpdateRecursosRequest>(EditForm);
        updateRequest.IdModulo = EditForm.Modulo.Id;
        updateRequest.EsMenuConfiguracion = EditForm.EsMenuConfiguracion ? 1 : 0;

        var res = await RecursoService.Update(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            ShowConfirmDialog = false;
            StateHasChanged();
            await DataUpdate.InvokeAsync(true);
        }
        else if ( res.Code == 500 )
        {
            await DataUpdate.InvokeAsync(false);
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            await DataUpdate.InvokeAsync(false);
            SnackBarComponent.ShowSnackBarError("Operación fallida", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
    }

    public async Task Cancel()
    {
        
        DialogVisible = false;
        await DataUpdate.InvokeAsync(false);
    }

    private async Task GetModulos()
    {
        StaticValues.IsLoading = true;
        query = $"PageSize=100";
        Modulos = new List<ModulosDto>();
        var res = await ModuloService.GetAll(query);

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
