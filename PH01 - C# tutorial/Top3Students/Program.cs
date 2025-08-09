using System;
using System.Text.Json;
using System.IO;                
using System.Collections.Generic; 
using System.Linq;
using PH01___C__tutorial;
using PH01___C__tutorial.DTO;

class Program
{
    static void Main(string[] args)
    {
        using var stContext = new StudentContext();
        var lessonAdder = new LessonAdder(stContext);
        var scoreAdder = new ScoreAdder(stContext);
        var studentAdder = new StudentAdder(stContext);
        var averageByStudent = new AverageCalculator();
        var studentFinder = new StudentFinder(stContext);
        var top3Students = averageByStudent.CalculateAverageTop3(stContext);
        
        for (int i = 0; i < top3Students.Count; i++)
        {
            var entry = top3Students[i];
            Console.WriteLine($"{studentFinder.FindStudentById(entry.StudentNumber).FirstName}" +
                              $" {studentFinder.FindStudentById(entry.StudentNumber).LastName}:" +
                              $" {entry.AverageScore:F5}");
        }
    }
}
