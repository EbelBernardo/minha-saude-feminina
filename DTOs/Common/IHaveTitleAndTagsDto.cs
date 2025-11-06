namespace MinhaSaudeFeminina.DTOs.Common
{
    public interface IHaveTitleAndTagsDto
    {
        string Title { get; set; }

        List<int> TagId { get; set; }
    }
}
