using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService(PizzaContext ctx)
{
    public IEnumerable<Pizza> GetAll()
    {
        return ctx.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id)
    {
        return ctx.Pizzas.Include(x => x.Toppings).Include(x => x.Sauce).AsNoTracking().SingleOrDefault(x => x.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        ctx.Pizzas.Add(newPizza);
        ctx.SaveChanges();
        return newPizza;
    }

    public void UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = ctx.Pizzas.Find(pizzaId);
        var sauceToUpdate = ctx.Sauces.Find(sauceId);
        if (pizzaToUpdate is not null && sauceToUpdate is not null)
        {
            pizzaToUpdate.Sauce = sauceToUpdate;
            ctx.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = ctx.Pizzas.Find(pizzaId);
        var toppingToAdd = ctx.Toppings.Find(toppingId);
        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException();
        }
        pizzaToUpdate.Toppings ??= new List<Topping>();
        pizzaToUpdate.Toppings.Add(toppingToAdd);

        ctx.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = ctx.Pizzas.Find(id);
        if (pizzaToDelete is not null)
        {
            ctx.Pizzas.Remove(pizzaToDelete);
            ctx.SaveChanges();
        }
    }
}