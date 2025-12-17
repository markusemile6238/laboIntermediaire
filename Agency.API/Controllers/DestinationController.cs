using Agency.API.Dtos;
using Agency.API.Dtos.DestinationDtos;
using Agency.API.Mapping;
using Agency.BLL.Services;
using Agency.DAL.ExceptionDAL;
using Agency.DAL.Mapper;
using Agency.Domaine.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Agency.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly ILogger<DestinationController> _logger;

        public DestinationController(IDestinationService destinationService, ILogger<DestinationController> logger)
        {
            _destinationService = destinationService;
            _logger = logger;
        }



        #region GETAL L
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { Message = "Api connecté " });
        }


        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<EnumerableResult<DestinationList>>> GetAll()
        {
            try
            {
                var destinations = await _destinationService.GetAsync();
                return Ok(new
                {
                    Status = "Success",
                    Code = 200,
                    Message = "List des destinations",
                    Data = destinations
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

        #region GETBYID
        [HttpGet]
        [Route("{id?}")]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<ValueResponse<DestinationDetails>>> GetById(int id)
        {
            try
            {
                var res = await _destinationService.GetByIdAsyn(id);
                return Ok(new 
                {
                    Status = "Success",
                    Code=200,
                    Message = "Destination récupérée avec succès",
                    Data = res
                });
            }
            catch (Exception ex) {
                return NotFound(new {
                    Status = "Error",
                    Message = ex.Message
                });
            }


        }
        #endregion

        #region CREATE_POST

        [HttpPost]
        [Route("New")]
        [ProducesResponseType<ValueResponse<Destination>>(201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<ValueResponse<Destination>>> Create([FromBody] DestinationCreateNewOne value)
        {
            try
            {
                var destination = DestinationApiMapping.ToDestination(value);
                var res = await _destinationService.CreateAsync(destination);
                return Ok(new
                {
                    Status = "Success",
                    Code = 200,
                    Message = "Destination créer avec success",
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
