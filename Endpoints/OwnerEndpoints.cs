using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAPI.Data;
using AnimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.Endpoints
{
    public static class OwnerEndpoints
    {
        public static void MapOwnersEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            var group = routeBuilder.MapGroup("/Owner").WithTags(nameof(Owner));

            group.MapGet("/", async (AnimalDbContext db) => await db.Owners.Include(o => o.Addressess).Include(o => o.Animals).ToListAsync());

            group.MapGet("/{id}", async (AnimalDbContext db, int id) => await db.Owners
            .Include(o => o.Addressess)
            .Include(o => o.Animals)
            .FirstOrDefaultAsync(o => o.Id == id));

            group.MapPost("/", async (AnimalDbContext db, Owner owner) =>
            {
                await db.Owners.AddAsync(owner);

                await db.SaveChangesAsync();

                return Results.Created($"/{owner.Id}", owner);
            });

            group.MapPut("/{id}", async (AnimalDbContext db, Owner updowner, int id) =>
            {
                var owner = await db.Owners.FindAsync(id);

                if (owner is null)
                {
                    await db.Owners.AddAsync(updowner);

                    await db.SaveChangesAsync();

                    return Results.Created($"/{updowner.Id}", updowner);
                }

                owner.Name = updowner.Name;
                owner.Age = updowner.Age;
                owner.Occupation = updowner.Occupation;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (AnimalDbContext db, int id) =>
            {
                var owner = await db.Owners.FindAsync(id);

                if (owner is null) return Results.NotFound();

                db.Remove(owner);

                return Results.Ok();
            });
        }
    }
}