using System.Net.Http.Json;
using System.Text.Json;

namespace Client;
public class Controller
{
    private static HttpClient _client = new HttpClient();
    public async Task<List<Student>> getStudents()
    {
        {
            var content = JsonContent.Create("");
            var response = await _client.GetAsync($"http://localhost:8080/students");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Student>>(responseString);
        }
    }

    public async Task<Student> getStudentById(int id)
    {
        {
            var content = JsonContent.Create("");
            var response = await _client.GetAsync($"http://localhost:8080/students/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Student>(responseString);
        }
    }

    public async Task<Student> createNewStudent(string name, int id)
    {
        {
            var content = JsonContent.Create("");
            var response = await _client.PostAsync($"http://localhost:8080/createStudent?name={name}&id={id}", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Student>(responseString);
        }
    }

    public async Task<Student> addNewStudent(Student student)
    {
        {
            var content = JsonContent.Create(student);
            var response = await _client.PostAsync($"http://localhost:8080/addStudent", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Student>(responseString);
        }
    }
}