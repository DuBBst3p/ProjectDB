public class Complaint
{
    public int ComplaintID { get; set; }
    public string? Description { get; set; }
    public DateTime DateFiled { get; set; }
    public int CitizenID { get; set; }
    public Citizen? Citizen { get; set; }
}
