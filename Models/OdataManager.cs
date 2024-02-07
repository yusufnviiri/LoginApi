
    public class OdataManager
    {
        public User User { get; set; } = new();
        public int UserId { get;set; }
        public string ResponseData { get; set;}
        public bool RequestConfirmation { get; set; }=false;
        public IEnumerable<User> Users { get; set; }

    
}
