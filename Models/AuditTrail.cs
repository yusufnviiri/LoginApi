
    public class AuditTrail
    {
        public  int AuditTrailId { get; set; }
        public User User { get; set; }
        public string? Reason { get; set; } = "Login";
        public DateTime LogInDate { get; set; }= DateTime.Now;

    
}
