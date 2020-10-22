namespace StarBlogs.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string CreatorEmail { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public bool Published { get; set; }
        public Profile Creator { get; set; }
    }
}