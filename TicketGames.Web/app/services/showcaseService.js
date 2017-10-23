ticketGamesApp.service('showcaseService', function ($http) {
    var urlBase = '/v1/showcase';

    var service = {
        getShowcases: getShowcases
    };


    function getShowcases(identifier) {

        var showcase = '';

        switch (identifier) {
            case 'Banner':

                showcase =
                    {
                        "Id": 1,
                        "Name": "Vitrine do banner",
                        "ShowcaseType": 1,
                        "Products": [
                            {
                                "Id": 1,
                                "Name": "Watch Dogs 2",
                                "Category": {
                                    "Id": 1,
                                    "Name": "Playstation 4",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 1,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                                "Description": null,
                                "Value": 10,
                                "Images": [
                                    {
                                        "Id": 1,
                                        "ImageType": 1,
                                        "URL": "1.png"
                                    },
                                    {
                                        "Id": 2,
                                        "ImageType": 2,
                                        "URL": "1-D-1.png"
                                    },
                                    {
                                        "Id": 3,
                                        "ImageType": 2,
                                        "URL": "1-D-2.png"
                                    },
                                    {
                                        "Id": 4,
                                        "ImageType": 2,
                                        "URL": "1-D-3.png"
                                    },
                                    {
                                        "Id": 5,
                                        "ImageType": 2,
                                        "URL": "1-D-4.png"
                                    },
                                    {
                                        "Id": 6,
                                        "ImageType": 2,
                                        "URL": "1-D-5.png"
                                    },
                                    {
                                        "Id": 7,
                                        "ImageType": 3,
                                        "URL": "1-B.png"
                                    }
                                ],
                                "UrlImage": "",
                                "UrlImageBanner": "",
                                "RaffleDate": "0001-01-01T00:00:00",
                                "SalesMade": 98,
                                "MissingtoSell": 2
                            },
                            {
                                "Id": 10,
                                "Name": "Gears of War 4",
                                "Category": {
                                    "Id": 4,
                                    "Name": "Xbox One",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 4,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                                "Description": null,
                                "Value": 5,
                                "Images": [
                                    {
                                        "Id": 8,
                                        "ImageType": 1,
                                        "URL": "10.png"
                                    },
                                    {
                                        "Id": 9,
                                        "ImageType": 3,
                                        "URL": "10-B.png"
                                    }
                                ],
                                "UrlImage": "",
                                "UrlImageBanner": "",
                                "RaffleDate": "0001-01-01T00:00:00",
                                "SalesMade": 20,
                                "MissingtoSell": 80
                            },
                            {
                                "Id": 100,
                                "Name": "Super Bomberman R",
                                "Category": {
                                    "Id": 8,
                                    "Name": "Nintendo Switch",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 8,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                                "Description": null,
                                "Value": 15,
                                "Images": [],
                                "UrlImage": "100.png",
                                "UrlImageBanner": "100-B.png",
                                "RaffleDate": "0001-01-01T00:00:00",
                                "SalesMade": 98,
                                "MissingtoSell": 2
                            }
                        ]
                    }

                break;
            case 'Recent':
                showcase = {
                    "Id": 2,
                    "Name": "Vitrine dos produtos recentes",
                    "ShowcaseType": 2,
                    "Products": [
                        {
                            "Id": 260,
                            "Name": "Resident Evil 2 Revelation",
                            "Category": {
                                "Id": 4,
                                "Name": "Xbox One",
                                "Departments": []
                            },
                            "Department": {
                                "Id": 4,
                                "Name": "Jogos"
                            },
                            "ShortDescription": "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            "Description": null,
                            "Value": 10,
                            "Images": [
                                {
                                    "Id": 8,
                                    "ImageType": 1,
                                    "URL": "10.png"
                                },
                                {
                                    "Id": 9,
                                    "ImageType": 3,
                                    "URL": "10-B.png"
                                }],
                            "UrlImage": "260.png",
                            "UrlImageBanner": "",
                            "RaffleDate": "",
                            "SalesMade": 79,
                            "MissingtoSell": 21
                        },
                        {
                            "Id": 261,
                            "Name": "Mortal Kombat XL",
                            "Category": {
                                "Id": 4,
                                "Name": "Xbox One",
                                "Departments": []
                            },
                            "Department": {
                                "Id": 4,
                                "Name": "Jogos"
                            },
                            "ShortDescription": "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                            "Description": null,
                            "Value": 5,
                            "Images": [
                                {
                                    "Id": 8,
                                    "ImageType": 1,
                                    "URL": "10.png"
                                },
                                {
                                    "Id": 9,
                                    "ImageType": 3,
                                    "URL": "10-B.png"
                                }],
                            "UrlImage": "261.png",
                            "UrlImageBanner": "",
                            "RaffleDate": "",
                            "SalesMade": 8,
                            "MissingtoSell": 92
                        },
                        {
                            "Id": 400,
                            "Name": "Switch 1 e 2",
                            "Category": {
                                "Id": 8,
                                "Name": "Nintendo Switch",
                                "Departments": []
                            },
                            "Department": {
                                "Id": 8,
                                "Name": "Jogos"
                            },
                            "ShortDescription": "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            "Description": null,
                            "Value": 10,
                            "Images": [],
                            "UrlImage": "400.png",
                            "UrlImageBanner": "",
                            "RaffleDate": "21/10/2017",
                            "SalesMade": 100,
                            "MissingtoSell": 0
                        },
                        {
                            "Id": 1,
                            "Name": "Watch Dogs 2",
                            "Category": {
                                "Id": 8,
                                "Name": "Nintendo Switch",
                                "Departments": []
                            },
                            "Department": {
                                "Id": 8,
                                "Name": "Jogos"
                            },
                            "ShortDescription": "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                            "Description": null,
                            "Value": 10,
                            "Images": [],
                            "UrlImage": "400.png",
                            "UrlImageBanner": "",
                            "RaffleDate": "21/10/2017",
                            "SalesMade": 100,
                            "MissingtoSell": 0
                        }
                    ]
                }
                break;
            case 'Popular':
                {
                    showcase = {
                        "Id": 3,
                        "Name": "Vitrine dos mais vendidos",
                        "ShowcaseType": 3,
                        "Products": [
                            {
                                "Id": 262,
                                "Name": "Minecraft",
                                "Category": {
                                    "Id": 4,
                                    "Name": "Xbox One",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 4,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                                "Description": null,
                                "Value": 15,
                                "Images": [],
                                "UrlImage": "262.png",
                                "UrlImageBanner": "",
                                "RaffleDate": "",
                                "SalesMade": 39,
                                "MissingtoSell": 61
                            },
                            {
                                "Id": 191,
                                "Name": "God of War 4",
                                "Category": {
                                    "Id": 1,
                                    "Name": "Playstation 4",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 1,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                                "Description": null,
                                "Value": 15,
                                "Images": [],
                                "UrlImage": "191.png",
                                "UrlImageBanner": "",
                                "RaffleDate": "",
                                "SalesMade": 98,
                                "MissingtoSell": 2
                            },
                            {
                                "Id": 192,
                                "Name": "Crash Bandicoot N Sane Trilogy",
                                "Category": {
                                    "Id": 1,
                                    "Name": "Playstation 4",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 1,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Super Bomberman R é um jogo de ação, desenvolvido pela Konami e HexaDrive. O jogo foi lançado em 3 de Março de 2017 como um dos títulos de lançamento para o Nintendo Switch",
                                "Description": null,
                                "Value": 15,
                                "Images": [],
                                "UrlImage": "192.png",
                                "UrlImageBanner": "",
                                "RaffleDate": "",
                                "SalesMade": 98,
                                "MissingtoSell": 2
                            },
                            {
                                "Id": 410,
                                "Name": "Has-Been Heroes",
                                "Category": {
                                    "Id": 8,
                                    "Name": "Nintendo Switch",
                                    "Departments": []
                                },
                                "Department": {
                                    "Id": 8,
                                    "Name": "Jogos"
                                },
                                "ShortDescription": "Gears of War 4 é um jogo de tiro em terceira pessoa produzido pelo estúdio canadense The Coalition. O quinto título da série Gears of War, foi publicado pela Microsoft Studios para Microsoft Windows e Xbox One em 11 de Outubro de 2016",
                                "Description": null,
                                "Value": 5,
                                "Images": [],
                                "UrlImage": "410.png",
                                "UrlImageBanner": "",
                                "RaffleDate": "23/10/2017",
                                "SalesMade": 100,
                                "MissingtoSell": 0
                            }
                        ]
                    }
                }
                break;
            default:

        }


        return showcase;

        //return $http.get(global.service + urlBase + '/' + identifier)
        //    .success(function (data) {
        //        return data;

        //    })
        //    .error(function (error, status) {
        //    });
    }


    return service;

});