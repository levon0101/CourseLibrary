using System;
using CourseLibrary.API.Entities;

namespace CourseLibrary.API.Dto
{
    public class CourseDto
    {

        public Guid Id { get; set; }
 
        public string Title { get; set; }

        public string Description { get; set; }
        
        public Guid AuthorId { get; set; }

    }
}
