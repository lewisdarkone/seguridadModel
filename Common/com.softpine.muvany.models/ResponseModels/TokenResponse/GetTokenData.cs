

using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels.TokenResponse
{
    public class GetTokenData
    {
        public GetTokenUserData? Data { get; set; }
        public Metadata? Meta { get; set; }

    }
    public class GetTokenUserData
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        public string? Email { get; set; }
        public string? NombreCompleto { get; set; }
        public int TypeRol { get; set; }
        public string[]? Role { get; set; }

    }
}
