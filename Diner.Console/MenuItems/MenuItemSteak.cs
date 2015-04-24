using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemSteak : IMenuItem
    {
        public string Name
        {
            get { return "Steak"; }
        }

        public DishType DishType
        {
            get { return DishType.Entree; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Night; }
        }
    }
}
