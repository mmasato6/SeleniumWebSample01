Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Edge

Module Module1

    Sub Main()
        Using webdriver = CreateWebDriver()

            'googleに遷移
            webdriver.Url = "https://www.google.co.jp"

            'CSSセレクタを使って要素を取得
            Dim element = webdriver.FindElement(By.Name("q"))
            '取得した要素にテキストを入力してsubmit
            element.SendKeys("selenium")
            element.Submit()

            '結果を見たいので3秒待つ
            Thread.Sleep(TimeSpan.FromSeconds(3))
            'ブラウザ終了
            webdriver.Quit()
        End Using
    End Sub

    Private Function CreateWebDriver() As IWebDriver
        Dim edgeService = EdgeDriverService.CreateDefaultService()
        Return New EdgeDriver
    End Function
End Module
