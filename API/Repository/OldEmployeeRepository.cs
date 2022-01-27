using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Repository
{
    public class OldEmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public OldEmployeeRepository(MyContext context)

        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
            //throw new NotImplementedException(string NIK);
        }

        public int Insert(Employee employee)
        {
            var emailExist = EmailExist(employee);
            var phoneExist = PhExist(employee);
            if (emailExist == false)
            {
                if(phoneExist==false)
                {
                    var NIK = GetNIK() +1;
                    var year = DateTime.Now.Year;
                    employee.NIK = year + "00" + NIK.ToString();

                    context.Employees.Add(employee);
                    var res = context.SaveChanges();
                    return res;
                }
                else
                {
                    return 3;
                }
            }
            else if (emailExist == true && phoneExist == true)
            {
                return 4;
            }
            else
            {
                return 2;
            }
        }

        public int Put(Employee employee)
        {
            
            context.Entry(employee).State = EntityState.Modified;
            
            var res = context.SaveChanges();
            return res;
        }

        public bool EmailExist(Employee employee)
        {
            var cekEmail = context.Employees.Where(emp => emp.Email == employee.Email).FirstOrDefault();
            if (cekEmail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool PhExist(Employee employee)
        {
            var cekPh = context.Employees.Where(emp => emp.Phone == employee.Phone).FirstOrDefault();
            if (cekPh != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetNIK()
        {
            var lastEmp = context.Employees.OrderByDescending(emp => emp.NIK).FirstOrDefault();
            if (lastEmp == null)
            {
                return 0;
            }
            else
            {
                var lastNIK = lastEmp.NIK.Remove(0, 5);
                return int.Parse(lastNIK);
            }
        }
    }


}
