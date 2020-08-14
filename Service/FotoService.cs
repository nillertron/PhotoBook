using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FotoService : IFotoService
    {
        private IRestClient client;
        public FotoService(IRestClient c)
        {
            client = c;
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
            var obj = new PB_Foto { Url = "http://photobook.nillertron.com/images/" + url, Beskrivelse = beskrivelse, PB_FotoalbumId = albumId };
            var request = new RestRequest("api/Foto/UploadToDb", Method.POST);
            request.AddParameter("application/json; charset=utf-8", obj, ParameterType.RequestBody);
            var response = await client.Client.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<string>(response.Content);
           

        }
    }
}
