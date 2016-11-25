namespace TMog.WowheadApi.Infrastructure
{
    internal class ItemXmlData
    {
        private int?[] values = new int?[3];

        public int? Quality
        {
            get
            {
                return this.values[0];
            }
            set
            {
                this.values[0] = value;
            }
        }

        public int? Class
        {
            get
            {
                return this.values[1];
            }
            set
            {
                this.values[1] = value;
            }
        }

        public int? Subclass
        {
            get
            {
                return this.values[2];
            }
            set
            {
                this.values[2] = value;
            }
        }

        public int? this[int index]
        {
            get
            {
                return this.values[index];
            }
            set
            {
                this.values[index] = value;
            }
        }
    }
}
