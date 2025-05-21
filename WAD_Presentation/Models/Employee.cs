public class Employee
{
    public int EmployeeID { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public int DepartmentID { get; set; }
    public Department? Department { get; set; }
}

