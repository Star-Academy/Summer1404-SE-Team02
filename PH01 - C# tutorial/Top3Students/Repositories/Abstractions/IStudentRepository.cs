namespace PH01___C__tutorial;

public interface IStudentRepository
{
    public void AddStudents(List<Student> students);
    public Student? GetStudent(int lessonName);
}