using Application.Common.Models.CrawlerService;
using Application.Common.Models.Order;
using Application.Common.Models.Product;
using Application.Features.Orders.Commands.Add;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace CrawlerService
{
    public class Crawler
    {
        private readonly IWebDriver _driver;
        private readonly List<ProductDto> Products;
        private HubConnection _sendLogHubConnection;
        private HubConnection _orderHubConnection;
        private HttpClient _httpClient;
        private const string BASE_URL = "http://localhost:7008/";
        private string access_token;

        public Crawler(HttpClient httpClient)
        {
            _driver = new ChromeDriver();
            Products = new List<ProductDto>();
            _orderHubConnection = new HubConnectionBuilder()
                .WithUrl($"{BASE_URL}Hubs/OrderHub")
                .WithAutomaticReconnect()
                .Build();
            _httpClient = httpClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                await DisposeAsync();

            try
            {
                await _orderHubConnection.StartAsync();
                await _sendLogHubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting SignalR connection: {ex.Message}");
            }

            _orderHubConnection.On<WorkerServiceTokenDto>(SignalRKeys.Log.SendToken, (tokenDto) =>
            {
                access_token = tokenDto.AccessToken;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            });

            _orderHubConnection.On<OrderDto>(SignalRKeys.Order.Added, async (orderDto) =>
            {
                Products.Clear();
                await CrawlProductsAsync(orderDto);
            });
        }

        public async Task CrawlProductsAsync(OrderDto orderDto)
        {
            try
            {
                DateTime now = DateTime.Now;
                Console.WriteLine("Website logged in. - " + now.ToString("dd.MM.yyyy : HH:mm"));
                await _sendLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot started. " + now.ToString("dd.MM.yyyy : HH:mm")));

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
                    _driver.Navigate().GoToUrl($"https://4teker.net/?currentPage={page}");

                    IReadOnlyCollection<IWebElement> productElements = _driver.FindElements(By.CssSelector(".card"));

                    foreach (IWebElement productElement in productElements)
                    {
                        string name = productElement.FindElement(By.CssSelector(".product-name")).Text;
                        string price = productElement.FindElement(By.CssSelector(".price")).Text;
                        string picture = productElement.FindElement(By.CssSelector(".card-img-top")).GetAttribute("src");

                        price = price.Replace("$", "");

                        Match match = Regex.Match(picture, pattern);

                        bool isOnSale = productElement.FindElements(By.CssSelector(".onsale")).Count > 0;
                        count++;

                        if (isOnSale)
                        {
                            string onSalePrice = productElement.FindElement(By.CssSelector(".sale-price")).Text;
                            onSalePrice = onSalePrice.Replace("$", "");
                            Products.Add(new ProductDto { Name = name, Price = decimal.Parse(price), Picture = picture, IsOnSale = isOnSale, SalePrice = decimal.Parse(onSalePrice), OrderId = Guid.NewGuid() });
                        }
                        else
                        {
                            Products.Add(new ProductDto { Name = name, Price = decimal.Parse(price), Picture = picture, IsOnSale = isOnSale, OrderId = Guid.NewGuid() });
                        }

                        if (count == crawledProduct)
                        {
                            stopLoop = true;
                            break;
                        }
                    }
                    await _sendLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"Page {page} scanned. Total {Products.Count} products - " + now.ToString("dd.MM.yyyy : HH:mm")));
                    Console.WriteLine(($"Page {page} scanned. Total {Products.Count} products - " + now.ToString("dd.MM.yyyy : HH:mm")));
                }

                var totalProduct = Products.Count;
                Console.WriteLine(($"{totalProduct} products detected - " + now.ToString("dd.MM.yyyy : HH:mm")));
                await _sendLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{totalProduct} products detected - " + now.ToString("dd.MM.yyyy : HH:mm")));
                Console.ReadKey();

                _driver.Quit();

                foreach (ProductDto product in Products)
                {
                    Console.WriteLine($"Product Name: {product.Name}");
                    Console.WriteLine($"Product Price: {product.Price}");
                    Console.WriteLine($"Product Image: {product.Picture}");
                    Console.WriteLine($"Product Is On Sale: {product.IsOnSale}");
                    Console.WriteLine($"Product Id: {product.OrderId}");
                    if (product.IsOnSale)
                    {
                        Console.WriteLine($"Product On Sale Price: {product.SalePrice}");
                    }
                    Console.WriteLine("------------------------");
                }

                Console.WriteLine("Data scraping completed. - " + now.ToString("dd.MM.yyyy : HH:mm"));
                Console.WriteLine(("Data scraping completed. - " + now.ToString("dd.MM.yyyy : HH:mm")));

                await _sendLogHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"Data scraping completed. - " + now.ToString("dd.MM.yyyy : HH:mm")));

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task DisposeAsync()
        {
            _driver.Quit();
            await _sendLogHubConnection.DisposeAsync();
            await _orderHubConnection.DisposeAsync();
        }

        private LogDto CreateLog(string message)
        {
            return new LogDto(message);
        }
    }
}