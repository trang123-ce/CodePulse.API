namespace CodePulse.API.Models.DTO.Request
{
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string FeaturedImageUrl { get; set; } = default!;
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; } = default!;
        public bool IsVisible { get; set; }
        public string UrlHandle { get; set; }

        public Guid[] Categories { get; set; }
    }
}
