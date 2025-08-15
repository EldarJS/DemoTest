# DemoProject (C# + Playwright + NUnit)

## Что делает
- **LoginTest**: логинится и проверяет, что попали на inventory.
- **PurchaseTShirtTest**: логинится, открывает товар *Sauce Labs Bolt T-Shirt*, добавляет в корзину, оформляет заказ с данными `Eldars Veromejs, LV-1005` и делает скриншоты.

## Запуск
1. Установи .NET 8 SDK.
2. В терминале перейди в папку проекта:
   ```powershell
   cd "<ПУТЬ>/DemoProject"
   ```
3. Установи Playwright CLI и браузеры (хватает Chromium):
   ```powershell
   dotnet tool install --global Microsoft.Playwright.CLI
   playwright install
   ```
4. (Опционально) задать логин/пароль:
   ```powershell
   $env:SAUCE_USERNAME="standard_user"
   $env:SAUCE_PASSWORD="secret_sauce"
   ```
5. Запуск тестов:
   ```powershell
   dotnet restore
   dotnet test
   ```

Скрины лежат в `TestResults/`.
