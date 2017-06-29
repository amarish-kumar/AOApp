using Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTfulAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private IEmployeeService _service;
        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        //GET api/employee
        public IHttpActionResult Get()
        {
            var empoyees = _service.GetEmployees();
            if (empoyees == null)
            {
                return this.NotFound();
            }
            return this.Ok(empoyees);
        }

        //GET api/employee/1
        public IHttpActionResult Get(long? id)
        {
            long newId = id ?? -1;
            if (id == -1)
                return this.BadRequest("The id is Null");

            var empoyee = _service.GetEmployee(newId);
            if (empoyee == null)
            {
                return this.NotFound();
            }
            return this.Ok(empoyee);
        }

        //POST api/employee/{employee}
        public IHttpActionResult Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                _service.InsertEmployee(employee);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
            
            return this.Ok();
        }

        //PUT api/employee/{employee}
        public IHttpActionResult Put(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                _service.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }

            return this.Ok();
        }

        //DELETE api/employee/1
        public IHttpActionResult Delete(long? id)
        {
            if (id == null)
            {
                return this.BadRequest("The id is Null");
            }

            long newId = id ?? -1;

            try
            {
                var employee = _service.GetEmployee(newId);
                if (employee == null)
                    return this.NotFound();
                else
                {
                    _service.DeleteEmployee(newId);
                    return this.Ok();
                }
                
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }
    }
}
