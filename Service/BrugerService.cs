using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAcces.Specific;
using RestSharp;
using Newtonsoft.Json;

namespace Service
{
    public class BrugerService : IBrugerService
    {
        private BrugerRepo repository;
        private IRestClient client;
        public BrugerService(BrugerRepo repo, IRestClient client)
        {
            repository = repo;
            this.client = client;
        }
        public async Task<PB_Bruger> FindUserFromSharedId(string id)
        {
            if (id == string.Empty)
                throw new Exception("Empty search string");
            var user = await repository.GetBrugerFromSharedId(id);
            if (user == null)
                throw new Exception("User not found");
            return user;
        }

        public async Task CrateUserRestApi(PB_Bruger model)
        {
            var request = new RestRequest("api/User/PostCreateUser", Method.POST);
            var serialiseret = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json; charset=utf-8", serialiseret, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = await client.Client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Either e-mail or SharedId is already taken, try again");
            }
        }
    }
}
