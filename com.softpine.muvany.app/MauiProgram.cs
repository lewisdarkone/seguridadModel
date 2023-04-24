using AutoMapper;
using Blazored.LocalStorage;
using com.softpine.muvany.app.Data;
using com.softpine.muvany.app.Tools;
using com.softpine.muvany.clientservices;
using com.softpine.muvany.component.States;
using com.softpine.muvany.component.States.User;
using com.softpine.muvany.component.Tools;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Repository.API;

namespace com.softpine.muvany.app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddMauiBlazorWebView();
            

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#else
            appConfigStream = "com.softpine.muvany.app.appsettings.json";
            
#endif
            //JSON CONFIG LOAD
            //var assembly = Assembly.GetExecutingAssembly();
            //using var stream = assembly.GetManifestResourceStream("com.softpine.muvany.app.appsettings.json");
            //var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            //builder.Configuration.AddConfiguration(config);


            //HTTP CLIENT CONFIG
            builder.Services.AddHttpClient("muvany", client =>
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.BaseAddress = new Uri("http://localhost:7290/api/");
                //client.BaseAddress = new Uri("https://9b31-2001-1308-28ed-a200-e0d2-ae21-4790-108b.ngrok-free.app/api/");

            });

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //STATES
            builder.Services.AddScoped(typeof(DevicePermissions));
            builder.Services.AddScoped(typeof(LoginState));
            builder.Services.AddScoped(typeof(RegisterUserState));
            builder.Services.AddScoped(typeof(TokenSingleton));
            builder.Services.AddScoped(typeof(MainModulosState));
            builder.Services.AddScoped(typeof(MainRecursosState));
            builder.Services.AddScoped(typeof(MainAccionesState));
            builder.Services.AddScoped(typeof(UsersBaseState));

            builder.Services.AddScoped(typeof(MainPermisosState));






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



            return builder.Build();
        }
    }
}
