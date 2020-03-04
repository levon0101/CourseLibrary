using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Course, Dto.CourseDto>(); // from Course to CourseDto
            CreateMap<Dto.CourseForCreationDto, Entities.Course>(); // from  CourseForCreationDto to Course
            CreateMap<Dto.CourseForUpdateDto, Entities.Course>(); // from  CourseForUpdateDto to Course
            CreateMap<Entities.Course, Dto.CourseForUpdateDto>(); // from Course to CourseForUpdateDto

        }
    }

}
