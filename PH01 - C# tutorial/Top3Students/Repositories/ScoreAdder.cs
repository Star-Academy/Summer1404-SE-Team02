using Microsoft.EntityFrameworkCore;

namespace PH01___C__tutorial;

public class ScoreAdder : IScoreAdder
{
    private StudentContext _stContext;
    public ScoreAdder(StudentContext stContext)
    {
        _stContext = stContext;
    }
    public void AddScores(List<ScoreItem> scoreItems)
    {
        var lessonMap = _stContext.Lessons
            .ToDictionary(l => l.LessonName, l => l.LessonId);
        
        var scores = scoreItems.Select(si => new Score
        {
            StudentNumber = si.StudentNumber,
            LessonId = lessonMap[si.Lesson.Trim()],
            Grade = si.Score
        }).ToList();
        foreach (var score in scores)
        {
            AddScore(score);
        }
        _stContext.SaveChanges();
    }

    public void AddScore(Score score)
    {
        bool exists = _stContext.Scores.Any(s =>
            s.StudentNumber == score.StudentNumber &&
            s.LessonId == score.LessonId);

        if (!exists)
        {
            _stContext.Scores.Add(score);
        }
    }

}