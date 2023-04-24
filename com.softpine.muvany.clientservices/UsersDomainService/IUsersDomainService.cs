

using com.softpine.muvany.models.ResponseModels.UsersDomainResponse;

namespace com.softpine.muvany.clientservices.UsersDomainService;

public interface IUsersDomainService
{
    Task<GetUsersDomainByIdResponse?> GetByEmail(string email);
    Task<GetUsersDomainResponse?> Get(string query = "");
}
