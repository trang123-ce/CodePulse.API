namespace CodePulse.API.Models.DTO.Request
{
    public class CreateCategoryRequestDto
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; } = default!;
    }
}
