using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Showcase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ShowcaseType ShowcaseType { get; set; }        
        public List<Product> Products { get; set; }

        public Showcase()
        {
            this.Products = new List<Product>();
        }

        public Showcase(Domain.Model.Showcase showcase)
        {
            this.Id = showcase.Id;
            this.Name = showcase.Name;
            this.ShowcaseType = (ShowcaseType)showcase.ShowcaseTypeId;
            this.Products = new List<Product>();

            foreach (Domain.Model.ShowcaseProduct showcaseProduct in showcase.ShowcaseProducts)
            {
                showcaseProduct.Product.Order = showcaseProduct.Order;
                Product product = new Product(showcaseProduct.Product);

                this.Products.Add(product);
            }
        }

        public void GetShowcase()
        {

            switch (this.ShowcaseType)
            {
                case ShowcaseType.Recent:
                    {
                        #region Xbox One

                        Product product5 = new Product()
                        {
                            Id = 260,
                            Name = "Resident Evil 2 Revelation",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00f,
                            
                            SalesMade = 79,
                            MissingtoSell = 21,
                        };

                        this.Products.Add(product5);

                        Product product6 = new Product()
                        {
                            Id = 261,
                            Name = "Mortal Kombat XL",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00f,
                            
                            SalesMade = 8,
                            MissingtoSell = 92
                        };

                        this.Products.Add(product6);



                        #endregion

                        #region Playstation 4

                        Product product1 = new Product()
                        {
                            Id = 180,
                            Name = "Street Fighter V",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00f,
                            
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(product1);

                        #endregion

                        #region Nintendo Switch

                        Product product9 = new Product()
                        {
                            Id = 400,
                            Name = "Switch 1 e 2",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00f,
                            
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(1)
                        };

                        this.Products.Add(product9);

                        #endregion

                        break;
                    }
                case ShowcaseType.Popular:
                    {
                        Product product7 = new Product()
                        {
                            Id = 262,
                            Name = "Minecraft",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00f,
                            
                            SalesMade = 39,
                            MissingtoSell = 61
                        };

                        this.Products.Add(product7);

                        Product product3 = new Product()
                        {
                            Id = 191,
                            Name = "God of War 4",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00f,
                            
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(product3);

                        Product product4 = new Product()
                        {
                            Id = 192,
                            Name = "Crash Bandicoot N Sane Trilogy",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00f,
                            
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(product4);

                        Product product10 = new Product()
                        {
                            Id = 410,
                            Name = "Has-Been Heroes",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00f,
                            
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(3)
                        };

                        this.Products.Add(product10);

                        break;
                    }
                case ShowcaseType.Console:
                    break;
            }
        }
    }
}