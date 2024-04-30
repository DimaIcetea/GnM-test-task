using System;
using dotenv.net;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace LinkedInAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!IsFirstRun())
            {
                Console.WriteLine("Программа уже была запущена. Продолжение выполнения...");
            }
            else
            {
                Console.WriteLine("Программа запущена впервые. Enter для продолжения");
                Console.ReadLine();
                CreateFirstRunFile();
            }

            Credentials credentials = LoadCredentials("credentials.json");

            string driverPath = @"C:\Program Files\Google\Chrome\Application\chromedriver.exe";

            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            
            using (var driver = new ChromeDriver(driverPath, options))
            {
                driver.Navigate().GoToUrl("https://www.linkedin.com/login");
                driver.FindElement(By.Id("username")).SendKeys(credentials.Username);
                driver.FindElement(By.Id("password")).SendKeys(credentials.Password);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                System.Threading.Thread.Sleep(2000);

                IWebElement profileInfoElement = driver.FindElement(By.CssSelector("div.t-16.t-black.t-bold"));

                string profileName = profileInfoElement.Text.Trim();

                Console.WriteLine($"Имя профиля:: {profileName}");

                IWebElement profileLink = null;
                try
                {
                    profileLink = driver.FindElement(By.XPath($"//div[text()='{profileName}']"));
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Error: Профиль не найден");
                    return;
                }

                string profileUrl = profileLink.FindElement(By.XPath("./ancestor::a")).GetAttribute("href");
                driver.Navigate().GoToUrl(profileUrl);

                System.Threading.Thread.Sleep(7000);

                IWebElement backgroundImage = driver.FindElement(By.Id("profile-background-image-target-image"));
                string backgroundImageUrl = backgroundImage.GetAttribute("src");

                string savePath = "images\\background_image.jpg";
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(new Uri(backgroundImageUrl), savePath);
                }
                Console.WriteLine("Фон профиля скачан");

                IWebElement profileImageElement = driver.FindElement(By.CssSelector("img.profile-photo-edit__preview"));
                string profileImageUrl = profileImageElement.GetAttribute("src");
                savePath = "images\\profile_image.jpg";
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(new Uri(profileImageUrl), savePath);
                }
                Console.WriteLine("Аватарка профиля скачана");
            }
        }
        static Credentials LoadCredentials(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Credentials>(json);
        }
        static bool IsFirstRun()
        {
            return !File.Exists("firstrun.txt");
        }
        static void CreateFirstRunFile()
        {
            File.Create("firstrun.txt").Close();
        }


    }
    }
public class Credentials
{
    public string Username { get; set; }
    public string Password { get; set; }
}


