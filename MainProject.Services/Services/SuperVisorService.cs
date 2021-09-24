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
    public interface ISuperVisorService
    {
        public Task<List<SuperVisor>> GetAllSuperVisors();
        public Task<SuperVisor> GetSuperVisorById(int? id);
        public Task<bool> CreateSuperVisor(SuperVisor superVisor);
        public Task UpdateSuperVisor(SuperVisor superVisor);
        public Task DeleteSuperVisor(int id);
        public bool SuperVisorExist(int id);

    }
    public class SuperVisorService : ISuperVisorService
    {
        public async Task<bool> CreateSuperVisor(SuperVisor superVisor)
        {
         using(var Context = new EmployeeDbContext())
            {
                try
                {
                    Context.Add(superVisor);
                    await Context.SaveChangesAsync();

                }
                catch (Exception)
                {
                    return false;

                    throw;
                }
                return true;
            }
       
        }

        public async Task DeleteSuperVisor(int id)
        {
            using(var Context = new EmployeeDbContext())
            {
                var superVisor = await GetSuperVisorById(id);
                Context.SuperVisors.Remove(superVisor);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<SuperVisor>> GetAllSuperVisors()
        {
           using(var Context = new EmployeeDbContext())
            {
                return await Context.SuperVisors.ToListAsync();

            }
        }

        public async Task<SuperVisor> GetSuperVisorById(int? id)
        {
            using(var Context = new EmployeeDbContext())
            {
                return await Context.SuperVisors.FirstOrDefaultAsync(s => s.ID == id);
            }
        }

        public bool SuperVisorExist(int id)
        {
          using(var Context = new EmployeeDbContext())
            {
                return Context.SuperVisors.Any(s => s.ID == id);
            }    
        }

        public async Task UpdateSuperVisor(SuperVisor superVisor)
        {
            using(var Context = new EmployeeDbContext())
            {
                Context.Update(superVisor);
                await Context.SaveChangesAsync();
                
            }
        }
    }
}
