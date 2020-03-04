using System.ComponentModel.DataAnnotations;
using CourseLibrary.API.ValidationAttributes;

namespace CourseLibrary.API.Dto
{
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "The provided description need to be different from the title.")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a title")]
        [MaxLength(100, ErrorMessage = "The title shouldn't have more then 100 characters.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "The Description shouldn't have more then 1500 characters.")]
        public virtual string Description { get; set; }
    }
}
