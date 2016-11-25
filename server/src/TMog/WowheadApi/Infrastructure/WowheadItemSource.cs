namespace TMog.WowheadApi.Infrastructure
{
    internal class WowheadItemSource : IWowheadItemSource
    {
        public int? Type
        {
            get;
            set;
        }

        public int? SubType
        {
            get;
            set;
        }

        public int? WowheadId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int? Zone
        {
            get;
            set;
        }

        public int? DropLevel
        {
            get;
            set;
        }
    }
}
