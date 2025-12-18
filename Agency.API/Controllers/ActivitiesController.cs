using Agency.API.Dtos;
using Agency.API.Dtos.ActivityDtos;
using Agency.API.Dtos.DestinationDtos;
using Agency.API.Mapping;
using Agency.BLL.Services;
using Agency.DAL.ExceptionDAL;
using Agency.DAL.Mapper;
using Agency.Domaine.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agency.API.Controllers
{
    [ApiController]
    [Route("api/Activity")]
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IActivityService activityService, ILogger<ActivitiesController> logger)
        {
            _activityService = activityService;
            _logger = logger;
        }




        #region GETALLASYNCH
        [HttpGet]
        [ProducesResponseType(typeof(ProblemDetails), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<EnumerableResult<Activity>>> GetAllAsynch()
        {
            try
            {
                var activities = await _activityService.GetAsync();
                return Ok(new
                {
                    Status = "Success",
                    Code = 200,
                    Message = "List des Activitées",
                    Data = activities
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Code = ex.GetType().Name,
                    Message = ex.Message,
                    Details = ex.StackTrace
                });
            }

        }
        #endregion

        #region CREATE_POST
        [HttpPost]
        [ProducesResponseType<ValueResponse<Destination>>(201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<ValueResponse<Activity>>> Create([FromBody] ActivityCreateDtos value)
        {
            try
            {
                var activity = ActivityApiMapping.ToActivity(value);
                var res = await _activityService.CreateAsync(activity);
                return Ok(new
                {
                    Status = "Success",
                    Code = 200,
                    Message = "Activité créer avec success",
                    Data = res
                });
            }
            catch (ArgumentException ex)
            {
                // Erreur de validation simple
                return BadRequest(new
                {
                    status = "Error",
                    code = 400,
                    message = ex.Message
                });
            }
            catch (DuplicateEntryException ex) when (ex.Message.Contains("existe déjà"))
            {
                // Destination déjà existante
                return Conflict(new
                {
                    status = "Error",
                    code = 409,
                    message = "Duplicate key",
                    details = ex.Message
                });
            }

        }

        #endregion

    }
}
