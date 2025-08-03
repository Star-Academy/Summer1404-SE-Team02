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
        _stContext.Scores.AddRange(scores);
        _stContext.SaveChanges();
    }
}