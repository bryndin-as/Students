using Newtonsoft.Json;
using StudentService.Client.Core.Utils;
using StudentService.DAL.DTO;
using System.Text;

namespace StudentService.Client.Core
{
    public class DataService(string host) : IDataService
    {
        readonly HttpClient _client = HttpWebClient.GetInstance(host); 

        const string uriStudents = "api/students";
        const string uriSubjects = "api/subjects";
        const string uriStudentsGrades = "api/students-grades"; 
        const string uriSeedData = "api/seed-data";

        public async Task<IEnumerable<StudentBaseDTO>> GetStudentsAsync()
        {
            return await GetDataAsync<StudentBaseDTO>(uriStudents);
        }

        public async Task<IEnumerable<SubjectBaseDTO>> GetSubjectsAsync()
        {
            return await GetDataAsync<SubjectBaseDTO>(uriSubjects);
        }

        public async Task<IEnumerable<StudentWithGradesDTO>> GetStudentWithGradesAsync()
        {
            return await GetDataAsync<StudentWithGradesDTO>(uriStudentsGrades);
        }

        public async Task<int> CreateStudentAsync(StudentCreateDTO item)
        {
            return await PostDataAsync(uriStudents, item);
        }

        public async Task<int> CreateSubjectAsync(SubjectCreateDTO item)
        {
            return await PostDataAsync(uriSubjects, item);
        }

        public async Task AddSeedTest(int count)
        {
            var address = Path.Combine(host, uriSeedData).Replace("\\", "/");
            var jsonData = JsonConvert.SerializeObject(new { Count = count });
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(address, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Логирование
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private async Task<IEnumerable<T>> GetDataAsync<T>(string uri)
        {
            var address = Path.Combine(host, uri).Replace("\\", "/");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, address);
            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(result) ?? Enumerable.Empty<T>();
            }

            return Enumerable.Empty<T>();
        }

        // Общий метод для выполнения POST-запросов
        private async Task<int> PostDataAsync<T>(string uri, T item)
        {
            var address = Path.Combine(host, uri).Replace("\\", "/");
            var jsonData = JsonConvert.SerializeObject(item);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, address)
            {
                Content = httpContent
            };

            var response = await _client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(result);
            }

            return 0;
        }
    }
}