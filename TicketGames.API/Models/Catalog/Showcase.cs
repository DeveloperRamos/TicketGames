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
        public List<Category> Categories { get; set; }

        public Showcase()
        {
            this.Products = new List<Product>();
            this.Categories = new List<Category>();
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
                case ShowcaseType.Category:
                    {

                        this.Categories.Add(new Category() { Id = 1, Name = "Playstation 4" });
                        this.Categories.Add(new Category() { Id = 4, Name = "Xbox One" });
                        this.Categories.Add(new Category() { Id = 8, Name = "Nintendo Switch" });

                        #region Playstation 4

                        Product productone = new Product()
                        {
                            Id = 120,
                            Name = "Resident Evil Origins Collection",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            UrlImage = "120.png",
                            SalesMade = 99,
                            MissingtoSell = 1
                        };

                        this.Products.Add(productone);

                        Product producttwo = new Product()
                        {
                            Id = 130,
                            Name = "Tomb Raider Definitive Edition",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "130.png",
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(20)
                        };

                        this.Products.Add(producttwo);

                        Product productthree = new Product()
                        {
                            Id = 140,
                            Name = "Battlefield 4",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "140.png",
                            SalesMade = 50,
                            MissingtoSell = 50
                        };

                        this.Products.Add(productthree);

                        Product productfour = new Product()
                        {
                            Id = 150,
                            Name = "Grand Theft Auto V",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "150.png",
                            SalesMade = 30,
                            MissingtoSell = 70
                        };

                        this.Products.Add(productfour);

                        Product productfive = new Product()
                        {
                            Id = 160,
                            Name = "Until Dawn",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "160.png",
                            SalesMade = 79,
                            MissingtoSell = 21
                        };

                        this.Products.Add(productfive);

                        Product productsix = new Product()
                        {
                            Id = 170,
                            Name = "Uncharted Collection",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "170.png",
                            SalesMade = 32,
                            MissingtoSell = 68
                        };

                        this.Products.Add(productsix);
                        #endregion

                        #region Xbox One

                        Product product7 = new Product()
                        {
                            Id = 200,
                            Name = "Halo 5",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            UrlImage = "200.png",
                            SalesMade = 21,
                            MissingtoSell = 79
                        };

                        this.Products.Add(product7);

                        Product product8 = new Product()
                        {
                            Id = 210,
                            Name = "The Witche 3",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "210.png",
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(product8);

                        Product product9 = new Product()
                        {
                            Id = 220,
                            Name = "Watch Dogs",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "220.png",
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(1)
                        };

                        this.Products.Add(product9);

                        Product product10 = new Product()
                        {
                            Id = 230,
                            Name = "Resident Evil VII",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "230.png",
                            SalesMade = 54,
                            MissingtoSell = 46
                        };

                        this.Products.Add(product10);

                        Product product11 = new Product()
                        {
                            Id = 240,
                            Name = "Grand Theft Auto V",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "240.png",
                            SalesMade = 99,
                            MissingtoSell = 1
                        };

                        this.Products.Add(product11);

                        Product product12 = new Product()
                        {
                            Id = 250,
                            Name = "Mad Max",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "250.png",
                            SalesMade = 92,
                            MissingtoSell = 8
                        };

                        this.Products.Add(product12);

                        #endregion

                        #region Nintendo Switch

                        Product product13 = new Product()
                        {
                            Id = 300,
                            Name = "Arms",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            UrlImage = "300.png",
                            SalesMade = 22,
                            MissingtoSell = 78
                        };

                        this.Products.Add(product13);

                        Product product14 = new Product()
                        {
                            Id = 310,
                            Name = "Mario Kart 8 Deluxe",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "310.png",
                            SalesMade = 43,
                            MissingtoSell = 57
                        };

                        this.Products.Add(product14);

                        Product product15 = new Product()
                        {
                            Id = 320,
                            Name = "Zelda Breath of the Wild",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "320.png",
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(product15);

                        Product product16 = new Product()
                        {
                            Id = 330,
                            Name = "Dragon Quest Heroes I/II",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "330.png",
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(15)
                        };

                        this.Products.Add(product16);

                        Product product17 = new Product()
                        {
                            Id = 340,
                            Name = "Ultra Street Fighter 2 The Final Challengers",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "340.png",
                            SalesMade = 98,
                            MissingtoSell = 2
                        };

                        this.Products.Add(product17);

                        Product product18 = new Product()
                        {
                            Id = 350,
                            Name = "Splatoon 2",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "350.png",
                            SalesMade = 54,
                            MissingtoSell = 46
                        };

                        this.Products.Add(product18);

                        #endregion

                        break;
                    }
                case ShowcaseType.Console:
                    {
                        break;
                    }
                case ShowcaseType.Product:
                    {
                        this.Categories.Add(new Category() { Id = 4, Name = "Xbox One" });
                        this.Categories.Add(new Category() { Id = 1, Name = "Playstation 4" });
                        this.Categories.Add(new Category() { Id = 8, Name = "Nintendo Switch" });

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

                        Product product8 = new Product()
                        {
                            Id = 263,
                            Name = "Forza Horizon 3",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "263.png",
                            SalesMade = 67,
                            MissingtoSell = 33
                        };

                        this.Products.Add(product8);

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

                        this.Products.Add(product1);

                        Product product2 = new Product()
                        {
                            Id = 190,
                            Name = "The Last of Us",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "190.png",
                            SalesMade = 0,
                            MissingtoSell = 100
                        };

                        this.Products.Add(product2);

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

                        Product product11 = new Product()
                        {
                            Id = 420,
                            Name = "Lego City Undercover",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "420.png",
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(10)
                        };

                        this.Products.Add(product11);

                        Product product12 = new Product()
                        {
                            Id = 430,
                            Name = "Disgaea 5 Complete",
                            Category = new Category() { Id = 8, Name = "Nintendo Switch" },
                            Department = new Department() { Id = 8, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "430.png",
                            SalesMade = 100,
                            MissingtoSell = 0,
                            RaffleDate = DateTime.Now.AddDays(5)
                        };

                        this.Products.Add(product12);

                        #endregion

                        break;
                    }
            }
        }
    }
}