namespace Service
{
    public interface IRestClient
    {
        RestSharp.RestClient Client { get; }
    }
}