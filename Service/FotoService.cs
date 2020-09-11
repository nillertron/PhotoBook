using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FotoService : IFotoService
    {
        private IRestClient client;
        private ILoginStateService loginState;
        public FotoService(IRestClient c, ILoginStateService loginState)
        {
            client = c;
            this.loginState = loginState;
        }
        public async Task Get()
        {

        }
        public async Task UploadPhoto(string location, string beskrivelse, int albumId)
        {
            var request = new RestRequest("api/Foto/upload", Method.POST);
            request.AddFile("file", location);
            request.AddHeader("Content-Type", "multipart/form-data");
            var response = await client.Client.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<string>(response.Content);
            if (result == "No file" || result == "Invalid file type")
            {
                throw new Exception(response.Content);
            }
            else
            {
                await UploadToDb(result, beskrivelse, albumId);
            }


        }
        private async Task UploadToDb(string url, string beskrivelse, int albumId)
        {
            var obj = new PB_Foto { Url = "http://photobook.nillertron.com/images/" + url, Beskrivelse = beskrivelse, PB_FotoalbumId = albumId, OprettetDato = DateTime.Now };
            var request = new RestRequest("api/Foto/UploadToDb", Method.POST);
            var serialiseret = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json; charset=utf-8", serialiseret, ParameterType.RequestBody);
            var response = await client.Client.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<string>(response.Content);
            if (result == "Ok")
            {
                var album = loginState.user.Fotoalbum.Where(x => x.Id == albumId).FirstOrDefault();
                album.Fotos.Insert(0,obj);
            }


        }
        public async Task DeleteFotoAPIAsync(PB_Foto foto)
        {
            var rq = new RestRequest("api/Foto/Delete", Method.POST);
            var serialiseret = JsonConvert.SerializeObject(foto);
            rq.AddParameter("application/json; charset=utf-8", serialiseret, ParameterType.RequestBody);
            var result = await client.Client.ExecuteAsync(rq);
            var deserialiseret = JsonConvert.DeserializeObject<string>(result.Content);
            if(deserialiseret != "Ok")
            {
                throw new Exception(deserialiseret);
            }
        }
        public async Task UpdateFotoAPIAsync(PB_Foto foto)
        {
            var rq = new RestRequest("api/Foto/Edit", Method.POST);
            var s = JsonConvert.SerializeObject(foto);
            rq.AddParameter("application/json; charset=utf-8", s, ParameterType.RequestBody);
            var response = await client.Client.ExecuteAsync(rq);
            var result = JsonConvert.DeserializeObject<string>(response.Content);
            if(result != "Ok")
            {
                throw new Exception(result);
            }
        }
    }
}
