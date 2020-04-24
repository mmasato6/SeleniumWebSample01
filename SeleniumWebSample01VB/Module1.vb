Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Edge

Module Module1

    Sub Main()
        Using webdriver = CreateWebDriver()
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
