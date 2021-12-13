using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Services.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DeviceManager.Api.Common;
using System.Linq;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// Base api controller all api controllers should inherit from this controller
    /// </summary>
    /// <typeparam name="TViewModel">Type of the view model</typeparam>
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [Authorize]
    public class BaseController<TViewModel> : Controller where TViewModel : class
    {
        private IGenericService<TViewModel> GenericService { get; set; }

        /// <summary>
        /// TODO: Pass base validation service and common CRUD operation in the base controller
        /// </summary>
        public BaseController(IGenericService<TViewModel> genericService)
        {
            GenericService = genericService;
        }

        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [ValidateActionParameters]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await GenericService.GetAll());
        }

        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByAny/{value}")]
        [ValidateActionParameters]
        public async Task<IActionResult> GetByAny([FromRoute][Required]int value)
        {
            return new OkObjectResult(await GenericService.GetByAny(value));
        }

        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllWithData")]
        [ValidateActionParameters]
        public async Task<IActionResult> GetAllWithData(int? id)
        {
            if (id == null)
            {
                var LoggedInUserId = User.Claims.Where(x => x.Type == ClaimsConstants.fkEmpId).FirstOrDefault().Value;
                id = int.Parse(LoggedInUserId);
            }
            return new OkObjectResult(await GenericService.GetAllWithData(id.Value));
        }

        /// <summary>
        /// Gets teh paged result for the entity requested
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetPaged")]
        [ValidateActionParameters]
        public async Task<IActionResult> Get([FromQuery, Required]int page, [FromQuery, Required]int pageSize)
        {
            return new OkObjectResult(await GenericService.GetAll(page, pageSize));
        }

        /// <summary>
        /// Gets the entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ValidateActionParameters]
        public async Task<IActionResult> GetById([FromRoute][Required]int id)
        {
            return new ObjectResult(await GenericService.GetById(id));
        }

        /// <summary>
        /// Inserts the entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateActionParameters]
        public IActionResult Post([FromBody]TViewModel model)
        {
            var entity = GenericService.Create(model);
            return new OkObjectResult(entity);
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateActionParameters]
        public IActionResult Put([FromRoute]int id, [FromBody]TViewModel model)
        {
            GenericService.Update(id, model);
            return new OkResult();
        }

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateActionParameters]
        public IActionResult Delete([FromRoute]int id)
        {
            GenericService.Delete(id);
            return new OkResult();
        }

    }
}