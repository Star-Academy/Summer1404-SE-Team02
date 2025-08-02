using System;
using System.Text.Json;
using System.IO;                
using System.Collections.Generic; 
using System.Linq;               

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
        var studentJson = readJsonFile("student.json");
        var scoreJson = readJsonFile("score.json");
        using var StContext = new StudentContext();

        var students = JsonSerializer.Deserialize<List<Student>>(studentJson)
        ?? new List<Student>();
        var scoreItems = JsonSerializer.Deserialize<List<ScoreItem>>(scoreJson)
        ?? new List<ScoreItem>();
        
        // Extract distinct lesson names
        var lessonNames = scoreItems
            .Select(si => si.Lesson.Trim())
            .Distinct()
            .ToList();

        // Create Lesson entities
        var lessons = lessonNames.Select(name => new Lesson
        {
            LessonName = name
        }).ToList();


        // Save lessons to generate LessonID
        StContext.Lessons.AddRange(lessons);
        StContext.SaveChanges(); // LessonID is auto-generated here

        // Build map: LessonName → LessonID
        var lessonMap = StContext.Lessons
            .ToDictionary(l => l.LessonName, l => l.LessonID);

        // Convert ScoreItem to Score
        var scores = scoreItems.Select(si => new Score
        {
            StudentNumber = si.StudentNumber,
            LessonID = lessonMap[si.Lesson.Trim()],
            Grade = si.Score
        }).ToList();

        // Save students and scores
        StContext.Students.AddRange(students);
        StContext.Scores.AddRange(scores);
        StContext.SaveChanges();

        var averageByStudent = scores
        .GroupBy(item => item.StudentNumber)
        .Select(g => new
        {
            StudentNumber = g.Key,
            AverageScore = g.Average(item => item.Grade)
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
