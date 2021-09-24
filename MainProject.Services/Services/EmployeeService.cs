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
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployeeById(int? id);
        public Task<bool> CreateEmployees(Employee employee);
        public Task UpdateEmployee(Employee employee);
        public Task DeleteEmployee(int id);
        public bool EmployeeExist(int id);


    }
    public class EmployeeService : IEmployeeService
    {
        public async Task<bool> CreateEmployees(Employee employee)
        {
            using (var Context = new EmployeeDbContext())
            {
                try
                {
                    Context.Add(employee);
                    await Context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    return false;
                }
                return true;
            }
        }

        public async Task DeleteEmployee(int id)
        {

            using(var Context = new EmployeeDbContext())
            {
                var employee = await GetEmployeeById(id);
                Context.Employees.Remove(employee);
                await Context.SaveChangesAsync();
            }
        }

        public bool EmployeeExist(int id)
        {
           using(var Context = new EmployeeDbContext())
            {
                return Context.Employees.Any(e => e.ID == id);
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
           using(var Context = new EmployeeDbContext())
            {
                return await Context.Employees.ToListAsync();

            }
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            using (var Context = new EmployeeDbContext())
            {
                return await Context.Employees.FirstOrDefaultAsync(o => o.ID == id);
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            using(var Context = new EmployeeDbContext())
            {
                Context.Update(employee);
                await Context.SaveChangesAsync();
            }
        }
    }
}
