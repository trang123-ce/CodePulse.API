namespace CodePulse.API.Models.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
