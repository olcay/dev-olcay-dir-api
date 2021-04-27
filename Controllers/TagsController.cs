using AutoMapper;
using WebApi.Models;
using WebApi.Services;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/items/{itemId}/tags")]
    [ResponseCache(CacheProfileName = "240SecondsCacheProfile")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    [HttpCacheValidation(MustRevalidate = true)]
    [Produces("application/json")]
    public class TagsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TagsController(IRepository repository,
            IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetTagsForItem")]
        public ActionResult<IEnumerable<TagDto>> GetTagsForItem(Guid itemId)
        {
            if (!_repository.ItemExists(itemId))
            {
                return NotFound();
            }

            var tagsForItemFromRepo = _repository.GetTags(itemId);
            return Ok(_mapper.Map<IEnumerable<TagDto>>(tagsForItemFromRepo));
        }

        [HttpPost(Name = "CreateTagForItem")]
        public ActionResult<TagDto> CreateTagForItem(
            Guid itemId, TagForCreationDto tag)
        {
            if (!_repository.ItemExists(itemId))
            {
                return NotFound();
            }

            var tagEntity = _mapper.Map<Entities.Tag>(tag);

            var tagFromRepo = _repository.GetTag(tagEntity.Name);

            _repository.AddItemTag(itemId, tagFromRepo.Id);
            _repository.Save();

            var tagToReturn = _mapper.Map<TagDto>(tagEntity);
            return CreatedAtRoute("GetTagForItem",
                new { itemId = itemId, tagId = tagToReturn.Id }, 
                tagToReturn);
        }
        
        [HttpDelete("{tagId}")]
        public ActionResult DeleteTagForItem(Guid itemId, Guid tagId)
        {
            if (!_repository.ItemExists(itemId))
            {
                return NotFound();
            }

            var itemTagFromRepo = _repository.GetItemTag(itemId, tagId);
            _repository.DeleteItemTag(itemTagFromRepo);
            _repository.Save();

            return NoContent();
        }

        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}