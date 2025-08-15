using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Playwright;


namespace DemoProject.Tests;

[TestFixture]
public class PurchaseTShirtTest
{
    private const string BaseUrl = "https://www.saucedemo.com/";
    private string Username => Environment.GetEnvironmentVariable("SAUCE_USERNAME") ?? "standard_user";
    private string Password => Environment.GetEnvironmentVariable("SAUCE_PASSWORD") ?? "secret_sauce";

    private IPlaywright _playwright = default!;
    private IBrowser _browser = default!;
    private IBrowserContext _context = default!;
    private IPage _page = default!;

    [SetUp]
    public async Task SetUp()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // показываем окно
        });
        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1280, Height = 800 }
        });
        _page = await _context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        try { if (_context != null) await _context.CloseAsync(); } catch {}
        try { if (_browser != null) await _browser.CloseAsync(); } catch {}
        try { _playwright?.Dispose(); } catch {}
    }

    [Test]
    public async Task Buy_SauceLabs_Bolt_TShirt_EndToEnd()
    {
        // Login
        await _page.GotoAsync(BaseUrl);
        await _page.Locator("[data-test='username']").FillAsync(Username);
        await _page.Locator("[data-test='password']").FillAsync(Password);
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await _page.WaitForURLAsync(new Regex("inventory.html"));

        // Open item by name
        var itemName = "Sauce Labs Bolt T-Shirt";
        await _page.Locator("[data-test='inventory-item-name']").Filter(new() { HasTextString = itemName }).ClickAsync();

        // Add to cart on product page
        await _page.Locator("[data-test='add-to-cart']").ClickAsync();
        await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "TestResults/added-to-cart.png", FullPage = true });

        // Go to cart
        await _page.Locator("[data-test='shopping-cart-link']").ClickAsync();
        await _page.WaitForURLAsync(new Regex("cart.html"));

        // Checkout
        await _page.Locator("[data-test='checkout']").ClickAsync();
        await _page.WaitForURLAsync(new Regex("checkout-step-one.html"));

        // Fill user data
        await _page.Locator("[data-test='firstName']").FillAsync("Eldars");
        await _page.Locator("[data-test='lastName']").FillAsync("Veromejs");
        await _page.Locator("[data-test='postalCode']").FillAsync("LV-1005");

        // Continue
        await _page.Locator("[data-test='continue']").ClickAsync();
        await _page.WaitForURLAsync(new Regex("checkout-step-two.html"));
    
        await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "TestResults/before-finish.png", FullPage = true });

        // Finish
        await _page.Locator("[data-test='finish']").ClickAsync();
        await _page.WaitForURLAsync(new Regex("checkout-complete.html"));

        await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "TestResults/order-complete.png", FullPage = true });
        StringAssert.Contains("checkout-complete.html", _page.Url);
    }
}
