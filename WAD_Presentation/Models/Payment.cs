public class Payment
{
    public int PaymentID { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int PermitID { get; set; }
    public Permit? Permit  { get; set; }
}
