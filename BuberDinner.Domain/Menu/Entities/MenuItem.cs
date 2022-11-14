namespace BuberDinner.Domain.Menu.Entities;

using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

public class MenuItem : Entity<MenuItemId>
{
    public MenuItem(MenuItemId id) : base(id)
    {
    }
    
    public string Name { get; set; }
    public string Description { get; set; }

    private MenuItem(MenuItemId menuItemId, string name, string description)
     :base(menuItemId)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name, string description)
    {
        return new(MenuItemId.CreateUnique(), name, description);
    }
    
}