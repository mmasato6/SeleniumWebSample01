using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;

namespace SeleniumWebSample01CS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver webDriver = CreateWebDriver())
            {
                Console.WriteLine("googleを表示するよ");
                // googleに遷移
                webDriver.Url = @"https://www.google.co.jp";
                //IEだとset_Urlで固まってタイムアウトして例外が起きる。IE事態は画面遷移しているけど・・・
                Console.WriteLine("googleを表示したよ");

                // CSSセレクタを使って要素を取得
                IWebElement element = webDriver.FindElement(By.CssSelector("#lst-ib"));
                // 取得した要素にテキストを入力してsubmit
                element.SendKeys("selenium");
                element.Submit();

                // 結果を見たいので3秒待つ
                Thread.Sleep(TimeSpan.FromSeconds(3));

                // ブラウザ終了
                webDriver.Quit();
            }
        }

        private static IWebDriver CreateWebDriver()
        {
            // Edge
            var EdgeService = EdgeDriverService.CreateDefaultService();
            return new EdgeDriver();
            ////IE
            //var IEService = InternetExplorerDriverService.CreateDefaultService();
            //return new InternetExplorerDriver(IEService);
        }
    }
}
