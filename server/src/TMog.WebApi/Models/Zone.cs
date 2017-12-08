namespace TMog.WebApi.Models
{
    public class Zone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string ParentZoneName { get; set; }
    }
}
