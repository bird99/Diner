using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemEggs : IMenuItem
    {
        public string Name
        {
            get { return "Eggs"; }
        }

        public DishType DishType
        {
            get { return DishType.Entree; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Morning; }
        }
    }
}