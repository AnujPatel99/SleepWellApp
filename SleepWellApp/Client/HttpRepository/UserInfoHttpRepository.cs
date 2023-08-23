using SleepWellApp.Shared;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using HPCTech2023FavoriteMovie.Shared.Wrappers;

namespace SleepWellApp.Client.HttpRepository
{
    public  class UserInfoHttpRepository : IUserInfoHttpRepository
    {
        public readonly HttpClient _httpClient;
        public UserInfoHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DataResponse<List<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/get-users");
                // package in dataresponse
                return new DataResponse<List<UserDto>>()
                {
                    Data = users,
                    Message = "Success",
                    Succeeded = true
                };

            }
            catch (Exception ex)
            {
                return new DataResponse<List<UserDto>>()
                {
                    Errors = new Dictionary<string, string[]> { { ex.Message, new string[] { ex.Message } } },
                    Data = new List<UserDto>(),
                    Message = ex.Message,
                    Succeeded = false
                };
            }
        }

        public async Task<DataResponse<List<string>>> GetRoles(string userId)
        {
            try
            {
                var roles = await _httpClient.GetFromJsonAsync<List<string>>($"api/get-roles/{userId}");
                // package in dataresponse
                return new DataResponse<List<string>>()
                {
                    Data = roles,
                    Message = "Success",
                    Succeeded = true
                };

            }
            catch (Exception ex)
            {
                return new DataResponse<List<string>>()
                {
                    Errors = new Dictionary<string, string[]> { { ex.Message, new string[] { ex.Message } } },
                    Data = new List<string>(),
                    Message = ex.Message,
                    Succeeded = false
                };
            }
        }
    }
}
