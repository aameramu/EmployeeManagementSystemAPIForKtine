using EMS.DAL.Entities;
using EMS.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EMS.DAL.Repository.Class
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Local Declaration

        private EmployeeManagementSystemKtineEntities _context;

        #endregion Local Declaration

        #region Constructor

        public EmployeeRepository(EmployeeManagementSystemKtineEntities employeeManagementSystemKtineEntities)
        {
            _context = employeeManagementSystemKtineEntities;
        }
        #endregion

        #region Repository Methods
        public bool AddEmployee(Employee employee)
        {
            bool isSaved = false;
            try
            {
                if (employee != null)
                {
                    Message = "Employee added successfully";
                    _context.Employees.Add(employee);
                    if (_context.SaveChanges() > 0)
                    {
                        isSaved = true;
                    }
                }
                else
                {
                    Message = "Sorry, Employee not added successfully.";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ErrorMessageDetail = ex.InnerException.Message;
            }
            return isSaved;
        }
        public bool UpdateEmployee(Employee employee)
        {
            bool isUpdated = false;
            try
            {
                if (employee !=null && employee.EmployeeID > 0 )
                {
                    var target = _context.Employees.Find(employee.EmployeeID);
                    if (target != null)
                    {
                        target.FirstName = employee.FirstName;
                        target.MiddleName = employee.MiddleName;
                        target.LastName = employee.LastName;
                        _context.Entry(target).State = EntityState.Modified;
                        if (_context.SaveChanges() > 0)
                        {
                            isUpdated = true;
                            Message = "Employee updated successfully";
                        }
                    }
                    else
                    {
                        Message = "Employee not found";
                    }
                }
                else
                {
                    Message = "Employee not found";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ErrorMessageDetail = ex.InnerException.Message;
            }
            return isUpdated;
        }
        public bool DeleteEmployee(long employeeID)
        {
            bool isDeleted = false;
            try
            {
                var oEmployee = _context.Employees.Find(employeeID);
                if (oEmployee != null)
                {
                    _context.Employees.Remove(oEmployee);
                    if (_context.SaveChanges() > 0)
                    {
                        isDeleted = true;
                        Message = "Employee deleted successfully";
                    }
                }
                else
                {
                    Message = "Employee not found.";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ErrorMessageDetail = ex.InnerException.Message;
            }
            return isDeleted;
        }
        public List<Employee> GetAllEmployee()
        {
            List<Employee> oList = new List<Employee>();
            try
            {
                oList = _context.Employees.Where(x => x.Active == true).ToList();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ErrorMessageDetail = ex.InnerException.Message;
            }
            return oList;
        }
        public Employee GetEmployeeByID(long employeeID)
        {
            Employee oEmployee = new Employee();
            try
            {
                oEmployee = _context.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeID == employeeID && x.Active == true);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ErrorMessageDetail = ex.InnerException.Message;
            }
            return oEmployee;
        }
        #endregion

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose
        public string Message { get; private set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;
        public string ErrorMessageDetail { get; private set; } = string.Empty;


    }
}
