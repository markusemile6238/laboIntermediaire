using Agency.API.Dtos;
using Agency.API.Dtos.DestinationDtos;
using Agency.API.Mapping;
using Agency.BLL.Services;
using Agency.DAL.ExceptionDAL;
using Agency.Domaine.Entities;
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
                    Code = 200,
                    Message = "Destination récupérée avec succès",
                    Data = res
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    Status = "Error",
                    Message = ex.Message
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





        #region DELETE DESTINATION
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ValueResponse<ApiResponse>> DeleteDestionationAsync(int id)
        {

            ValueResponse<ApiResponse> res = new ValueResponse<ApiResponse>();
            if (id <= 0)
            {
                var response = new ApiResponse()
                {
                    Status = "Error",
                    StatusCode = 400,
                    Message = $"Bad request with id:{id}"
                };
                res.Value = response;
                return res;
            }
            ;

            try
            {
                int isDeleted = await _destinationService.DeleteAsynch(id);
                if (isDeleted == 1)
                {
                    var response = new ApiResponse()
                    {
                        Status = "Success",
                        StatusCode = 200,
                        Message = "Destionation supprimer avec succes"
                    };
                    res.Value = response;
                    return res;
                }
                else
                {
                    var response = new ApiResponse()
                    {
                        Status = "Error",
                        StatusCode = 500,
                        Message = $"Erreur inconnue !?"
                    };
                    res.Value = response;
                    return res;
                }



            }
            catch (Exception ex)
            {
                var response = new ApiResponse()
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = ex.Message
                };
                res.Value = response;
                return res;
            }
        } 
        #endregion

    }

}


