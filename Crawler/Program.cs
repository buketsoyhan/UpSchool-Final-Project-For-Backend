using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();

        List<Product> products = new List<Product>();

        for (int page = 1; page <= 10; page++)
        {
            driver.Navigate().GoToUrl($"https://finalproject.dotnet.gg/?currentPage={page}");

            IReadOnlyCollection<IWebElement> productElements = driver.FindElements(By.CssSelector(".card"));

            foreach (IWebElement productElement in productElements)
            {
                string name = productElement.FindElement(By.CssSelector(".product-name")).Text;
                string price = productElement.FindElement(By.CssSelector(".price")).Text;
                string imageLocation = productElement.FindElement(By.CssSelector(".card-img-top")).GetAttribute("src");

                products.Add(new Product { Name = name, Price = price, ImageLocation = imageLocation });
            }
        }

        driver.Quit();

        foreach (Product product in products)
        {
            Console.WriteLine($"Product Name: {product.Name}");
            Console.WriteLine($"Product Price: {product.Price}");
            Console.WriteLine($"Product Image: {product.ImageLocation}");
            Console.WriteLine("------------------------");
        }

        Console.ReadLine();
    }
}

class Product
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string ImageLocation { get; set; }
}