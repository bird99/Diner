using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemToast : IMenuItem
    {
        public string Name
        {
            get { return "Toast"; }
        }

        public DishType DishType
        {
            get { return DishType.Side; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Morning; }
        }
    }
}