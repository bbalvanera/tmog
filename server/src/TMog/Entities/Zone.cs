namespace TMog.Entities
{
    public class Zone
    {
        public int? ZoneId { get; set; }

        public string Name { get; set; }

        public ZoneType Type { get; set; }

        public Location Location { get; set; }
    }
}
