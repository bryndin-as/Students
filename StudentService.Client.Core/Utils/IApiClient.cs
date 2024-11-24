namespace StudentService.Client.Core.Utils
{
    public interface IApiClient
    {
        Task<IEnumerable<T>> GetDataAsync<T>(string uri);
        Task<int> PostDataAsync<T>(string uri, T item);
    }
}
