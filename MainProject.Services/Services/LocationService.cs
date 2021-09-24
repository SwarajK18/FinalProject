using FinalProject.DAL.Data;
using MainProject.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Services.Services
{
    public interface ILocationService
    {

        public Task<List<Location>> GetAllLocations();
        public Task<Location> GetLocationById(int? id);
        public Task<bool> CreateLocations(Location location);
        public Task UpdateLocation(Location location);
        public Task DeleteLocation(int id);
        public bool LocationExist(int id);

    }
    public class LocationService : ILocationService
    {
        public async Task<bool> CreateLocations(Location location)
        {
            using(var Context = new EmployeeDbContext())
            {
                try
                {
                    Context.Add(location);
                    await Context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    return false;
                }
                return true;
            }
        }

        public async Task DeleteLocation(int id)
        {
            using (var Context = new EmployeeDbContext())
            {
                var location = await GetLocationById(id);
                Context.Locations.Remove(location);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<Location>> GetAllLocations()
        {
            using (var Context = new EmployeeDbContext())
            {
                return await Context.Locations.ToListAsync();

            }
        }

        public async Task<Location> GetLocationById(int? id)
        {
           using(var Context = new EmployeeDbContext())
            {
                return await Context.Locations.FirstOrDefaultAsync(o => o.ID == id);
            }
        }

        public bool LocationExist(int id)
        {
            using (var Context = new EmployeeDbContext())
            {
                return Context.Employees.Any(e => e.ID == id);
            }
        }

        public async Task UpdateLocation(Location location)
        {
            using (var Context = new EmployeeDbContext())
            {
                Context.Update(location);
                await Context.SaveChangesAsync();
            }
        }
    }
}
