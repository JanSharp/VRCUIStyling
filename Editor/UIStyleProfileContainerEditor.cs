using System.Collections.Generic;
using System.Linq;

namespace JanSharp
{
    public static class UIStyleProfileContainerUtil
    {
        public static IEnumerable<UIStyleProfile> GetActiveProfiles(UIStyleProfileContainer container)
        {
            return container.GetComponentsInChildren<UIStyleProfile>(includeInactive: true)
                .Where(IsProfileActive);
        }

        public static bool IsProfileActive(UIStyleProfile profile) => profile.gameObject.activeSelf;
    }
}