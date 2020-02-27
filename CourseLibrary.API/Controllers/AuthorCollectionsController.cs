using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CourseLibrary.API.Dto;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using CourseLibrary.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw  new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{ids}", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorsCollection([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null) return BadRequest();

            var authorsEntities = _courseLibraryRepository.GetAuthors(ids);

            if (ids.Count() != authorsEntities.Count()) return NotFound();


            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorsEntities);

            return Ok(authorsToReturn);

        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection([FromBody] IEnumerable<AuthorForCreationDto> authorsForCreationDtos)
        {
            var authorsEntities = _mapper.Map<IEnumerable<Author>>(authorsForCreationDtos);

            foreach (var author in authorsEntities)
            {
                _courseLibraryRepository.AddAuthor(author);
            }

            _courseLibraryRepository.Save(); 

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorsEntities);

            var idsAsString = string.Join(",", authorsToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection", new {ids=idsAsString}, authorsToReturn); 

        }
    }
}
