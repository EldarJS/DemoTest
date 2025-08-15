using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Playwright;

namespace DemoProject.Tests;

[TestFixture]
public class LoginTest
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
            Headless = true
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
    public async Task Login_ShouldShowInventory()
    {
        await _page.GotoAsync(BaseUrl);
        await _page.Locator("[data-test='username']").FillAsync(Username);
        await _page.Locator("[data-test='password']").FillAsync(Password);
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await _page.WaitForURLAsync(new Regex("inventory.html"));
        StringAssert.Contains("inventory.html", _page.Url);
    }
}
