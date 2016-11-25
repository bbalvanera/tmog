namespace TMog.WowheadApi
{
    public interface IWowheadItemSource
    {
        int? Type { get; }

        int? SubType { get; }

        int? WowheadId { get; }

        string Name { get; }

        int? DropLevel { get; }

        int? Zone { get; }
    }
}
