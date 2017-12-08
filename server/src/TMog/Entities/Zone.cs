namespace TMog.Entities
{
    public class Zone
    {
        public int? ZoneId { get; set; }

        public string Name { get; set; }

        public ZoneType Type { get; set; }

        public Zone Parent { get; set; }

        public Region Region { get; set; }
    }
}
