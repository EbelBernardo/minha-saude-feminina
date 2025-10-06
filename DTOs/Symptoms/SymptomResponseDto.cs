using MinhaSaudeFeminina.DTOs.Tags;

namespace MinhaSaudeFeminina.DTOs.Symptoms
{
    public class SymptomResponseDto
    {
        public int SymptomId { get; set; }
        public string Title { get; set; } = null!;

        public List<TagResponseDto> Tags { get; set; } = new();   
    }
}
