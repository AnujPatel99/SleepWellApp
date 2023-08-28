using SleepWellApp.Shared;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using HPCTech2023FavoriteMovie.Shared.Wrappers;
using HPCTech2023FavoriteMovie.Shared;
using SleepWellApp.Shared.Wrappers;

namespace SleepWellApp.Client.HttpRepository
{
    public class UserInfoHttpRepository : IUserInfoHttpRepository
    {
        public readonly HttpClient _httpClient;
        private const string ReplicateAPIToken = "r8_Ci8f12uEXCMuBYzWJGIdBvw8vxbQ3Gf2kkiHI";
        private const string ApiBaseURL = "https://api.replicate.com/v1/predictions/";
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

        public async Task<DataResponse<List<RoleDto>>> GetRoles(string userId)
        {
            try
            {
                var roles = await _httpClient.GetFromJsonAsync<List<RoleDto>>($"api/get-roles/{userId}");
                // package in dataresponse
                return new DataResponse<List<RoleDto>>()
                {
                    Data = roles,
                    Message = "Success",
                    Succeeded = true
                };

            }
            catch (Exception ex)
            {
                return new DataResponse<List<RoleDto>>()
                {
                    Errors = new Dictionary<string, string[]> { { ex.Message, new string[] { ex.Message } } },
                    Data = new List<RoleDto>(),
                    Message = ex.Message,
                    Succeeded = false
                };
            }
        }

        public async Task<Response> AddRole(string role)
        {
            try
            {
                RoleDto newRole = new RoleDto()
                {
                    Name = role
                };
                var res = await _httpClient.PostAsJsonAsync<RoleDto>("api/add-role", newRole);
                if (res.IsSuccessStatusCode)
                {
                    return new Response()
                    {
                        Message = "Success",
                        Succeeded = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Failed",
                        Succeeded = false,
                        Errors = new Dictionary<string, string[]> { { "Failed to add Role", new string[] { "Failed to add Role" } } }
                    };
                }

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
    }
}