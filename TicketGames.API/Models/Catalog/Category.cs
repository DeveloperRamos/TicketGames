using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Department> Departments { get; set; }

        public Category()
        {
            this.Departments = new List<Department>();
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            var objCategories = HttpContext.Current.Session["Categories"];

            if (objCategories == null)
            {
                List<Department> departments1 = new List<Department>();
                departments1.Add(new Department() { Id = 1, Name = "Acessorios" });
                departments1.Add(new Department() { Id = 2, Name = "Consoles" });
                departments1.Add(new Department() { Id = 3, Name = "Jogos" });

                List<Department> departments2 = new List<Department>();
                departments2.Add(new Department() { Id = 4, Name = "Acessorios" });
                departments2.Add(new Department() { Id = 5, Name = "Consoles" });
                departments2.Add(new Department() { Id = 6, Name = "Jogos" });

                List<Department> departments3 = new List<Department>();
                departments3.Add(new Department() { Id = 7, Name = "Acessorios" });
                departments3.Add(new Department() { Id = 8, Name = "Consoles" });
                departments3.Add(new Department() { Id = 9, Name = "Jogos" });

                List<Department> departments4 = new List<Department>();
                departments4.Add(new Department() { Id = 10, Name = "Acessorios" });
                departments4.Add(new Department() { Id = 11, Name = "Consoles" });
                departments4.Add(new Department() { Id = 12, Name = "Jogos" });

                List<Department> departments5 = new List<Department>();
                departments5.Add(new Department() { Id = 13, Name = "Acessorios" });
                departments5.Add(new Department() { Id = 14, Name = "Consoles" });
                departments5.Add(new Department() { Id = 15, Name = "Jogos" });

                List<Department> departments6 = new List<Department>();
                departments6.Add(new Department() { Id = 16, Name = "Acessorios" });
                departments6.Add(new Department() { Id = 17, Name = "Consoles" });
                departments6.Add(new Department() { Id = 18, Name = "Jogos" });

                List<Department> departments7 = new List<Department>();
                departments7.Add(new Department() { Id = 19, Name = "Acessorios" });
                departments7.Add(new Department() { Id = 20, Name = "Consoles" });
                departments7.Add(new Department() { Id = 21, Name = "Jogos" });

                List<Department> departments8 = new List<Department>();
                departments8.Add(new Department() { Id = 22, Name = "Acessorios" });
                departments8.Add(new Department() { Id = 23, Name = "Consoles" });
                departments8.Add(new Department() { Id = 24, Name = "Jogos" });

                categories.Add(new Category() { Id = 1, Name = "Playstation 3", Departments = departments1 });
                categories.Add(new Category() { Id = 2, Name = "Playstation 4", Departments = departments2 });
                categories.Add(new Category() { Id = 3, Name = "Xbox 360", Departments = departments3 });
                categories.Add(new Category() { Id = 4, Name = "Xbox One", Departments = departments4 });
                categories.Add(new Category() { Id = 5, Name = "Nintendo WII U", Departments = departments5 });
                categories.Add(new Category() { Id = 6, Name = "Nintendo Switch", Departments = departments6 });
                categories.Add(new Category() { Id = 7, Name = "Nintendo 3DS", Departments = departments7 });
                categories.Add(new Category() { Id = 8, Name = "Playstation Vita", Departments = departments8 });

                HttpContext.Current.Session["Categories"] = categories;
            }
            else
            {
                categories = (List<Category>)objCategories;
            }

            return categories;
        }
    }
}