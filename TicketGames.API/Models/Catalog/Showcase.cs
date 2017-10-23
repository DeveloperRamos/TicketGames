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

        public void GetShowcase()
        {

            switch (this.ShowcaseType)
            {
                case ShowcaseType.Banner:
                    {

                        List<Image> images = new List<Image>();

                        images.Add(new Image() { Id = 1, ImageType = ImageType.Cover, URL = "1.png" });
                        images.Add(new Image() { Id = 2, ImageType = ImageType.Detail, URL = "1-D-1.png" });
                        images.Add(new Image() { Id = 3, ImageType = ImageType.Detail, URL = "1-D-2.png" });
                        images.Add(new Image() { Id = 4, ImageType = ImageType.Detail, URL = "1-D-3.png" });
                        images.Add(new Image() { Id = 5, ImageType = ImageType.Detail, URL = "1-D-4.png" });
                        images.Add(new Image() { Id = 6, ImageType = ImageType.Detail, URL = "1-D-5.png" });
                        images.Add(new Image() { Id = 7, ImageType = ImageType.Banner, URL = "1-B.png" });

                        Product productone = new Product()
                        {
                            Id = 1,
                            Name = "Watch Dogs 2",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            Images = images,
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(productone);


                        List<Image> imagestwo = new List<Image>();

                        imagestwo.Add(new Image() { Id = 8, ImageType = ImageType.Cover, URL = "10.png" });
                        imagestwo.Add(new Image() { Id = 9, ImageType = ImageType.Banner, URL = "10-B.png" });

                        Product producttwo = new Product()
                        {
                            Id = 10,
                            Name = "Gears of War 4",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            Images = imagestwo,
                            SalesMade = 20,
                            MissingtoSell = 80
                        };

                        this.Products.Add(producttwo);

                        Product productthree = new Product()
                        {
                            Id = 100,
                            Name = "Super Bomberman R",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "100.png",
                            UrlImageBanner = "100-B.png",
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(productthree);

                        break;
                    }
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
                            Value = 10.00m,
                            UrlImage = "260.png",
                            SalesMade = 79,
                            MissingtoSell = 21
                        };

                        this.Products.Add(product5);

                        Product product6 = new Product()
                        {
                            Id = 261,
                            Name = "Mortal Kombat XL",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "261.png",
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
                            Value = 10.00m,
                            UrlImage = "180.png",
                            SalesMade = 98,
                            MissingtoSell = 2
                        };


                        #endregion

                        #region Nintendo Switch

                        Product product9 = new Product()
                        {
                            Id = 400,
                            Name = "Switch 1 e 2",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            UrlImage = "400.png",
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
                            Value = 15.00m,
                            UrlImage = "262.png",
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
                            Value = 15.00m,
                            UrlImage = "191.png",
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
                            Value = 15.00m,
                            UrlImage = "192.png",
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
                            Value = 5.00m,
                            UrlImage = "410.png",
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