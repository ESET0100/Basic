using DbWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ApplicationDbContext context;
        public WeatherForecastController(ApplicationDbContext context)
        {
            context = context;
        }
    }
}
