namespace AutomationExercise.Tests;

using AutomationExercise.Common;
using OpenQA.Selenium;
using Xunit;

public class LoginPageTests : SharedBaseFixture
{
    [Fact]
    public void LoginPage_LoadsSuccessfully()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        Assert.Contains("login", _driver.Url.ToLower());
    }

    [Fact]
    public void LoginPage_HeadingIsDisplayed()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var loginHeading = _driver.FindElement(By.XPath("//h2[contains(text(),'Login to your account')]"));
        Assert.True(loginHeading.Displayed, "Login heading should be visible.");
    }

    [Fact]
    public void LoginPage_EmailInputIsPresent()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var emailInput = _driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
        Assert.True(emailInput.Displayed, "Login email input should be present.");
    }

    [Fact]
    public void LoginPage_PasswordInputIsPresent()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var passwordInput = _driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        Assert.True(passwordInput.Displayed, "Login password input should be present.");
    }

    [Fact]
    public void LoginPage_LoginButtonIsPresent()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var loginButton = _driver.FindElement(By.XPath("//button[@data-qa='login-button']"));
        Assert.True(loginButton.Displayed, "Login button should be present.");
    }

    [Fact]
    public void LoginPage_FormIsDisplayed()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var loginForm = _driver.FindElement(By.XPath("//form[@action='/login']"));
        Assert.True(loginForm.Displayed, "Login form should be displayed.");
    }

    [Fact]
    public void LoginPage_EmailInputIsEnabled()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var emailInput = _driver.FindElement(By.XPath("//input[@data-qa='login-email']"));
        Assert.True(emailInput.Enabled, "Email input should be enabled.");
    }

    [Fact]
    public void LoginPage_PasswordInputIsEnabled()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var passwordInput = _driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        Assert.True(passwordInput.Enabled, "Password input should be enabled.");
    }

    [Fact]
    public void LoginPage_PasswordInputTypeIsPassword()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var passwordInput = _driver.FindElement(By.XPath("//input[@data-qa='login-password']"));
        Assert.Equal("password", passwordInput.GetAttribute("type"));
    }

    [Fact]
    public void LoginPage_LoginButtonIsClickable()
    {
        // Arrange & Act
        NavigateToLoginOrSignUpPage();

        // Assert
        var loginButton = _driver.FindElement(By.XPath("//button[@data-qa='login-button']"));
        Assert.True(loginButton.Enabled, "Login button should be clickable.");
    }
}