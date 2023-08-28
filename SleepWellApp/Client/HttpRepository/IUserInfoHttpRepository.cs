using HPCTech2023FavoriteMovie.Shared;
using HPCTech2023FavoriteMovie.Shared.Wrappers;
using SleepWellApp.Shared;
using SleepWellApp.Shared.Wrappers;

namespace SleepWellApp.Client.HttpRepository
{
    public interface IUserInfoHttpRepository
    {
        Task<DataResponse<List<UserDto>>> GetUsers();
        Task<DataResponse<List<RoleDto>>> GetRoles(string id);
        Task<Response> AddRole(string role);

        //AI API METHODS
    
    }
}
