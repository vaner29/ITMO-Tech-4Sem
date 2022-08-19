namespace Client;

using Client;

public class Runner
{
    public static void Main()
    {
        var client = new Controller();
        client.createNewStudent("Deodat", 1);
        client.createNewStudent("Gayson", 2);

        List<Student> results = client.getStudents().Result;
        foreach (var student in results)
        {
            Console.WriteLine(student.id + ": " + student.name);
        }
        
        Console.WriteLine("client with Id 2:" + client.getStudentById(2).Result.id + ": " + client.getStudentById(2).Result.name);
        
    }
    
}
