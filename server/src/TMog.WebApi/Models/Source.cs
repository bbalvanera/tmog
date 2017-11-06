namespace TMog.WebApi.Models
{
    public class Source
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string SubType { get; set; }

        public int DropLevel { get; set; }

        public string DropLevelName { get; set; }

        public string Description { get; set; }

        public int? WowheadId { get; set; }

        public Zone Zone { get; set; }
    }
}
