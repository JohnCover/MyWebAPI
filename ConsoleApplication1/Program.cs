using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49830/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Student[]>();
                    readTask.Wait();

                    var students = readTask.Result;

                    foreach (var student in students)
                    {
                        Console.WriteLine(student.Name);
                    }
                }

                //HTTP POST
                var student = new Student() { Name = "Steve" };

                var postTask = client.PostAsJsonAsync<Student>("student", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    var insertedStudent = readTask.Result;

                    Console.WriteLine("Student {0} inserted with id: {1}", insertedStudent.Name, insertedStudent.Id);
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }

                //HTTP PUT
            }

        }
            Console.ReadLine();
        }
    }
}