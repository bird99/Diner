using Diner.Enums;

namespace Diner.MenuItems
{
    public interface IMenuItem
    {
        string Name { get; }
        DishType DishType { get; }
        TimeOfDay TimeOfDay { get; }
    }
}