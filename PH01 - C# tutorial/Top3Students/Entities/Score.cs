public class Score
{
    public int StudentNumber {get; set;}
    public int LessonId {get; set; }
    public double Grade {get; set;}
    public Student Student { get; set; }
    public Lesson Lesson { get; set; }
}