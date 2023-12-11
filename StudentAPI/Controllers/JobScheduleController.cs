

using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class JobScheduleController : Controller
    {

        [HttpPost]
        [Route("CreateBackgroundJob")]
        public IActionResult CreateScheduledJob()
        {
            var scheduleDateTime = DateTime.Now.AddSeconds(3);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Background job executed."));
            return Ok("readu");
        }

        [HttpGet]
        [Route("GetAllStudentsJob")]
        public async Task<IActionResult> GetAllStudentsJob()
        {
            RecurringJob.AddOrUpdate<StudentController>("job", x => x.GetAll(), "28 2 * * *");
            return Ok();
        }
    }
}
