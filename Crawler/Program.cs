using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://finalproject.dotnet.gg/");

        bool hasNextPage = true;

        while (hasNextPage)
        {
            var products = driver.FindElements(By.CssSelector(".card"));

            foreach (var product in products)
            {
                var name = product.FindElement(By.CssSelector(".product-name")).Text;
                //var price = product.FindElement(By.CssSelector(".price")).Text;
                var imageUrl = product.FindElement(By.CssSelector(".card-img-top")).GetAttribute("src");

                Console.WriteLine($"Product Name: {name}");
                //Console.WriteLine($"Price: {price}");
                Console.WriteLine($"Image URL: {imageUrl}");
            }

           
            try
            {
                var nextPageLink = driver.FindElement(By.CssSelector(".next-page"));
                //if (nextPageLink.Enabled)
                //{
                //    nextPageLink.Click();
                //}
                //else
                //{
                //    hasNextPage = false;
                //}
                if (nextPageLink is null) 
                {
                    hasNextPage = false;
                }

            }
            catch (NoSuchElementException)
            {
                hasNextPage = false;
            }

            driver.Quit();
        }
    }
}