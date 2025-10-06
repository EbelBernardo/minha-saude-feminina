using MinhaSaudeFeminina.DTOs.Tags;

namespace MinhaSaudeFeminina.DTOs.Statuses
{
    public class StatusResponseDto
    {
        public int StatusId { get; set; }
        public string Title { get; set; } = null!;

        public int TagId { get; set; }
        public TagResponseDto Tag { get; set; } = null!;
    }
}
