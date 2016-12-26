namespace TMog.WebApi.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Quality { get; set; }

        public Source Source { get; set; }
    }
}
