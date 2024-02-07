using Microsoft.EntityFrameworkCore;
  
    public class UserActions
    {
        public OdataManager OdataManager { get; set; }= new();
        public AuditTrail AuditTrail { get; set; } = new();
        public User User { get; set; } = new();
        public IEnumerable<User> Users { get; set; }


        public async Task<OdataManager> CreateUser(ApplicationDbContext _db, User user)
    {
        if (user == null || _db==null)
        {

                return OdataManager;
        }
        else
            {
                OdataManager= await CheckUserName(user, _db);
                if (OdataManager.ResponseData.ToLower()==("valid"))
                {
                    User.Password = user.Password;
                    User.UserName = user.UserName;
                    await _db.Users.AddAsync(User);
                    await _db.SaveChangesAsync();
                    OdataManager.RequestConfirmation = true;
                    OdataManager.ResponseData = "ok";
                    var userFromDb = await GetUser(user, _db);
                    if (userFromDb != null)
                    {
                        await AddAuditTrail(_db, userFromDb);
                       
                    }
                }
                else
                {
                    OdataManager.RequestConfirmation = false;
                    OdataManager.ResponseData = "user already used.kindly select another user name";

                }
                return OdataManager;
            }
    }
        public async Task<IEnumerable<User>> AllUsers(ApplicationDbContext _db)
        {
            Users = await _db.Users.ToListAsync();
           
            return Users;
        }

        public async Task<OdataManager> CheckUserName(User user ,ApplicationDbContext _db)
        {
           var userFromDb = await _db.Users.Where(p=>p.UserName.ToLower()==user.UserName.ToLower()).FirstOrDefaultAsync();
            if (userFromDb == null)
            {
                OdataManager.ResponseData = "Valid";
                return OdataManager;
            }
            else
            {
                OdataManager.ResponseData = "Invalid";
                return OdataManager;
            }

        }
        public async Task<User> GetUser(User user, ApplicationDbContext _db)
        {
            User = await _db.Users.Where(p => p.UserName.ToLower() == user.UserName.ToLower()).FirstOrDefaultAsync();
            return User;        }

        public async Task<IEnumerable<AuditTrail>> AllAuditTrails(ApplicationDbContext _db)
        {
            var trails = new List<AuditTrail>();
            trails = await _db.AuditTrails.Include(p => p.User).ToListAsync();

            return trails;

        }
        public async Task AddAuditTrail(ApplicationDbContext _db, User user)
        {
            AuditTrail.User = user;
            
            await _db.AuditTrails.AddAsync(AuditTrail);
            await _db.SaveChangesAsync();
        }

            public  async Task UpdatePassword(ApplicationDbContext _db,PasswordChange change)
        {
            await _db.PasswordChanges.AddAsync(change);
            await _db.SaveChangesAsync();
            var user = await GetUser(User,_db);
            AuditTrail.User = user;
            AuditTrail.Reason = "Change Password";
            user.Password = change.NewPassword;
            await _db.SaveChangesAsync();

        }
        
}
