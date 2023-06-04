using Application.Features.Orders.Commands.Add;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net.Http;
using System.Text.RegularExpressions;

using var httpClient = new HttpClient();

Thread.Sleep(5000);
Console.WriteLine("Pless any key to open crawler console app...");
Console.ReadKey();
var connectionHubAddress= "https://localhost:7008/Hubs/SeleniumLogHub";


var hubConnection = new HubConnectionBuilder()
                .WithUrl(connectionHubAddress)
                .WithAutomaticReconnect()
                .Build();

await hubConnection.StartAsync();

try
{
    IWebDriver driver = new ChromeDriver();

    DateTime now = DateTime.Now;

    Console.WriteLine("Website logged in. - " + now.ToString("dd.MM.yyyy : HH:mm"));
    //Eğer log basmazsa async await yapmayı dene!!
    //hubConnection.InvokeAsync<bool>("SendLogNotificationAsync", CreateLog("Website logged in. - " + now.ToString("dd.MM.yyyy : HH:mm")));
    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot started. "+ now.ToString("dd.MM.yyyy : HH:mm")));

    List<Product> products = new List<Product>();
    string pattern = @"(\d+)";

    int crawledProduct = 0;
    int count = 0;
    int answer = 0;


    bool validInput = true;
    bool stopLoop = false;

    var orderAddRequest = new OrderAddCommand();

    while (validInput)
    {
        Console.WriteLine("How many items do you want to crawl?");
        string input = Console.ReadLine();

        try
        {
            crawledProduct = int.Parse(input);
            validInput = false;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please indicate how many items you would like.");
        }
    }

    while (answer != 1 && answer != 2 && answer != 3)
    {
        Console.WriteLine("What products do you want to crawl?");
        Console.WriteLine("1) All, 2) On Sale, 3) Regular Price Products");
        string input = Console.ReadLine();
        if (int.TryParse(input, out answer))
        {
            if (answer == 1)
            {
                orderAddRequest = new OrderAddCommand()
                {
                    Id = Guid.NewGuid(),
                    ProductCrawlType = ProductCrawlType.All,
                };
            }
            else if (answer == 2)
            {
                orderAddRequest = new OrderAddCommand()
                {
                    Id = Guid.NewGuid(),
                    ProductCrawlType = ProductCrawlType.OnDiscount,
                };
            }
            else if (answer == 3)
            {
                orderAddRequest = new OrderAddCommand()
                {
                    Id = Guid.NewGuid(),
                    ProductCrawlType = ProductCrawlType.NonDiscount,
                };
            }
            else
            {
                Console.WriteLine("You entered an invalid option. Try again.");
            }
        }
        else
        {
            Console.WriteLine("You have entered an invalid entry. Please enter a numeric value.");
        }
    }

    for (int page = 1; page <= 10 && !stopLoop; page++)
    {
        driver.Navigate().GoToUrl($"https://finalproject.dotnet.gg/?currentPage={page}");

        IReadOnlyCollection<IWebElement> productElements = driver.FindElements(By.CssSelector(".card"));

        foreach (IWebElement productElement in productElements)
        {
            string name = productElement.FindElement(By.CssSelector(".product-name")).Text;
            string price = productElement.FindElement(By.CssSelector(".price")).Text;
            string picture = productElement.FindElement(By.CssSelector(".card-img-top")).GetAttribute("src");

            price = price.Replace("$", "");

            Match match = Regex.Match(picture, pattern);

            bool isOnSale = productElement.FindElements(By.CssSelector(".onsale")).Count > 0;
            count++;

            if (isOnSale == true)
            {
                string onSalePrice = productElement.FindElement(By.CssSelector(".sale-price")).Text;
                onSalePrice = onSalePrice.Replace("$", "");
                products.Add(new Product { Name = name, Price = decimal.Parse(price), Picture = picture, IsOnSale = isOnSale, SalePrice = decimal.Parse(onSalePrice), OrderId = Guid.NewGuid() });
            }

            else
            {
                products.Add(new Product { Name = name, Price = decimal.Parse(price), Picture = picture, IsOnSale = isOnSale, OrderId = Guid.NewGuid() });
            }

            if (count == crawledProduct)
            {
                stopLoop=true;
                break;
            }
        }
        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"Page {page} scanned. Total {products.Count} products - " + now.ToString("dd.MM.yyyy : HH:mm")));
        Console.WriteLine(($"Page {page} scanned. Total {products.Count} products - " + now.ToString("dd.MM.yyyy : HH:mm")));

    }
    var totalProduct = products.Count;
    Console.WriteLine(($"{totalProduct} products detected - " + now.ToString("dd.MM.yyyy : HH:mm")));
    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{totalProduct} products detected - " + now.ToString("dd.MM.yyyy : HH:mm")));
    Console.ReadKey();


    driver.Quit();

    foreach (Product product in products)
    {
        Console.WriteLine($"Product Name: {product.Name}");
        Console.WriteLine($"Product Price: {product.Price}");
        Console.WriteLine($"Product Image: {product.Picture}");
        Console.WriteLine($"Product Is On Sale: {product.IsOnSale}");
        Console.WriteLine($"Product Id: {product.OrderId}");
        if (product.IsOnSale == true)
        {
            Console.WriteLine($"Product On Sale Price: {product.SalePrice}");
        }
        Console.WriteLine("------------------------");
    }

    Console.WriteLine("Data scraping completed. - " + now.ToString("dd.MM.yyyy : HH:mm"));
    Console.WriteLine(("Data scraping completed. - " + now.ToString("dd.MM.yyyy : HH:mm")));

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"Data scraping completed. - " + now.ToString("dd.MM.yyyy : HH:mm")));

    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
SeleniumLogDto CreateLog(string message) => new SeleniumLogDto(message);
