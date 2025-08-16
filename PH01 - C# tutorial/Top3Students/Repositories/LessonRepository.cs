using Microsoft.EntityFrameworkCore;
using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class LessonRepository : ILessonRepository
{
    private IUniversityDbContextFactory _dbContextFactory;
    public LessonRepository(IUniversityDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
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
    }

    public void AddLesson(Lesson lesson)
    {
        var lessonDbContext = _dbContextFactory.CreateLessonDbContext();
        if (GetLesson(lesson.LessonName) != null)
        {
            lessonDbContext.Lessons.Add(lesson);
            lessonDbContext.SaveChanges();
        }
    }

    public Lesson? GetLesson(string lessonName)
    {
        return _dbContextFactory.CreateLessonDbContext().Lessons.Find(lessonName);
    }
}