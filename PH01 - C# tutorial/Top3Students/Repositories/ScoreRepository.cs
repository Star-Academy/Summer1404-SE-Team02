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
        var uniLessonContext = _universityDbContextFactory.CreateLessonDbContext();
        var lessonMap = uniLessonContext.Lessons
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
        var scoreDbContext = _universityDbContextFactory.CreateScoreDbContext();
        if (GetScore(score.StudentNumber, score.LessonId) != null)
        {
            scoreDbContext.Scores.Add(score);
            scoreDbContext.SaveChanges();
        }
    }

    public Score? GetScore(int studentNumber, int lessonId)
    {
        return _universityDbContextFactory.CreateScoreDbContext().Scores.Find(studentNumber, lessonId);
    }
}