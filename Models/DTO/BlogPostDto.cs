namespace CodePulse.API.Models.DTO
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string FeaturedImageUrl { get; set; } = default!;
        public DateTime PublishedDate { get; set; }
        public string UrlHandle { get; set; }
        public string Author { get; set; } = default!;
        public bool IsVisible { get; set; }
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
