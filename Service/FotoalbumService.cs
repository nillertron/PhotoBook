using DataAcces.Specific;
using Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FotoalbumService : IFotoalbumService
    {
        private IRestClient client;
        private ILoginStateService state;
        private FotoalbumRepo repo;
        public FotoalbumService(IRestClient c, ILoginStateService state, FotoalbumRepo repo)
        {
            client = c;
            this.state = state;
            this.repo = repo;
        }
        public async Task<PB_Fotoalbum> GetApi(int photoAlbumId)
        {
            return null;
        }
        public async Task<List<PB_Fotoalbum>> GetAllForUserApi(int userId)
        {
            var request = new RestRequest("api/Fotoalbum/" + userId, Method.GET);
            var response = await client.Client.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<List<PB_Fotoalbum>>(response.Content);
            return result;
        }

        public async Task CreateApi(PB_Fotoalbum model)
        {
            var request = new RestRequest("api/Fotoalbum/create", Method.POST);
            model.PB_BrugerId = state.user.Id;
            var serialiseret = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json; charset=utf-8", serialiseret, ParameterType.RequestBody);
            var response = await client.Client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Something went wrong");
            }
            else
            {
                state.user.Fotoalbum.Add(model);
            }
        }
        public async Task DeleteApi(PB_Fotoalbum model)
        {

        }
        public async Task<List<PB_Foto>> GetFotoAlbum(int id)
        {
            return await repo.GetUserFotosFromAlbumid(id);
        }
    }
}
