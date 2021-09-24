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
    public interface IMasterService
    {
        public Task<List<Master>> GetAllMasters();
        public Task<Master> GetMasterById(int? id);
        public Task<bool> CreateMasters(Master master);
        public Task UpdateMasters(Master master);
        public Task DeleteMasters(int id);
        public bool MasterExists(int id);



    }
    public class MasterService : IMasterService
    {
        public async Task<bool> CreateMasters(Master master)
        {
            using(var Context = new EmployeeDbContext())
            {
              
                try
                {
                    Context.Add(master);
                    await Context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task DeleteMasters(int id)
        {
            var master = await GetMasterById(id);
            using (var Context = new EmployeeDbContext())
            {
                //var joinContext = Context.Masters.Include(a => a.Employee).Include(a => a.JobType).Include(a => a.Location).Include(a => a.SuperVisor);
                
                Context.Masters.Remove(master);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<Master>> GetAllMasters()
        {
            using(var Context = new EmployeeDbContext())
            {
                var joinContext = Context.Masters.Include(a => a.Employee).Include(a => a.JobType).Include(a => a.Location).Include(a => a.SuperVisor);
                return await joinContext.ToListAsync();
            }
        }

        public async Task<Master> GetMasterById(int? id)
        {
            using (var Context = new EmployeeDbContext())
            {
                var joinContext = Context.Masters.Include(a => a.Employee).Include(a => a.JobType).Include(a => a.Location).Include(a => a.SuperVisor);
                return await joinContext.FirstOrDefaultAsync(m => m.ID == id);
            }
        }

        public bool MasterExists(int id)
        {
            using(var Context = new EmployeeDbContext())
            {
                return Context.Masters.Any(m => m.ID == id);

            }    
        }

        public async Task UpdateMasters(Master master)
        {
           using(var Context = new EmployeeDbContext())
            {
                Context.Update(master);
                await Context.SaveChangesAsync();

            }
        }
    }
}
