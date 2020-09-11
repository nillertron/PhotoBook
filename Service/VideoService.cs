using DataAcces.Specific;
using Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VideoService : IVideoService
    {
        private IRestClient client;
        private VideoRepo repo;
        private ILoginStateService loginState;
        public VideoService(IRestClient client, VideoRepo repo, ILoginStateService loginState)
        {
            this.client = client;
            this.repo = repo;
            this.loginState = loginState;
        }
        public async Task<string> UploadVideo(string location, string beskrivelse, int albumdId)
        {
            client.Client.ReadWriteTimeout = int.MaxValue;
            client.Client.Timeout = int.MaxValue;
            var video = new PB_Video { Beskrivelse = beskrivelse, PB_FotoalbumId = albumdId, Url = $"http://photobook.nillertron.com/video/", OprettetDato = DateTime.Now };

            await Task.Run(async () =>
            {
                var rq = new RestRequest("api/Video/upload", Method.POST);
                rq.AddFile("file", location);
                rq.AddHeader("Content-Type", "multipart/form-data");

                var response = await client.Client.ExecuteAsync(rq);

                var status = JsonConvert.DeserializeObject<string>(response.Content);
                if (status == "File null")
                {
                    throw new Exception("No file");
                }
                video.Url += status;
            });
            var created = await SaveVideoToDb(video);
            if (created == "Created")
                return "Created";
            else
                return "Error";
        }

        public async Task<string> SaveVideoToDb(PB_Video video)
        {
            string status = string.Empty;
            await Task.Run(async () =>
            {
                var rq = new RestRequest("api/Video/savedb", Method.POST);
                var serialiseretVideo = JsonConvert.SerializeObject(video);
                rq.AddParameter("Application/json; charset=utf-8", serialiseretVideo, ParameterType.RequestBody);
                var response = await client.Client.ExecuteAsync(rq);
                status = JsonConvert.DeserializeObject<string>(response.Content);
            });

            if (status == "Ok")
            {
                loginState.user.Fotoalbum[video.PB_FotoalbumId].Videos.Insert(0, video);
                return "Created";

            }
            else
                return status;

        }
        public async Task<List<PB_Video>> GetUserVideosFromFotoAlbumId(int id)
        {
            return await repo.GetUserVideoFromAlbumId(id);
        }
        public async Task DeleteVideo(PB_Video video)
        {
            var rq = new RestRequest("api/Video/Delete", Method.POST);
            var s = JsonConvert.SerializeObject(video);
            rq.AddParameter("application/json; charset=utf-8", s, ParameterType.RequestBody);
            var response = await client.Client.ExecuteAsync(rq);
            var result = JsonConvert.DeserializeObject<string>(response.Content);
            if (result != "Ok")
            {
                throw new Exception(result);
            }

        }
        public async Task EditVideo(PB_Video video)
        {
            var rq = new RestRequest("api/Video/Delete", Method.POST);
            var s = JsonConvert.SerializeObject(video);
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
