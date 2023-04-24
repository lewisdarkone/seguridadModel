using Blazored.LocalStorage;

namespace com.softpine.muvany.clientservices;

public class TokenSingleton
{
    private readonly ILocalStorageService _localStorage;
    private string _token;
    public TokenSingleton(ILocalStorageService localStorageService)
    {
        _localStorage = localStorageService;
    }

    public async Task<string> GetToken() => !string.IsNullOrEmpty(_token) ? _token : await _localStorage.GetItemAsync<string>("UserToken");

}
