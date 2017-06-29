using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetEmployee(long id)
        {
            return _employeeRepository.Get(id);
        }

        public void InsertEmployee(Employee employee)
        {
            _employeeRepository.Insert(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public void DeleteEmployee(long id)
        {
            Employee tmpEmployee = _employeeRepository.Get(id);
            _employeeRepository.Remove(tmpEmployee);
            _employeeRepository.SaveChanges();
        }
    }
}
