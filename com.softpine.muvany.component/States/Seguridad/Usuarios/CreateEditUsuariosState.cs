using System.Diagnostics.CodeAnalysis;
using com.softpine.muvany.component.Forms;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.States;

public class CreateEditUsuariosState : UsuariosBaseState
{
    public CreateEditUserForm EditForm;
    public CreateEditUserFormValidator Validator = new();
    public IEnumerable<RoleDto>? Roles;
    public IEnumerable<RoleDto>? RolesAsignados { get; set; }
    public ICollection<UserRoleDto>? RolesActuales { get; set; }

    public RoleDto? SelectedRole;
    int initialRolCount = 0;
    int finalRolCount = 0;



    public string query = string.Empty;

    public bool Success;
    public bool IsUpdate;
    public bool ShowConfirmDialog;
    public string[] Errors = { };



    [Parameter]
    [AllowNull]
    public UserDetailsDto? ItemToEdit { get; set; }

    [Parameter]
    public bool DialogVisible { get; set; }

    [Parameter]
    public EventCallback<bool> DataUpdate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        
    }

    protected override async Task OnParametersSetAsync()
    {
        StaticValues.IsLoading = true;
        await GetRoles();
        EditForm = new();
        if ( ItemToEdit != null )
        {
            IsUpdate = true;
            EditForm.Id = ItemToEdit.Id.ToString();
            EditForm.Email = ItemToEdit.Email;
            EditForm.FullName = ItemToEdit.NombreCompleto;
            EditForm.PhoneNumber = ItemToEdit.PhoneNumber;
            EditForm.Roles = ItemToEdit.Roles;

            await GetRolesAsignados();
            SplitRoles();
        }
        else
        {
            IsUpdate = false;
        }

        StaticValues.IsLoading = false;
        StateHasChanged();
    }

   
    
    public string GetMultiSelectionText(List<string> selectedValues)
    {
        
            return $"{selectedValues.Count} : {string.Join(", ", selectedValues.Select(x => x))}";
        
    }
    private async Task GetRoles()
    {
        Roles = new List<RoleDto>();
        var res = await RolesService.GetRoles("PageSize=500");

        if ( res.Data != null )
        {
            Roles = res.Data.Data.ToList();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error Obteniendo permisos", res.Exception.messages[0]);
        }
    }

    /// <summary>
    /// Obtener los roles del usuario seleccionado si es una edición
    /// </summary>
    /// <returns></returns>
    private async Task GetRolesAsignados()
    {
        if ( EditForm.Id == null )
            return;

        RolesAsignados = new List<RoleDto>();
        
        var res = await Service.GetUserRoles(EditForm.Id);

        if ( res.Data != null && res.Data.Data != null )
        {
            RolesActuales =  res.Data.Data.ToList();
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error Obteniendo permisos", res.Exception.messages[0]);
        }
        StateHasChanged();
    }

    private void SplitRoles()
    {
        var list = new List<RoleDto>();
        foreach ( var rol in RolesActuales )
        {
            var myRol = Roles.FirstOrDefault(r => r.Id == rol.RoleId);
            if ( myRol != null )
                list.Add(myRol);
        }
        RolesAsignados = list.ToList();
        initialRolCount = RolesAsignados.Count();

    }

    /// <summary>
    /// Actualizar los roles del usuario en edición
    /// </summary>
    /// <returns></returns>
    private async Task UpdateRoles()
    {
        //Si no se asignaron roles o no se cambiaron, entonces no actualiza los roles

        if ( RolesAsignados == null )
            return;

        finalRolCount = RolesAsignados.Count();

        if ( finalRolCount == initialRolCount)
            return;
        
        var updateRolesRequest = new UserRolesRequest() { UserRoles = RolesAsignados.Select(x => new RolesIds() { RoleId = x.Id}).ToList() };

        var res = await Service.UpdateUserRoles(EditForm.Id,updateRolesRequest);

        if ( res.Code == 200 )
        {
            SnackBarComponent.ShowSnackBarInfoOperacionExitosa();
            ShowConfirmDialog = false;
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
            await UpdateRoles();
        }
        else
            await Create();

        ShowConfirmDialog = false;
        StaticValues.IsLoading = false;
        await DataUpdate.InvokeAsync(true);

    }
    public async Task Create()
    {
        var createRequest = Mapper.Map<CreateUserRequest>(EditForm);
        createRequest.RoleId = EditForm.Role!.Id;

        var res = await Service.CreateUser(createRequest);

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

    /// <summary>
    /// Actualizar datos permitidos de usuario
    /// </summary>
    /// <returns></returns>
    public async Task Update()
    {
        await Form.Validate();
        if ( !Form.IsValid )
        {
            SnackBarComponent.ShowSnackBarErrorCamposRequeridos();
            return;
        }
        var updateRequest = Mapper.Map<UpdateUserRequest>(EditForm);

        var res = await Service.UpdateUser(updateRequest);

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
