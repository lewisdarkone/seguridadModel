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

public class CreateEditRolesState : RolesBaseState
{
    public EditCreateRoleForm EditForm;
    public EditCreateRoleFormValidator Validator = new();
    public IEnumerable<PermisosDto>? PermisosDisponibles;
    public IEnumerable<PermisosDto>? PermisosAsignados { get; set; }
    public ICollection<RolePermisoModel> PermisosActuales;
    public ICollection<RolesClaimDto>? RolPermisos;

    public PermisosDto SelectedPermiso { get; set; } = new();

    public List<int> LeftCheck = new List<int>();
    public List<int> RightCheck = new List<int>();


    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };



    [Parameter]
    [AllowNull]
    public RoleDto? ItemToEdit { get; set; }

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
            EditForm.Id = ItemToEdit.Id;
            EditForm.RolInterno = (int)ItemToEdit.TypeRol == 66 ? true : false;
            EditForm.Name = ItemToEdit.Name;
            EditForm.Description = ItemToEdit.Description;
        }
        else
        {
            IsUpdate = false;
        }

        StaticValues.IsLoading = true;
        await GetPermisos();

        if ( !string.IsNullOrEmpty(EditForm.Id) )
        {
            await GetPermisosRole();
            SplitPermisos();
        }
        StaticValues.IsLoading = false;
        StateHasChanged();
    }

   
    
    public string GetMultiSelectionText(List<string> selectedValues)
    {
        
            return $"{selectedValues.Count} : {string.Join(", ", selectedValues.Select(x => x))}";
        
    }
    private async Task GetPermisos()
    {
        PermisosDisponibles = new List<PermisosDto>();
        var res = await PermisosService.GetPermisos("PageSize=500");

        if ( res.Data != null )
        {
            PermisosDisponibles = res.Data.Data.ToList();
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

    private async Task GetPermisosRole()
    {     
        PermisosActuales = new List<RolePermisoModel>();
        var res = await RolesClaimsService.GetRoleClaimsPermission(ItemToEdit.Id);

        if ( res.Data != null )
        {
            PermisosActuales = res.Data.Data.Permisos.ToList();
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

    private async Task UpdatePermisosRole()
    {
        var updateRoleRequest = new CreateRolePermissionsRequest();
        updateRoleRequest.RoleId = EditForm.Id;
        updateRoleRequest.Permissions = PermisosAsignados.Select(x => new PermissionsRequest() { Id = x.Id, Descripcion = x.Descripcion }).ToList();


        var res = await RolesClaimsService.UpdateRoleClaims(updateRoleRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
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

    private void SplitPermisos()
    {
        PermisosAsignados = new HashSet<PermisosDto>();
        var list = new List<PermisosDto>();
        foreach ( var p in PermisosActuales )
        {
            var exist = PermisosDisponibles.FirstOrDefault(x => x.Id == p.Id);
            if ( exist != null )
            {                
                list.Add(exist);
            }
            
        }
        PermisosAsignados = list.ToList();
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
            if(EditForm.Name != "Administrador")
            await Update();

            await UpdatePermisosRole();
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
        var createRequest = new CreateRoleRequest()
        {
            Name = EditForm.Name,
            Description = EditForm.Description,
            TypeRol = EditForm.RolInterno ? 66 : 67
        };


        var res = await Service.CreateRole(createRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            ShowConfirmDialog = false;
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
        var updateRequest = new UpdateRoleRequest()
        {
            Id = EditForm.Id,
            Name = EditForm.Name,
            Description = EditForm.Description
        };

        var res = await Service.UpdateRole(updateRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();

        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
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
