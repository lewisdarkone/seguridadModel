using Blazored.LocalStorage;
using com.softpine.muvany.clientservices;
using com.softpine.muvany.component.States;
using com.softpine.muvany.component.States.User;
using com.softpine.muvany.component.Tools;
using com.softpine.muvany.web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Repository.API;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//HTTP CLIENT CONFIG
builder.Services.AddHttpClient("muvany", client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    //client.BaseAddress = new Uri("http://localhost:7292/api/");
    client.BaseAddress = new Uri("http://10.0.0.237:9090/api/");
});

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//STATES
builder.Services.AddScoped(typeof(LoginState));
builder.Services.AddScoped(typeof(RegisterUserState));
builder.Services.AddScoped(typeof(TokenSingleton));
builder.Services.AddScoped(typeof(MainModulosState));
builder.Services.AddScoped(typeof(MainRecursosState));
builder.Services.AddScoped(typeof(MainAccionesState));
builder.Services.AddScoped(typeof(MainPermisosState));
builder.Services.AddScoped(typeof(MainAccionesState));
builder.Services.AddScoped(typeof(UsersBaseState));






//COMPONENTS
builder.Services.AddScoped(typeof(SnackBarComponent));


//SERVICES
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPersonalService, PersonalService>();
builder.Services.AddScoped<IRecursoService, RecursoService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<IAccionesService, AccionesService>();
builder.Services.AddScoped<IPermisoService, PermisoService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRolesClaimsService, RolesClaimsService>();
builder.Services.AddScoped<IEndpointService, EndpointService>();
await builder.Build().RunAsync();
