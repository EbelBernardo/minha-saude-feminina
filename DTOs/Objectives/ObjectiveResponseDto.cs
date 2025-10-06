using MinhaSaudeFeminina.DTOs.Tags;

namespace MinhaSaudeFeminina.DTOs.Objectives
{
    public class ObjectiveResponseDto
    {
        public int ObjectiveId { get; set; }
        public string Title { get; set; } = null!;

        public int TagId { get; set; }
        public TagResponseDto Tag { get; set; } = null!;
    }
}
