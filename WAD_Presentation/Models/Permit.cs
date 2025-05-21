public class Permit
{
    public int PermitID { get; set; }
    public string? Type { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal Fee { get; set; }
    public int CitizenID { get; set; }
    public Citizen? Citizen { get; set; }
}
