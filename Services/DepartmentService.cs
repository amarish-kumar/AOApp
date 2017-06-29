using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private IRepository<Department> _departmentRepository;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _departmentRepository.GetAll();
        }

        public Department GetDepartment(long id)
        {
            return _departmentRepository.Get(id);
        }

        public void InsertDepartment(Department employee)
        {
            _departmentRepository.Insert(employee);
        }

        public void UpdateDepartment(Department employee)
        {
            _departmentRepository.Update(employee);
        }

        public void DeleteDepartment(long id)
        {
            Department tmpEmployee = _departmentRepository.Get(id);
            _departmentRepository.Remove(tmpEmployee);
            _departmentRepository.SaveChanges();
        }
    }
}
