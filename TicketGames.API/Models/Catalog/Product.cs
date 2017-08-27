using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Department Department { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public List<Image> Images { get; set; }
        public string UrlImage { get; set; }
        public string UrlImageBanner { get; set; }
        public DateTime RaffleDate { get; set; }
        public int SalesMade { get; set; }
        public int MissingtoSell { get; set; }

        public Product()
        {
            this.Id = 0;
            this.Name = this.ShortDescription = this.UrlImage = this.UrlImageBanner = string.Empty;
            this.Category = new Category();
            this.Department = new Department();
            this.Images = new List<Image>();
        }


        public Product GetProductById(long id)
        {
            List<Image> images = new List<Image>();

            images.Add(new Image() { Id = 1, ImageType = ImageType.Cover, URL = "1.png" });
            images.Add(new Image() { Id = 2, ImageType = ImageType.Detail, URL = "1-D-1.png" });
            images.Add(new Image() { Id = 3, ImageType = ImageType.Detail, URL = "1-D-2.png" });
            images.Add(new Image() { Id = 4, ImageType = ImageType.Detail, URL = "1-D-3.png" });
            images.Add(new Image() { Id = 5, ImageType = ImageType.Detail, URL = "1-D-4.png" });
            images.Add(new Image() { Id = 6, ImageType = ImageType.Detail, URL = "1-D-5.png" });
            images.Add(new Image() { Id = 7, ImageType = ImageType.Banner, URL = "1-B.png" });

            Product product = new Product()
            {
                Id = 1,
                Name = "Watch Dogs 2",
                Category = new Category() { Id = 1, Name = "Playstation 4" },
                Department = new Department() { Id = 1, Name = "Jogos" },
                ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                Description = "Sequência do jogo de ação da Ubisoft de 2014. O jogo agora se passa em São Francisco e traz um novo protagonista, Marcus Holloway, um jovem hacker brilhante que vive no berço da revolução da tecnologia, próximo a região do Vale do Silício, lar de empresas como Facebook, Google e Twitter, para continuar contando sua história sobre vida digital, excesso de compartilhamento de dados pessoais e espionagem estabelecida no primeiro jogo da franquia.",
                Value = 10.00m,
                Images = images,
                SalesMade = 100,
                MissingtoSell = 0,
                RaffleDate = DateTime.Now.AddDays(4)
            };

            return product;
        }
        public List<Product> MostAwardedProducts()
        {
            List<Product> Products = new List<Product>();

            switch (this.Category.Id)
            {
                case 1:
                    {
                        Product productone = new Product()
                        {
                            Id = 120,
                            Name = "Resident Evil Origins Collection",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            UrlImage = "120.png",
                            SalesMade = 50,
                            MissingtoSell = 50
                        };

                        Products.Add(productone);

                        Product producttwo = new Product()
                        {
                            Id = 130,
                            Name = "Tomb Raider Definitive Edition",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "130.png",
                            SalesMade = 20,
                            MissingtoSell = 80
                        };

                        Products.Add(producttwo);

                        Product productthree = new Product()
                        {
                            Id = 140,
                            Name = "Battlefield 4",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "140.png",
                            SalesMade = 31,
                            MissingtoSell = 69
                        };

                        Products.Add(productthree);

                        Product productfour = new Product()
                        {
                            Id = 150,
                            Name = "Grand Theft Auto V",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "150.png",
                            SalesMade = 40,
                            MissingtoSell = 60
                        };

                        Products.Add(productfour);

                        Product productfive = new Product()
                        {
                            Id = 160,
                            Name = "Until Dawn",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "160.png",
                            SalesMade = 31,
                            MissingtoSell = 69
                        };

                        Products.Add(productfive);

                        Product productsix = new Product()
                        {
                            Id = 170,
                            Name = "Uncharted Collection",
                            Category = new Category() { Id = 1, Name = "Playstation 4" },
                            Department = new Department() { Id = 1, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "170.png",
                            SalesMade = 50,
                            MissingtoSell = 50
                        };

                        Products.Add(productsix);

                        break;
                    }
                case 4:
                    {
                        Product product7 = new Product()
                        {
                            Id = 200,
                            Name = "Halo 5",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            Value = 10.00m,
                            UrlImage = "200.png",
                            SalesMade = 31,
                            MissingtoSell = 69
                        };

                        Products.Add(product7);

                        Product product8 = new Product()
                        {
                            Id = 210,
                            Name = "The Witche 3",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            Value = 5.00m,
                            UrlImage = "210.png",
                            SalesMade = 70,
                            MissingtoSell = 30
                        };

                        Products.Add(product8);

                        Product product9 = new Product()
                        {
                            Id = 220,
                            Name = "Watch Dogs",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "220.png",
                            SalesMade = 70,
                            MissingtoSell = 30
                        };

                        Products.Add(product9);

                        Product product10 = new Product()
                        {
                            Id = 230,
                            Name = "Resident Evil VII",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "230.png",
                            SalesMade = 80,
                            MissingtoSell = 20
                        };

                        Products.Add(product10);

                        Product product11 = new Product()
                        {
                            Id = 240,
                            Name = "Grand Theft Auto V",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "240.png",
                            SalesMade = 70,
                            MissingtoSell = 30
                        };

                        Products.Add(product11);

                        Product product12 = new Product()
                        {
                            Id = 250,
                            Name = "Mad Max",
                            Category = new Category() { Id = 4, Name = "Xbox One" },
                            Department = new Department() { Id = 4, Name = "Jogos" },
                            ShortDescription = "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                            Value = 15.00m,
                            UrlImage = "250.png",
                            SalesMade = 90,
                            MissingtoSell = 10
                        };

                        Products.Add(product12);
                        break;
                    }
            }



            return Products;
        }
    }
}