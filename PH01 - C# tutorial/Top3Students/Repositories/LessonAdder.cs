using Microsoft.EntityFrameworkCore;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class LessonAdder : ILessonAdder
{
    private ILessonDbContext _lessonDbContext;
    public LessonAdder(IUniversityDbContextFactory dbContextFactory)
    {
        _lessonDbContext = dbContextFactory.CreateLessonDbContext();
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
        // _lessonDbContext.SaveChanges();
    }

    public void AddLesson(Lesson lesson)
    {
        bool exists = _lessonDbContext.Lessons.Any(l => l.LessonName == lesson.LessonName);

        if (!exists)
        {
            _lessonDbContext.Lessons.Add(lesson);
        }
    }
}