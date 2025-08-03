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
        _stContext.Students.AddRange(students);
        _stContext.SaveChanges();
    }
}