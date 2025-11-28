namespace AutomationExercise.Tests;

using AutomationExercise.Common;
using OpenQA.Selenium;
using Xunit;

public class SignupPageTests : SharedBaseFixture
{
    [Fact]
    public void SignupPage_LoadsSuccessfully()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        Assert.Contains("login", _driver.Url.ToLower());
    }

    [Fact]
    public void SignupPage_HeadingIsDisplayed()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var signupHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'New User Signup!')]"));
        Assert.True(signupHeading.Displayed, "Signup heading should be visible.");
    }

    [Fact]
    public void SignupPage_NameInputIsPresent()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var nameInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));
        Assert.True(nameInput.Displayed, "Signup name input should be present.");
    }

    [Fact]
    public void SignupPage_EmailInputIsPresent()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var emailInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
        Assert.True(emailInput.Displayed, "Signup email input should be present.");
    }

    [Fact]
    public void SignupPage_SignupButtonIsPresent()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var signupButton = _driver.FindElement(By.XPath("//button[@data-qa='signup-button']"));
        Assert.True(signupButton.Displayed, "Signup button should be present.");
    }

    [Fact]
    public void SignupPage_NameInputIsEnabled()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var nameInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));
        Assert.True(nameInput.Enabled, "Name input should be enabled.");
    }

    [Fact]
    public void SignupPage_EmailInputIsEnabled()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var emailInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
        Assert.True(emailInput.Enabled, "Email input should be enabled.");
    }

    [Fact]
    public void SignupPage_SignupButtonIsClickable()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var signupButton = _driver.FindElement(By.XPath("//button[@data-qa='signup-button']"));
        Assert.True(signupButton.Enabled, "Signup button should be clickable.");
    }

    [Fact]
    public void SignupPage_NameInputHasPlaceholder()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var nameInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-name']"));
        var placeholder = nameInput.GetAttribute("placeholder");
        Assert.NotNull(placeholder);
        Assert.NotEmpty(placeholder);
    }

    [Fact]
    public void SignupPage_EmailInputTypeIsEmail()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var emailInput = _driver.FindElement(By.XPath("//input[@data-qa='signup-email']"));
        Assert.Equal("email", emailInput.GetAttribute("type"));
    }
}