namespace PH01___C__tutorial;

public class StudentFinder : IStudentFinder
{
    StudentContext _stContext;
    public StudentFinder(StudentContext stcontex)
    {
        _stContext = stcontex;
    }
    public Student FindStudentById(int studentId)
    {
        return _stContext.Students.Find(studentId);
    }
}