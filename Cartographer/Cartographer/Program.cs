using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartographer
{
    class Program
    {
        //https://stackoverflow.com/questions/899480/how-can-i-recursively-visit-links-without-revisiting-links
        //How to do this recursivley to maybe increase performance?
        static void Main(string[] args)
        {
            IWebDriver driver = new EdgeDriver();
            string pageToMap = "https://www.runescape.com/";
            List<String> visitedUrls = new List<string>();
            Queue<String> pageManifest = new Queue<string>();
            driver.Url = pageToMap;
            GetAllUrls(visitedUrls, pageManifest, pageToMap, driver);
            while (pageManifest.Count != 0)
            {
                driver.Url = pageManifest.Dequeue();
                visitedUrls.Add(driver.Url);
                GetAllUrls(visitedUrls, pageManifest, pageToMap, driver);
            }
        }
        private static void GetAllUrls(List<String> visitedUrls, Queue<String>pageManifest, String pageToMap, IWebDriver driver)
        {
            String basePage = pageToMap.Substring(12, pageToMap.Length - 12);
            ICollection<IWebElement> links = driver.FindElements(By.TagName("a"));
            String url;
            foreach (IWebElement link in links)
            {
                url = link.GetAttribute("href");
                try
                {
                    if (!visitedUrls.Contains(url) && !pageManifest.Contains(url) && url.Contains(basePage))
                    {
                        pageManifest.Enqueue(url);
                        Console.WriteLine(url);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
