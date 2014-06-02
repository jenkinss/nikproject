

using SoyalBISAPI.HexCommands;

namespace SoyalWorkTimeWebManager.ViewModels.Helpers
{
    public class AccessConverter
    {
        public static AccessMode GetMode(int mode)
        {
            var result = AccessMode.Invalid;
            switch (mode)
            {
                case 0:
                    result = AccessMode.Invalid;
                    break;
                case 1:
                    result = AccessMode.Readonly;
                    break;
                case 2:
                    result = AccessMode.CardOrPin;
                    break;
                case 3:
                    result = AccessMode.CardAndPin;
                    break;
            }
            return result;
        }


    }
}