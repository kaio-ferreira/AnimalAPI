using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAPI.Data;
using AnimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.Endpoints
{
    public static class AddressEndpoints
    {
        public static void MapAddressessEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            var group = routeBuilder.MapGroup("/Address").WithTags(nameof(Address));

            group.MapGet("/", async (AnimalDbContext db) => await db.Addresses.ToListAsync());

            group.MapGet("/{id}", async (AnimalDbContext db, int id) => await db.Addresses.FirstOrDefaultAsync(address => address.Id == id));

            group.MapPost("/", async (AnimalDbContext db, Address address) =>
            {
                await db.Addresses.AddAsync(address);

                await db.SaveChangesAsync();

                return Results.Created($"/{address.Id}", address);
            });

            group.MapPut("/{id}", async (AnimalDbContext db, Address updaddress, int id) =>
            {
                var address = await db.Addresses.FindAsync(id);

                if (address is null)
                {
                    await db.AddAsync(updaddress);

                    await db.SaveChangesAsync();

                    return Results.Created($"/{updaddress.Id}", updaddress);
                }

                address.Street = updaddress.Street;
                address.District = updaddress.District;
                address.State = updaddress.State;
                address.ZipCode = updaddress.ZipCode;
                address.Country = updaddress.Country;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (AnimalDbContext db, int id) =>
            {
                var address = await db.Addresses.FindAsync(id);

                if (address is null) return Results.NotFound();

                db.Remove(address);

                await db.SaveChangesAsync();

                return Results.Ok();
            });


        }
    }
}