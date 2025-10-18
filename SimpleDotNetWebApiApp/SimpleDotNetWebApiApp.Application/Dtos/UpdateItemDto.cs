namespace SimpleDotNetWebApiApp.Application.Dtos
{
    public class UpdateItemDto : ItemDto
    {
        public bool IgnoreImage { get; set; }
    }
}
