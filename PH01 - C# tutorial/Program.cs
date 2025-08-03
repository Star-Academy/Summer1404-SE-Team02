using System;
using System.Text.Json;
using System.IO;                
using System.Collections.Generic; 
using System.Linq;
using PH01___C__tutorial;

class Program
{
    static string readJsonFile(string filePath)
    {
        try
        {
            return File.ReadAllText(filePath);
        }
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"Error: {filePath} could not be opened. {ex.Message}");
            return string.Empty;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: An unexpected error occurred while reading {filePath}. {ex.Message}");
            return string.Empty;
        }
    }
    static void Main(string[] args)
    {
        var studentJson = readJsonFile("../../../student.json");
        var scoreJson = readJsonFile("../../../score.json");
        using var StContext = new StudentContext();
        var lessonAdder = new LessonAdder(StContext);
        var scoreAdder = new ScoreAdder(StContext);
        var studentAdder = new StudentAdder(StContext);

        var students = JsonSerializer.Deserialize<List<Student>>(studentJson)
        ?? new List<Student>();
        var scoreItems = JsonSerializer.Deserialize<List<ScoreItem>>(scoreJson)
        ?? new List<ScoreItem>();

        lessonAdder.AddLessons(LessonNameExtractor.ExtractName(scoreItems));
        scoreAdder.AddScores(scoreItems);
        studentAdder.AddStudents(students);

        var averageByStudent = scoreItems
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
            score => score.StudentNumber,
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
    }
}
