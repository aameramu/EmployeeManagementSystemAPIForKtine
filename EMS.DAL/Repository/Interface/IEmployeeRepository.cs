using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Repository.Interface
{
    public interface IEmployeeRepository
    {
        bool AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(long employeeID);
        List<Employee> GetAllEmployee();
        Employee GetEmployeeByID(long employeeID);
        string Message { get; }
        string ErrorMessage { get; }
        string ErrorMessageDetail { get; }
    }
}
