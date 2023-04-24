using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace com.softpine.muvany.component.States;

public class MainPermisosState : PermisosBaseState
{
    public ICollection<PermisosDto>? ListItems;
    

    public PermisosDto? SelectedItem;
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
        ListItems = new List<PermisosDto>();
        var res = await Service.GetPermisos(query);

        if ( res.Data != null )
        {
            ListItems = res.Data.Data.ToList();
            Metadata = res.Data.Meta;
            StaticValues.TotalPages = Metadata.TotalPages;
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error Obteniendo modulos", res.Exception.messages[0]);
        }
        StaticValues.IsLoading = false;        
        StateHasChanged();
    }

    public async Task IsDataUpdated(bool updated)
    {
        if ( updated )
            await GetData();

        ShowCreateDialog = false;
    }
    public void ShowEditCreate(PermisosDto? item)
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
            query = $"Descripcion={SearchValue}";
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
