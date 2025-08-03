namespace PH01___C__tutorial;

public class LessonAdder : ILessonAdder
{
    private StudentContext _stContext;
    public LessonAdder(StudentContext stContext)
    {
        _stContext = stContext;
    }
    public void AddLessons(List<string> lessonNames)
    {
        var lessons = lessonNames.Select(name => new Lesson
        {
            LessonName = name
        }).ToList();
        _stContext.Lessons.AddRange(lessons);
        _stContext.SaveChanges();
    }
}