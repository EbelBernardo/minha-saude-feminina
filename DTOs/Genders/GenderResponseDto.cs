using MinhaSaudeFeminina.DTOs.Tags;

namespace MinhaSaudeFeminina.DTOs.Gender
{
    public class GenderResponseDto
    {
        public int GenderId { get; set; }
        public string Title { get; set; } = null!;

        public int TagId { get; set; }
        public TagResponseDto Tag { get; set; } = null!;
    }
}
