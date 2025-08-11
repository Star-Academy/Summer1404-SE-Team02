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

        _dbContextFactory.CreateStudentDbContext().SaveChanges();
    }
    public void AddStudent(Student student)
    {
        if (GetStudent(student.StudentNumber) != null)
        {
            _dbContextFactory.CreateStudentDbContext().Students.Add(student);
        }
    }

    public Student? GetStudent(int studentNumber)
    {
        return _dbContextFactory.CreateStudentDbContext().Students.Find(studentNumber);
    }
}