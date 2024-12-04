using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class SecurityHelper : ISecurityHelper
    {

        IServiceRepository ServiceRepository;

        public SecurityHelper(IServiceRepository serviceRepository)
        {
            ServiceRepository = serviceRepository;  
        }
        public LoginAPI Login(UserViewModel user)
        {
            try
            {
                HttpResponseMessage response = ServiceRepository
                                                    .PostResponse("/api/Auth/login", new { user.UserName, user.Password });
                var content = response.Content.ReadAsStringAsync().Result;
                LoginAPI loginAPI = JsonConvert.DeserializeObject<LoginAPI>(content);

                return loginAPI;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public RegisterAPI Register(UserViewModel user)
        {
            try
            {
                HttpResponseMessage response = ServiceRepository
                                                    .PostResponse("/api/Auth/register", new { user.UserName, user.Password, user.Email });
                var content = response.Content.ReadAsStringAsync().Result;
                RegisterAPI registerAPI= JsonConvert.DeserializeObject<RegisterAPI>(content);

                return registerAPI;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
