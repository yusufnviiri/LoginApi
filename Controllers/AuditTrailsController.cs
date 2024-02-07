using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


    [Authorize(Policy = "CustomPolicy")]

    [Route("api/[controller]")]
    [ApiController]
    public class AuditTrailsController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        private readonly UserActions _userActions = new();
        public AuditTrailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "AuditTrails")]
        public async Task<IActionResult> AuditTrails()
        {
            var users = await _userActions.AllAuditTrails(_db);
            if (users.Any())
            {
                return Ok(users);
            }
            else
            {
                return Ok("No data found");
            }

        }


    }

