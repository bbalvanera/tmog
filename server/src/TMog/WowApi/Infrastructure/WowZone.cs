namespace TMog.WowApi.Infrastructure
{
    public class WowZone : IWowZone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDungeon { get; set; }

        public bool IsRaid { get; set; }

        public WowLocation Location { get; set; }

        IWowLocation IWowZone.Location
        {
            get { return this.Location; }
        }
    }
}
