namespace PH01___C__tutorial;

public interface ILessonRepository
{
    public void AddLessons(List<string> lessonNames);
    public Lesson? GetLesson(string lessonName);
}