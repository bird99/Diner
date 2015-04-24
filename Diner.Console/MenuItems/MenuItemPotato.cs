using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemPotato : IMenuItem
    {
        public string Name
        {
            get { return "Potato"; }
        }

        public DishType DishType
        {
            get { return DishType.Side; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Night; }
        }
    }
}