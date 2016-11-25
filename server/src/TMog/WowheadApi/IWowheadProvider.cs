using System.Threading.Tasks;

namespace TMog.WowheadApi
{
    public interface IWowheadProvider
    {
        Task<IWowheadItem> GetItemById(int itemId);

        Task<IWowheadSet> GetSetById(int setId);
    }
}
