namespace BuberDinner.Domain.Menu;

using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;

public sealed class Menu : AggregateRoot<MenuId>
{
    public Menu(MenuId id) : base(id)
    {
    }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public float AverageRating { get; set; }

    private readonly List<MenuSection> _sections = new();

    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    


}