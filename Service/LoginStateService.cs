using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAcces;
using RestSharp;
using Model;
using Newtonsoft.Json;
namespace Service
{
    public class LoginStateService : ILoginStateService
    {
        public PB_Bruger user { get; private set; }
        private IRestClient client;

        public LoginStateService(IRestClient c)
        {
            client = c;
        }
        public async Task Login(string email, string password)
        {
            var userToSearch = new PB_Bruger { Email = email, Password = password };
            var request = new RestRequest("api/User", Method.POST);
            var serialiseret = JsonConvert.SerializeObject(userToSearch);
            request.AddParameter("application/json; charset=utf-8", serialiseret, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                user = JsonConvert.DeserializeObject<PB_Bruger>(response.Content);
            }
            else
                throw new Exception("User not found");

        }
        public async Task Logout()
        {
            if (user != null)
                user = null;
            else throw new Exception("No user was online");
        }
        public async Task GetUserFromId(int id)
        {
            var request = new RestRequest("api/User/" + id, Method.GET);
            var response = client.Client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                user = JsonConvert.DeserializeObject<PB_Bruger>(response.Content);
            }

        }

        public async Task EditUser(PB_Bruger model)
        {
            var rq = new RestRequest("api/User/Edit", Method.POST);
            var s = JsonConvert.SerializeObject(model);
            rq.AddParameter("application/json; charset=utf-8", s, ParameterType.RequestBody);
            var response = await client.Client.ExecuteAsync(rq);
            var result = JsonConvert.DeserializeObject<string>(response.Content);
            if (result != "Ok")
            {
                throw new Exception(result);
            }
        }
    }
}
