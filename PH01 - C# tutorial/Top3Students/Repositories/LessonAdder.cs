using Microsoft.EntityFrameworkCore;

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
        foreach (var lesson in lessons)
        {
            AddLesson(lesson);
        }
        _stContext.SaveChanges();
    }

    public void AddLesson(Lesson lesson)
    {
        bool exists = _stContext.Lessons.Any(l => l.LessonName == lesson.LessonName);

        if (!exists)
        {
            _stContext.Lessons.Add(lesson);
        }
    }
}