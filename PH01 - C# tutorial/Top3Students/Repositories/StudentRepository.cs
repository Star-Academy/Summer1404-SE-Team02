using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class StudentRepository : IStudentRepository
{
    private IUniversityDbContextFactory _dbContextFactory;

    public StudentRepository(IUniversityDbContextFactory  dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public void AddStudents(List<Student> students)
    {
        foreach (var student in students)
        {
            AddStudent(student);
        }
    }
    public void AddStudent(Student student)
    {
        var studentDbContext = _dbContextFactory.CreateStudentDbContext();
        if (GetStudent(student.StudentNumber) != null)
        {
            studentDbContext.Students.Add(student);
            studentDbContext.SaveChanges();
        }
    }

    public Student? GetStudent(int studentNumber)
    {
        return _dbContextFactory.CreateStudentDbContext().Students.Find(studentNumber);
    }
}