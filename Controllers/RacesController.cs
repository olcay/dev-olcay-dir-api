using AutoMapper;
using WebApi.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Enums;
using WebApi.Persistence;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RacesController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{petTypeValue}", Name = "GetRacesForPetType")]
        public ActionResult<IEnumerable<RaceDto>> GetRacesForPetType(PetType petTypeValue)
        {
            var races = _unitOfWork.Races.Get(petTypeValue);
            return Ok(_mapper.Map<IEnumerable<RaceDto>>(races));
        }
    }
}