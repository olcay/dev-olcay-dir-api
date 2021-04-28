using WebApi.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/enums")]
    [ResponseCache(CacheProfileName = "240SecondsCacheProfile")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    [HttpCacheValidation(MustRevalidate = true)]
    [Produces("application/json")]
    public class EnumsController : ControllerBase
    {
        [HttpGet("petTypes")]
        public ActionResult<IEnumerable<EnumDto>> GetPetTypes()
        {
            return Ok(EnumService.GetPetTypes());
        }

        [HttpGet("genders")]
        public ActionResult<IEnumerable<EnumDto>> GetGenders()
        {
            return Ok(EnumService.GetGenders());
        }

        [HttpGet("ages")]
        public ActionResult<IEnumerable<EnumDto>> GetAges()
        {
            return Ok(EnumService.GetAges());
        }

        [HttpGet("sizes")]
        public ActionResult<IEnumerable<EnumDto>> GetSizes()
        {
            return Ok(EnumService.GetSizes());
        }

        [HttpGet("fromWhere")]
        public ActionResult<IEnumerable<EnumDto>> GetFromWhere()
        {
            return Ok(EnumService.GetFromWhere());
        }

        [HttpGet("cities")]
        public ActionResult<IEnumerable<EnumDto>> GetCities()
        {
            return Ok(EnumService.GetCities());
        }
    }
}