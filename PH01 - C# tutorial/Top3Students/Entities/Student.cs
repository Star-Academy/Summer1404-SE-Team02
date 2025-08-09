using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public int StudentNumber {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public ICollection<Score> Scores { get; set; }
}