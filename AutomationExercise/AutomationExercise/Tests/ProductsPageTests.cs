namespace AutomationExercise.Tests;

using AutomationExercise.Common;
using OpenQA.Selenium;

public class ProductsPageTests : SharedBaseFixture
{
    [Fact]
    public void ProductsPage_LoadsSuccessfully()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        Assert.Contains("products", _driver.Url.ToLower());
    }

    [Fact]
    public void ProductsPage_TitleIsCorrect()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        Assert.Contains("Automation Exercise", _driver.Title);
    }

    [Fact]
    public void ProductsPage_AllProductsHeadingIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var heading = _driver.FindElement(By.XPath("//h2[contains(text(),'All Products')]"));
        Assert.True(heading.Displayed, "All Products heading should be visible.");
    }

    [Fact]
    public void ProductsPage_ProductListIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var products = _driver.FindElements(By.XPath("//div[@class='productinfo text-center']"));
        Assert.True(products.Count > 0, "Product list should contain at least one product.");
    }

    [Fact]
    public void ProductsPage_SearchBoxIsPresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var searchBox = _driver.FindElement(By.XPath("//input[@id='search_product']"));
        Assert.True(searchBox.Displayed, "Search box should be present.");
    }

    [Fact]
    public void ProductsPage_SearchButtonIsPresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var searchButton = _driver.FindElement(By.XPath("//button[@id='submit_search']"));
        Assert.True(searchButton.Displayed, "Search button should be present.");
    }

    [Fact]
    public void ProductsPage_ViewProductLinksArePresent()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var viewProductLinks = _driver.FindElements(By.XPath("//a[contains(text(),'View Product')]"));
        Assert.True(viewProductLinks.Count > 0, "View Product links should be present.");
    }

    [Fact]
    public void ProductsPage_BrandsSectionIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        ScrollToBottom();

        // Assert
        var brandsHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Brands')]"));
        Assert.True(brandsHeading.Displayed, "Brands section should be visible.");
    }

    [Fact]
    public void ProductsPage_CategorySectionIsDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var categoryHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Category')]"));
        Assert.True(categoryHeading.Displayed, "Category section should be visible.");
    }

    [Fact]
    public void ProductsPage_ProductPricesAreDisplayed()
    {
        // Arrange & Act
        NavigateToAutomationExerciseWebsite();
        HandleCookieConsent();

        _driver.FindElement(By.XPath("//a[@href='/products']")).Click();
        WaitForPageToLoadInSeconds();

        // Assert
        var prices = _driver.FindElements(By.XPath("//h2[contains(text(),'Rs.')]"));
        Assert.True(prices.Count > 0, "Product prices should be displayed.");
    }

}
