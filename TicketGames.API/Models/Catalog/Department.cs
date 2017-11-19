using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department()
        {
            this.Id = 0;
            this.Name = string.Empty;
        }
        public Department(Domain.Model.Department department)
        {
            this.Id = department.Id;
            this.Name = department.Name;
        }

        public List<Department> MappingDepartments(List<Domain.Model.Department> departments)
        {

            List<Department> _departments = new List<Department>();

            foreach (Domain.Model.Department department in departments)
            {
                _departments.Add(new Department(department));
            }

            return _departments;
        }
    }
}