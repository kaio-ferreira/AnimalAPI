using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAPI.Data;
using AnimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.Endpoints
{
    public static class AnimalEndpoints
    {
        public static void MapAnimalsEndpoints(this IEndpointRouteBuilder route)
        {
            var group = route.MapGroup("/Animal").WithTags(nameof(Animal));

            group.MapGet("/", async (AnimalDbContext db) => await db.Animals.ToListAsync());

            group.MapGet("/{id}", async (AnimalDbContext db, int id) => await db.Animals.FirstOrDefaultAsync(a => a.Id == id));

            group.MapPost("/", async (AnimalDbContext db, Animal animal) =>
            {
                await db.Animals.AddAsync(animal);

                await db.SaveChangesAsync();

                return Results.Created($"/{animal.Id}", animal);
            });

            group.MapPut("/{id}", async (AnimalDbContext db, Animal updanimal, int id) =>
            {
                var animal = await db.Animals.FindAsync(id);

                if (animal is null)
                {
                    await db.Animals.AddAsync(updanimal);

                    await db.SaveChangesAsync();

                    return Results.Created($"/{updanimal.Id}", updanimal);
                }

                animal.Name = updanimal.Name;
                animal.Age = updanimal.Age;
                animal.Race = updanimal.Race;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (AnimalDbContext db, int id) =>
            {
                var animal = await db.Animals.FindAsync(id);

                if (animal is null) return Results.NotFound();

                db.Remove(animal);

                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}