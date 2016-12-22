namespace TMog.WebApi.Models
{
    public class Source
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public Zone Zone { get; set; }
    }
}
