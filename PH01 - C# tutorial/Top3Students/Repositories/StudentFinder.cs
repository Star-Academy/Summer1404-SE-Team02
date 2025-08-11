using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class StudentFinder : IStudentFinder
{
    IStudentDbContext _studentContext;
    public StudentFinder(IUniversityDbContextFactory dbContextFactory)
    {
        _studentContext = dbContextFactory.CreateStudentDbContext();
    }
    public Student FindStudentById(int studentId)
    {
        return _studentContext.Students.Find(studentId);
    }
}