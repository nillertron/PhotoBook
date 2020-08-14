using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Service
{
    public class RestClient : IRestClient
    {
        public RestSharp.RestClient Client { get; private set; }
        public RestClient()
        {
            Client = new RestSharp.RestClient("http://photobook.nillertron.com/");
            //Client = new RestSharp.RestClient("http://localhost:44333/");

        }
    }
}
