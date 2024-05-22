namespace CodePulse.API.Models.Domain
{
    public class BlogPost
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

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
