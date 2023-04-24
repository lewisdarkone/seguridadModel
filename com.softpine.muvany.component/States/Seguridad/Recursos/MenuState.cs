using com.softpine.muvany.component.Tools;
using com.softpine.muvany.core.Contracts;
using MudBlazor;

namespace com.softpine.muvany.component.States;

public class MenuState : RecursosBaseState
{
    public ICollection<Menu>? RecursosMenu;
    public bool open = false;
    public bool openProfile = false;
    public string Company = "Mi Compañia SRL";
    public string UserName = "";


    protected override async Task OnInitializedAsync()
    {
        RecursosMenu = new List<Menu>();
        UserName = await LocalStorageService.GetItemAsync<string>(LocalStorageValueKey.USERNAME);
        var res = await Service.GetMenu();

        if ( res.Data != null )
        {
            RecursosMenu = res.Data.Data.ToList();
        }
    }

    public void ToggleDrawer()
    {
        open = !open;
    }

    public void ToggleProfile()
    {
        openProfile = !openProfile;
    }
    public void GoHome()
    {
        NavigationManager.NavigateTo(StaticValues.HomePage);
    }

    public async Task Salir()
    {
        await LocalStorageService.ClearAsync();
        NavigationManager.NavigateTo(NavigationManager.BaseUri);
    }
}
