public class Lesson
{
    public int LessonId {get; set;}
    public string LessonName {get; set;}
    public ICollection<Score> Scores { get; set; }
}