using Microsoft.EntityFrameworkCore;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class ScoreRepository : IScoreRepository
{
    private readonly IUniversityDbContextFactory _universityDbContextFactory;
    public ScoreRepository(IUniversityDbContextFactory dbContextFactory)
    {
        _universityDbContextFactory = dbContextFactory;
    }
    public void AddScores(List<ScoreItem> scoreItems)
    {
        var _uniLessonContext = _universityDbContextFactory.CreateLessonDbContext();
        var lessonMap = _uniLessonContext.Lessons
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

        _universityDbContextFactory.CreateScoreDbContext().SaveChanges();
    }

    public void AddScore(Score score)
    {

        if (GetScore(score.StudentNumber, score.LessonId) != null)
        {
            _universityDbContextFactory.CreateScoreDbContext().Scores.Add(score);
        }
    }

    public Score? GetScore(int studentNumber, int lessonId)
    {
        return _universityDbContextFactory.CreateScoreDbContext().Scores.Find(studentNumber, lessonId);
    }
}