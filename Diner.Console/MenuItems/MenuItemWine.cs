using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemWine : IMenuItem
    {
        public string Name
        {
            get { return "Wine"; }
        }

        public DishType DishType
        {
            get { return DishType.Drink; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Night; }
        }
    }
}