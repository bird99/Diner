using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemCake : IMenuItem
    {
        public string Name
        {
            get { return "Cake"; }
        }

        public DishType DishType
        {
            get { return DishType.Desert; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Night; }
        }
    }
}