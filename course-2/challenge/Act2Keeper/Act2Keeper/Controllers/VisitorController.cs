using Act2Keeper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Act2Keeper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitorController : ControllerBase
    {
        private readonly AppDbContext _db;
        public VisitorController(AppDbContext db) {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateVisitor([FromBody] Visitor visitor)
        {
            visitor.LevelOfTrust = 0;
            _db.Visitors.Add(visitor);
            _db.SaveChanges();
            return Created();
        }

        [HttpGet]
        public IActionResult GetAllVisitors()
        {
            var visitors = _db.Visitors.ToList();
            return Ok(visitors);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetVisitorById(int id)
        {
            var visitor = _db.Visitors.FirstOrDefault(v => v.Id == id);
            if (visitor is null) return NotFound();
            return Ok(visitor);
        }

        [HttpPost("{id:int}/actions")]
        public IActionResult DoAction(int id, [FromBody] ActionRequest request)
        {
            var visitor = _db.Visitors.Include(v => v.History).FirstOrDefault(v => v.Id == id);
            if (visitor is null) return NotFound();

            bool isDone = visitor.History.Any(h => h.Action == request.Action);
            if (isDone)
                return BadRequest("Действие уже выполнено!");

            int points = 0;
            if (request.Action == ActionType.Introduce) points = 20;
            if (request.Action == ActionType.OfferHelp) points = 30;
            if (request.Action == ActionType.Threaten) points = -20;

            int newTrust = visitor.LevelOfTrust + points;
            if (newTrust < 0) newTrust = 0;
            else if (newTrust > 100) newTrust = 100;
            visitor.LevelOfTrust = newTrust;

            var history = new VisitorAtionsHistory
            {
                VisitorId = visitor.Id,
                Action = request.Action
            };

            _db.Histories.Add(history);
            _db.SaveChanges();

            return Ok("Действие выполнено!");
        }

        [HttpGet("{id:int}/access")]
        public IActionResult Check([FromRoute] int id)
        {
            var visitor = _db.Visitors.FirstOrDefault(v => v.Id == id);
            if (visitor is null) return NotFound();

            if (visitor.LevelOfTrust >= 50)
                return Ok("Хранитель разрешает вам пройти!");
            return Ok("Хранитель пока не доверяет вам");
        }
    }
}
