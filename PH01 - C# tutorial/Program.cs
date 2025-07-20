using System;
using System.Text.Json;
using System.IO;                
using System.Collections.Generic; 
using System.Linq;               


namespace HelloWorld
{
    public class Student
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int StudentNumber {get; set;}
    }

    public class ScoreItem
    {
        public int StudentNumber {get; set;}
        public string Lesson {get; set;}
        public double Score {get; set;}
    }

    class Program
    {
        static void Main(string[] args)
        {
            string json1, json2;
            try
            {
                json1 = File.ReadAllText("student.json");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: student.json could not be opened. {ex.Message}");
                return;
            }
            try
            {
                json2 = File.ReadAllText("score.json");
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine($"Error: score.json could not be opened. {ex.Message}");
                return;
            }
            var students = JsonSerializer.Deserialize<List<Student>>(json1)
            ?? new List<Student>();
            var scores = JsonSerializer.Deserialize<List<ScoreItem>>(json2)
            ?? new List<ScoreItem>();

            var averageByStudent = scores
            .GroupBy(item => item.StudentNumber)                      
            .Select(g => new                                     
            {
                StudentNumber = g.Key,                            
                AverageScore = g.Average(item => item.Score)     
            })
            .OrderByDescending(item => item.AverageScore)
            .Take(3);


            var studentScores = students
            .Join(
                averageByStudent,                         
                student => student.StudentNumber,  
                score   => score.StudentNumber,    
                (student, score) => new             
                {
                    student.FirstName,
                    student.LastName,
                    score.AverageScore
                }
            )
            .OrderByDescending(item => item.AverageScore);
            var list = studentScores.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var entry = list[i];
                Console.WriteLine($"{entry.FirstName} {entry.LastName}: {entry.AverageScore:F5}");
            }
            // var serializedItem = JsonSerializer.Serialize(studentScores);
            // Console.WriteLine(serializedItem);
        }
    }
}