namespace AutomationExercise.Tests;

using AutomationExercise.Common;
using OpenQA.Selenium;

public class HomePageTests : SharedBaseFixture
{
    [Fact]
    public void HomePage_LoadsSuccessfully()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        Assert.Contains("Automation Exercise", _driver.Title);
    }

    [Fact]
    public void HomePage_LogoIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var logo = _driver.FindElement(By.XPath("//img[@alt='Website for automation practice']"));
        Assert.True(logo.Displayed, "Logo should be displayed on homepage.");
    }

    [Fact]
    public void HomePage_NavigationMenuIsVisible()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var homeLink = _driver.FindElement(By.XPath("//a[contains(text(),'Home')]"));
        Assert.True(homeLink.Displayed, "Home link should be visible in navigation.");
    }

    [Fact]
    public void HomePage_ProductsLinkNavigatesToProductsPage()
    {
        // Arrange
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Act
        var productsLink = _driver.FindElement(By.XPath("//a[@href='/products']"));
        productsLink.Click();
        WaitForPageToLoadInSeconds(1);

        // Assert
        Assert.Contains("products", _driver.Url.ToLower());
    }

    [Fact]
    public void HomePage_FooterIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        ScrollToBottom();

        // Assert
        var footer = _driver.FindElement(By.XPath("//footer"));
        Assert.True(footer.Displayed, "Footer should be displayed.");
    }

    [Fact]
    public void HomePage_SubscriptionSectionExists()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        ScrollToBottom();

        // Assert
        var subscriptionHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Subscription')]"));
        Assert.True(subscriptionHeading.Displayed, "Subscription section should be visible.");
    }

    /// <summary>
    /// Test Case: Verify subscription section exists in footer
    /// Expected Result: Subscription heading and form are visible
    /// </summary>
    [Fact]
    public void HomePage_SubscriptionSectionInFooterExists()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        ScrollToBottom();

        // Assert
        var subscriptionHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Subscription')]"));
        Assert.True(subscriptionHeading.Displayed, "Subscription section should be visible in footer.");
    }

    /// <summary>
    /// Test Case: Verify copyright text is present in footer
    /// Expected Result: Copyright text is displayed
    /// </summary>
    [Fact]
    public void HomePage_CopyrightTextIsPresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        ScrollToBottom();

        // Assert
        var footer = _driver.FindElement(By.XPath("//footer"));
        Assert.Contains("Copyright", footer.Text);
    }

    [Fact]
    public void HomePage_SubscriptionEmailInputExists()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        ScrollToBottom();

        // Assert
        var emailInput = _driver.FindElement(By.XPath("//footer//input[@type='email']"));
        Assert.True(emailInput.Displayed, "Subscription email input should be present.");
    }

    [Fact]
    public void HomePage_UrlIsCorrect()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        Assert.NotNull(_driver.Url);
        Assert.NotEmpty(_driver.Url);
        Assert.Equal(Constants.AutomationExerciseWebsiteUrl, _driver.Url);
    }

    [Fact]
    public void HomePage_CartLinkIsClickable()
    {
        // Arrange
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Act
        var cartLink = _driver.FindElement(By.XPath("//a[@href='/view_cart']"));
        cartLink.Click();
        WaitForPageToLoadInSeconds(1);

        // Assert
        Assert.Contains("view_cart", _driver.Url);
    }

    [Fact]
    public void HomePage_ContactUsLinkIsPresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var contactLink = _driver.FindElement(By.XPath("//a[contains(text(),'Contact us')]"));
        Assert.True(contactLink.Displayed, "Contact Us link should be visible.");
    }

    [Fact]
    public void HomePage_CategorySectionExists()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var categoryHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Category')]"));
        Assert.True(categoryHeading.Displayed, "Category section should be displayed.");
    }

    [Fact]
    public void HomePage_FeaturedItemsSectionIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var featuredHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Features Items')]"));
        Assert.True(featuredHeading.Displayed, "Featured Items section should be visible.");
    }

    [Fact]
    public void HomePage_ProductsAreDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();
        WaitForPageToLoadInSeconds(1);

        // Assert
        var products = _driver.FindElements(By.XPath("//div[@class='productinfo text-center']"));
        Assert.True(products.Count > 0, "At least one product should be displayed.");
    }

    [Fact]
    public void HomePage_CarouselSliderIsPresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var carousel = _driver.FindElement(By.XPath("//div[@id='slider-carousel']"));
        Assert.True(carousel.Displayed, "Carousel slider should be present.");
    }

    [Fact]
    public void HomePage_SignupLoginLinkIsVisible()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var signupLink = _driver.FindElement(By.XPath("//a[contains(text(),'Signup / Login')]"));
        Assert.True(signupLink.Displayed, "Signup/Login link should be visible.");
    }

    [Fact]
    public void ProductsPage_SearchBoxIsPresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Navigate to products page
        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds(1);

        // Assert
        var searchBox = _driver.FindElement(By.XPath("//input[@id='search_product']"));
        Assert.True(searchBox.Displayed, "Search box should be present on products page.");
    }

    [Fact]
    public void HomePage_TestCasesLinkIsClickable()
    {
        // Arrange
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Act
        var testCasesLink = _driver.FindElement(By.XPath("//a[@href='/test_cases']"));
        testCasesLink.Click();
        WaitForPageToLoadInSeconds(1);

        // Assert
        Assert.Contains("test_cases", _driver.Url);
    }

    [Fact]
    public void HomePage_MainContentAreaExists()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        // Assert
        var mainContent = _driver.FindElement(By.XPath("//div[@class='container']"));
        Assert.True(mainContent.Displayed, "Main content area should be displayed.");
    }
}
