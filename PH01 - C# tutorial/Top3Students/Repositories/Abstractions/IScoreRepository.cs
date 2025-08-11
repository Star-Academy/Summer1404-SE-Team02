namespace PH01___C__tutorial;

public interface IScoreRepository
{
    public void AddScores(List<ScoreItem> scoreItems);
    public Score?  GetScore(int  studentNumber, int lessonId);
}