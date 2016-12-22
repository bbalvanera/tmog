using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMog.WowApi
{
    public interface IWowProvider
    {
        Task<IEnumerable<IWowZone>> GetAllZones();
    }
}
