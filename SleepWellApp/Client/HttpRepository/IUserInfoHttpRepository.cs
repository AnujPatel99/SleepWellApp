using HPCTech2023FavoriteMovie.Shared.Wrappers;
using SleepWellApp.Shared;

namespace SleepWellApp.Client.HttpRepository
{
    public interface IUserInfoHttpRepository
    {
        Task<DataResponse<List<UserDto>>> GetUsers();
        Task<DataResponse<List<string>>> GetRoles(string id);

        

    }
}
