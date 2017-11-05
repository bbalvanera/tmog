namespace TMog.WebApi.Models
{
    public class Zone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? LocationId { get; set; }

        public string LocationName { get; set; }
    }
}
