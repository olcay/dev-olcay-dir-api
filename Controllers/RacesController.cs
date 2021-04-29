using AutoMapper;
using WebApi.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Persistence.Services;
using WebApi.Enums;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/races")]
    [ResponseCache(CacheProfileName = "240SecondsCacheProfile")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    [HttpCacheValidation(MustRevalidate = true)]
    [Produces("application/json")]
    public class RacesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public RacesController(IRepository repository,
            IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{petTypeValue}", Name = "GetRacesForPetType")]
        public ActionResult<IEnumerable<RaceDto>> GetRacesForPetType(PetType petTypeValue)
        {
            var races = _repository.GetRaces(petTypeValue);
            return Ok(_mapper.Map<IEnumerable<RaceDto>>(races));
        }
    }
}