using PH01___C__tutorial.UniversityContexts;

namespace PH01___C__tutorial;

public class StudentAdder : IStudentAdder
{
    private IStudentDbContext _stContext;

    public StudentAdder(IUniversityDbContextFactory dbContextFactory)
    {
        _stContext = dbContextFactory.CreateStudentDbContext();
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
        bool exists = _stContext.Students.Any(s => s.StudentNumber == student.StudentNumber);

        if (!exists)
        {
            _stContext.Students.Add(student);
        }
    }
}