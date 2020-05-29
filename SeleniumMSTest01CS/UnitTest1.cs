using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace SeleniumMSTest01CS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (IWebDriver webDriver = CreateWebDriver())
            {
                // googleに遷移
                webDriver.Navigate().GoToUrl(@"https://www.google.co.jp");
                // CSSセレクタを使って要素を取得
                IWebElement element = webDriver.FindElement(By.CssSelector("input[name=\"q\"]"));
                // 取得した要素にテキストを入力してsubmit
                const string SearchWord = "selenium"; 
                element.SendKeys(SearchWord);
                element.Submit();
                /*
                //submitの直後ではURLもタイトルも変わっていない(=遷移が終わっていない(Seleniumの仕様？Edgeの仕様？Googleの仕様？))
                Console.WriteLine(webDriver.Url);
                Console.WriteLine(webDriver.Title);
                */

                //sleepしてからチェックすると遷移してるから、Submitが終わった後、ブラウザ側の処理が終わるまで待機する必要がある
                //Thread.Sleep(3000);
                //遷移が終わるまで最大3秒待機する
                const string expectedTitle = "selenium - Google 検索";
                //const string expectedTitle = "テストケース誤り";
                OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(webDriver, TimeSpan.FromSeconds(3));
                var transfered = wait.Until(e => e.Title == expectedTitle );
                /*
                Console.WriteLine(webDriver.Url);
                Console.WriteLine(webDriver.Title);
                */

                var screenShot = ((ITakesScreenshot)webDriver).GetScreenshot();
                // TODO:ヘルパーに切り出したほうがよさそう
                const string ScreenshotDirectoryRoot = "SeleniumSample";
                string screenShotDirectoryPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), ScreenshotDirectoryRoot,DateTime.Now.ToString("yyyyMMddHHmm"));
                if (!System.IO.Directory.Exists(screenShotDirectoryPath))
                {
                    System.IO.Directory.CreateDirectory(screenShotDirectoryPath);
                }
                string screenShotName = $"{this.GetType().Name}.{nameof(TestMethod1)}.01.png";
                string screenShotFullName = System.IO.Path.Combine(screenShotDirectoryPath,screenShotName);
                screenShot.SaveAsFile(screenShotFullName,ScreenshotImageFormat.Png);

                // 最初のリンクの文字列に検索語句が含まれていることを確認する
                var firstElement = webDriver.FindElement(By.ClassName("LC20lb")); //2020/05/29時点。
                string actualText = firstElement.Text;
                Console.WriteLine(actualText);
                Assert.IsTrue(actualText.ToLower().Contains(SearchWord), $"actual text:{actualText}");
                //Assert.IsTrue(actualText.ToLower().Contains("テストケース誤り"), $"actual text:{actualText}");

                // ブラウザ終了
                webDriver.Quit();
            }
        }

        private IWebDriver CreateWebDriver()
        {
            // Edge
            var EdgeService = EdgeDriverService.CreateDefaultService();
            return new EdgeDriver();
        }

    }
}
