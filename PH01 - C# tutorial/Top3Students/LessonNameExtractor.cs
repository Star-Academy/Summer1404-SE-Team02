namespace PH01___C__tutorial;

public class LessonNameExtractor
{
    public static List<string> ExtractName(List<ScoreItem> scoreItems)
    {
        return scoreItems
            .Select(si => si.Lesson.Trim())
            .Distinct()
            .ToList();
    }
}