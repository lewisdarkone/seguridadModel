using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using MudBlazor;

namespace com.softpine.muvany.component.States;

public class MainRecursosState : RecursosBaseState
{
    public ICollection<RecursosDto>? ListRecursos;
    public RecursosDto? SelectedItem;
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
        ListRecursos = new List<RecursosDto>();
        var res = await Service.Get(query);

        if ( res.Data != null )
        {
            ListRecursos = res.Data.Data.ToList();
            Metadata = res.Data.Meta;
            StaticValues.TotalPages = Metadata.TotalPages;
        }
        else if ( res.Code == 500 )
        {
            SnackBarComponent.ShowSnackBarError("Error conectando al servidor", "No se pudo establecer conexión con el servidor");
        }
        else
        {
            SnackBarComponent.ShowSnackBarError("Error iniciando sesión", res.Exception.messages[0]);
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
    public void ShowEditCreate(RecursosDto? item)
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
            query = $"Nombre={SearchValue}";
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
