namespace TMog.Entities
{
    public class Quest
    {
        public int QuestId { get; set; }

        public string Name { get; set; }

        public WowSide Side { get; set; }

        public Zone Zone { get; set; }
    }
}
