using System;
using System.Collections.Generic;
using AutoMapper;
using CourseLibrary.API.Dto;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using CourseLibrary.API.Utils.ExtantionMethods;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")] // or  [Route("api/[Controller]")]
    public class AuthorsController : ControllerBase  //Controller - for with View
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery]AuthorsResourceParameters authorsResourceParameters)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParameters);

 
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }



        [HttpGet("{authorId}")]
        public IActionResult GetAuthors(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null) return NotFound();

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }
    }
}
