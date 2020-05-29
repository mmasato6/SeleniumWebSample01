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
                webDriver.Url = @"https://www.google.co.jp";
                // CSSセレクタを使って要素を取得
                IWebElement element = webDriver.FindElement(By.CssSelector("input[name=\"q\"]"));
                // 取得した要素にテキストを入力してsubmit
                element.SendKeys("selenium");
                element.Submit();
                // 結果を見たいので3秒待つ
                Thread.Sleep(TimeSpan.FromSeconds(3));
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
