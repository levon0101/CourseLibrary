using System;
using System.Collections.Generic;
using AutoMapper;
using CourseLibrary.API.Dto;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository,IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }




        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId)) return NotFound();


            var coursesFromDb = _courseLibraryRepository.GetCourses(authorId);


            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesFromDb));
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {

            
            if (!_courseLibraryRepository.AuthorExists(authorId)) return NotFound();

            var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorFromRepo == null) return NotFound();

            return Ok(_mapper.Map<Course, CourseDto>(courseForAuthorFromRepo));

        }
    }
}
