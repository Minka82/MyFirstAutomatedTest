namespace AutomationExercise.Common;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public abstract class SharedBaseFixture : IDisposable
{
    #region Fields

    protected readonly IWebDriver _driver;
    protected readonly WebDriverWait _wait;

    // By => Provides a mechanism by which to find elements within a document.
    private readonly By _consentButtonLocator;
    private readonly Func<IWebDriver, IWebElement?> _consentCanBeClicked;

    #endregion

    #region Constructor

    protected SharedBaseFixture()
    {
        // Initialize Chrome-specific options for the browser instance.
        var options = new ChromeOptions();

        // Set the browser to launch in a maximized window for consistent testing across different resolutions.
        options.AddArgument(Constants.MaximizedWindow);

        // Instantiate the WebDriver (the browser interface) using the configured options.
        // _driver is the main object used to interact with the web page.
        _driver = new ChromeDriver(options);

        // Set the Implicit Wait timeout. This tells the driver how long to wait 
        // when trying to find an element before throwing a 'NoSuchElementException'
        _driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWaitInSeconds;

        // Initialize the Explicit Wait object, which is used for specific conditions (like element clickable).
        // This object polls the DOM for the condition for the specified duration.
        _wait = new WebDriverWait(_driver, Constants.ExplicitWaitInSeconds);

        // Initialize the private readonly 'By' locator field for the consent button.
        // This centralizes the XPath string for easy maintenance.
        _consentButtonLocator = By.XPath(Constants.ConsentXPath);

        // Initialize the Func delegate (Expected Condition). This pre-defines the specific wait condition to check if the element located by _consentButtonLocator is visible and enabled.
        _consentCanBeClicked = ExpectedConditions.ElementToBeClickable(_consentButtonLocator);
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Navigates to the base URL
    /// </summary>
    protected void NavigateToAutomationExerciseWebsite()
    {
        _driver.Navigate().GoToUrl(Constants.AutomationExerciseWebsiteUrl);
    }

    /// <summary>
    /// Navigates to the Login/Signup page
    /// </summary>
    protected void NavigateToLoginOrSignUpPage()
    {
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();
        WaitForPageToLoadInSeconds(2); // Extra wait to ensure page is stable

        _driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]")).Click();
        WaitForPageToLoadInSeconds(1);
    }


    /// <summary>
    /// Handles cookie consent dialog if present.
    /// Waits for the consent button and clicks it to dismiss the overlay.
    /// If dialog doesn't appear, continues with the test.
    /// </summary>
    protected void HandleCookieConsent()
    {
        try
        {
            IWebElement? consentButton = _wait.Until(_consentCanBeClicked);

            consentButton.Click();

            // Wait for overlay to disappear
            Thread.Sleep(Constants.ConsentOverlayWaitTimeInMs); 
        }
        catch (WebDriverTimeoutException)
        {
            // Cookie dialog not present or already dismissed
        }
    }

    // <summary>
    /// Scrolls to the bottom of the page
    /// </summary>
    protected void ScrollToBottom()
    {
        ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

        WaitForPageToLoadInSeconds();
    }

    /// <summary>
    /// Scrolls to the top of the page
    /// </summary>
    protected void ScrollToTop()
    {
        ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, 0);");

        WaitForPageToLoadInSeconds();
    }

    /// <summary>
    /// Scrolls to a specific element
    /// </summary>
    /// <param name="element">Element to scroll to</param>
    protected void ScrollToElement(IWebElement element)
    {
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

        WaitForPageToLoadInMs(500);
    }

    /// <summary>
    /// Waits for page to load
    /// </summary>
    /// <param name="seconds">Time to wait in seconds (default: 1)</param>
    protected void WaitForPageToLoadInSeconds(int seconds = 1)
    {
        Thread.Sleep(seconds * 1000);
    }

    /// <summary>
    /// Waits for page to load
    /// </summary>
    /// <param name="seconds">Time to wait in milliseconds (default: 1000)</param>
    protected void WaitForPageToLoadInMs(int milliseconds = 1000)
    {
        Thread.Sleep(milliseconds);
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    #endregion
}