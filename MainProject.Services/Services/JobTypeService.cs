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
    public interface IJobTypeService
    {
        public Task<List<JobType>> GetAllJobTypes();
        public Task<JobType> GetJobTypeById(int? id);
        public Task<bool> CreateJobTypes(JobType jobtype);
        public Task UpdateJobType(JobType jobType);
        public Task DeleteJobType(int id);
        public bool JobTypeExist(int id);

    }
    public class JobTypeService : IJobTypeService
    {
        public async Task<bool> CreateJobTypes(JobType jobType)
        {
            using (var Context = new EmployeeDbContext())
            {
                try
                {
                    Context.Add(jobType);
                    await Context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    return false;
                }
                return true;
            }
        }

        public async Task DeleteJobType(int id)
        {
            using (var Context = new EmployeeDbContext())
            {
                var jobtype = await GetJobTypeById(id);
                Context.JobTypes.Remove(jobtype);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<JobType>> GetAllJobTypes()
        {
            using(var Context = new EmployeeDbContext())
            {
                return await Context.JobTypes.ToListAsync();

            }
        }

        public async Task<JobType> GetJobTypeById(int? id)
        {
            using(var Context = new EmployeeDbContext())
            {
             return await Context.JobTypes.FirstOrDefaultAsync(o => o.ID == id);
            }
        }

        public bool JobTypeExist(int id)
        {
            using (var Context = new EmployeeDbContext())
            {
                return Context.JobTypes.Any(e => e.ID == id);
            }
        }

        public async Task UpdateJobType(JobType jobType)
        {
            using (var Context = new EmployeeDbContext())
            {
                Context.Update(jobType);
                await Context.SaveChangesAsync();
            }
        }
    }
}
