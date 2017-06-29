using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(long id);
        void InsertDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(long id);
    }
}
