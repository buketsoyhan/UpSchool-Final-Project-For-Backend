using Domain.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            IWebDriver driver = new ChromeDriver();

            DateTime now = DateTime.Now;
            Console.WriteLine("Website logged in. - " + now.ToString("dd.MM.yyyy : HH:mm"));
            List<Product> products = new List<Product>();
            string pattern = @"(\d+)";
            string orderId;

            Console.WriteLine("How many items do you want to crawl?");
            int crawledProduct = int.Parse(Console.ReadLine());

            for (int page = 1; page <= 10; page++)
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

                    if (match.Success)
                    {
                        orderId = match.Value;
                    }
                    else
                    {
                        orderId = Guid.NewGuid().ToString();
                    }

                    bool isOnSale = productElement.FindElements(By.CssSelector(".onsale")).Count > 0;

                    if (isOnSale == true)
                    {
                        string onSalePrice = productElement.FindElement(By.CssSelector(".sale-price")).Text;
                        onSalePrice = onSalePrice.Replace("$", "");

                        products.Add(new Product { Name = name, Price = decimal.Parse(price), Picture = picture, IsOnSale = isOnSale, SalePrice = decimal.Parse(onSalePrice), OrderId = Guid.Parse(orderId) });

                    }

                    else
                    {
                        products.Add(new Product { Name = name, Price = decimal.Parse(price), Picture = picture, IsOnSale = isOnSale, OrderId = Guid.Parse(orderId)});

                    }
                }

                Console.WriteLine(($"Page {page} scanned. Total {products.Count} products - " + now.ToString("dd.MM.yyyy : HH:mm")));

            }
            var totalProduct = products.Count;
            Console.WriteLine(($"{totalProduct} products detected - " + now.ToString("dd.MM.yyyy : HH:mm")));

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

            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}


