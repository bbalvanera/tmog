namespace TMog.WowApi
{
    public interface IWowZone
    {
        int Id { get; }

        string Name { get; }

        IWowLocation Location { get; }

        bool IsDungeon { get; }

        bool IsRaid { get; }
    }
}
