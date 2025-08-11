using Microsoft.EntityFrameworkCore;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class ScoreAdder : IScoreAdder
{
    private IScoreDbContext _scoreContext;
    private ILessonDbContext _lessonContext;
    public ScoreAdder(IUniversityDbContextFactory dbContextFactory)
    {
        _scoreContext = dbContextFactory.CreateScoreDbContext();
        _lessonContext = dbContextFactory.CreateLessonDbContext();
    }
    public void AddScores(List<ScoreItem> scoreItems)
    {
        var lessonMap = _lessonContext.Lessons
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
    }

    public void AddScore(Score score)
    {
        bool exists = _scoreContext.Scores.Any(s =>
            s.StudentNumber == score.StudentNumber &&
            s.LessonId == score.LessonId);

        if (!exists)
        {
            _scoreContext.Scores.Add(score);
        }
    }

}