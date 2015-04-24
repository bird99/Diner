using Diner.Enums;

namespace Diner.MenuItems
{
    public class MenuItemCoffee : IMenuItem
    {
        public string Name
        {
            get { return "Coffee"; }
        }

        public DishType DishType
        {
            get { return DishType.Drink; }
        }

        public TimeOfDay TimeOfDay
        {
            get { return TimeOfDay.Morning; }
        }
    }
}