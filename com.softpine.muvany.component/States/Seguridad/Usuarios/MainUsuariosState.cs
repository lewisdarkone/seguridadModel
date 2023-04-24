using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace com.softpine.muvany.component.States;

public class MainUsuariosState : UsuariosBaseState
{
    public ICollection<UserDetailsDto>? ListItems;
    

    public UserDetailsDto? SelectedItem;
    private Metadata? Metadata;
    public string SearchValue = string.Empty;
    public string query = string.Empty;
    public bool ShowCreateDialog;


    protected override async Task OnInitializedAsync()
    {
        SetQuery();
        await GetData();

    }

    private void SetQuery()
    {
        query = $"PageSize={StaticValues.sizePage}&PageNumber={StaticValues.ActualPage}";
    }
    private async Task GetData()
    {
        StaticValues.IsLoading = true;
        StaticValues.ActualPage = 1;
        ListItems = new List<UserDetailsDto>();
        var res = await Service.GetUsers(query);

        if ( res.Data != null )
        {
            ListItems = res.Data.Data.ToList();
            Metadata = res.Data.Meta;
            StaticValues.TotalPages = Metadata.TotalPages;
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", res.Exception.messages[0]);
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error Obteniendo modulos", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
    }

    public async Task IsDataUpdated(bool updated)
    {
        if ( updated )
            await GetData();

        ShowCreateDialog = false;
        
        StaticValues.IsLoading = false;
    }

    public void Test(bool t) { }
    public async Task ActivateToggle(bool active,UserDetailsDto usr)
    {
        StaticValues.IsLoading = true;
        var changeStatusRequest = new ToggleUserStatusRequest() {UserId = usr.Id.ToString(),ActivateUser=active };


        var res = await Service.ChangeUserStatus(changeStatusRequest);

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
            SnackBarComponent.ShowSnackBarError("Error Obteniendo permisos", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;
        await GetData();
        StateHasChanged();
    }
    public void ShowEditCreate(DataGridRowClickEventArgs<UserDetailsDto>? item)
    {
        SelectedItem = item.Item;
        ShowCreateDialog = true;
    }
    public void ShowEditCreate(UserDetailsDto? item)
    {
        SelectedItem = item;
        ShowCreateDialog = true;
        StateHasChanged();
    }
    public void AddNew()
    {
        SelectedItem = null;
        ShowCreateDialog = true;
    }
    public async Task Search()
    {
        if ( SearchValue != string.Empty )
            query = $"NombreCompleto={SearchValue}";
        else
            SetQuery();

        StaticValues.ActualPage = 1;
        await GetData();
    }

    public async Task PageChanged(int page)
    {
        StaticValues.IsLoading = true;
        StaticValues.ActualPage = page;
        SetQuery();
        await GetData();
        StaticValues.IsLoading = false;
    }
}
