using System.Collections.Generic;
using TMog.Data;

namespace TMog.UnitTests.Data.TMogDatabaseTests
{
    public class TMogDatabaseTestHelper : SystemUnderTestHelper<TMogDatabase>
    {
        protected static readonly TMogDatabase db = new TMogDatabase();

        protected override TMogDatabase GetSubject()
        {
            return new TMogDatabase();
        }

        internal static class ReservedIds
        {
            private static int current = 100000;
            private static readonly Dictionary<Group, List<int>> reservedIds = new Dictionary<Group, List<int>>();

            public enum Group
            {
                Sets,
                Items,
                Sources,
                Zones
            }

            public static int GetNexIdFor(Group group)
            {
                SafeAdd(group, ++current);
                return current;
            }

            public static List<int> GetReservedIds(Group group)
            {
                return reservedIds[group];
            }

            private static void SafeAdd(Group group, int id)
            {
                if (!reservedIds.ContainsKey(group))
                {
                    reservedIds.Add(group, new List<int>());
                }

                reservedIds[group].Add(id);
            }
        }
    }
}
