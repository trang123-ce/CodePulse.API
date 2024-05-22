namespace CodePulse.API.Models.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public Transaction? Transaction { get; set; }
    }
}
