namespace PH01___C__tutorial;

public class StudentAdder : IStudentAdder
{
    private StudentContext _stContext;

    public StudentAdder(StudentContext stContext)
    {
        _stContext = stContext;
    }
    
    public void AddStudents(List<Student> students)
    {
        foreach (var student in students)
        {
            AddStudent(student);
        }
        _stContext.SaveChanges();
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