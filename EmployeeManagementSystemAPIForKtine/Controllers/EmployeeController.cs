using EMS.DAL.Entities;
using EMS.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EmployeeManagementSystemAPIForKtine.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository employeeRepository;
        //Injected IEmployeeRepository in Controller Constructor using Unity IOC
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Get all employees record
        /// </summary>
        /// <returns>This will return list of employees object</returns>
        public IHttpActionResult GetAllEmployees()
        {
            var data = employeeRepository.GetAllEmployee().ToArray();
            return Ok(data);
        }

        /// <summary>
        /// This action is use for 
        /// getting employee by employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This will return single employee object</returns>
        public IHttpActionResult GetEmployeeByID(long id)
        {
            var oEmployee = employeeRepository.GetEmployeeByID(id);
            if (oEmployee == null)
            {
                return NotFound();
            }
            return Ok(oEmployee);
        }
        /// <summary>
        ///This action is use to
        ///add employee
        /// </summary>
        /// <param name="oEmployee"></param>
        /// <returns>This will return success message after adding employee</returns>

        public IHttpActionResult PostAddEmployee(Employee oEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                _ = employeeRepository.AddEmployee(oEmployee);
            }
            return Created("", employeeRepository.Message);
        }

        /// <summary>
        ///This action is use to
        ///update employee
        /// </summary>
        /// <param name="oEmployee"></param>
        /// <returns>This will return success message after updating employee</returns>
        public IHttpActionResult PutUpdateEmployee(Employee oEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                _ = employeeRepository.UpdateEmployee(oEmployee);
            }
            return Created("", employeeRepository.Message);
        }

        /// <summary>
        ///This action is use to
        ///deleting Employee
        /// </summary>
        /// <param name="oEmployee"></param>
        /// <returns>This will return success message after deleting employee</returns>
        public IHttpActionResult DeleteRemoveEmployee(long id)
        {
            bool isDeleted = employeeRepository.DeleteEmployee(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(employeeRepository.Message);
        }
    }
}