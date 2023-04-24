using com.softpine.muvany.component.Tools;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.ResponseModels.ModuloResponse;
using MudBlazor;

namespace com.softpine.muvany.component.States;

public class MainModulosState : ModulosBaseState
{
    public ICollection<ModulosDto>? ListItems;
    public ModulosDto? SelectedItem;
    private Metadata? Metadata;
    public string SearchValue = string.Empty;
    public string query = string.Empty;
    public bool ShowCreateDialog;


    protected override async Task OnInitializedAsync()
    {

       ListItems = await GetData();

    }

   
    private async Task<ICollection<ModulosDto>?> GetData(string? query = null)
    {
        StaticValues.IsLoading = true;
        StaticValues.ActualPage = 1;
        var res = new GetModulosResponse();

        if ( string.IsNullOrEmpty(query))        
            res = await Service.GetAll();
        else
            res = await Service.GetAll(query);

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
        
        return ListItems;
    }

    public async Task IsDataUpdated(bool updated)
    {
        if ( updated )
            await GetData();

        ShowCreateDialog = false;
    }
    public void ShowEditCreate(ModulosDto? item)
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
        StaticValues.ActualPage = 1;

        if ( SearchValue != string.Empty )
        {
            query = $"Nombre={SearchValue}";
            ListItems = await GetData(query);
        }
        else
        {        
            ListItems = await GetData();
        }

    }

    public async Task PageChanged(int page)
    {
        StaticValues.IsLoading = true;
        StaticValues.ActualPage = page;        
        ListItems = await GetData();
        StaticValues.IsLoading = false;
        StateHasChanged();
    }
}
