using System.Diagnostics.CodeAnalysis;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class CreateEditAccionesState : AccionesBaseState
{
    public EditCreateAccionesForm EditForm;
    public EditCreateAccionesFormValidator Validator = new();
    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };
    


    [Parameter]
    [AllowNull]
    public AccionesDto? ItemToEdit { get; set; }

    [Parameter]
    public bool DialogVisible { get; set; }

    [Parameter]
    public EventCallback<bool> DataUpdate { get; set; }

    protected override async Task OnInitializedAsync()
    {
       
    }

    protected override void OnParametersSet()
    {
        if ( ItemToEdit != null )
        {
            IsUpdate = true;
            
            EditForm = Mapper.Map<EditCreateAccionesForm>(ItemToEdit);
            
        }
        else
        {
            EditForm = new();
            IsUpdate = false;
        }
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
        var createRequest = Mapper.Map<CreateAccionesRequest>(EditForm);


        var res = await Service.CreateAcciones(createRequest);

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
        var updateRequest = Mapper.Map<UpdateAccionesRequest>(EditForm);

        var res = await Service.UpdateAcciones(updateRequest);

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

    public async Task Cancel()
    {
        
        DialogVisible = false;
       await DataUpdate.InvokeAsync(false);
    }

}
