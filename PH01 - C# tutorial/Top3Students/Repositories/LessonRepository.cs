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

        _dbContextFactory.CreateLessonDbContext().SaveChanges();
    }

    public void AddLesson(Lesson lesson)
    {
        if (GetLesson(lesson.LessonName) != null)
        {
            _dbContextFactory.CreateLessonDbContext().Lessons.Add(lesson);
        }
    }

    public Lesson? GetLesson(string lessonName)
    {
        return _dbContextFactory.CreateLessonDbContext().Lessons.Find(lessonName);
    }
}