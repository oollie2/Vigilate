using Vigilate.Core;

namespace Vigilate.Tests;

[TestFixture]
public class SettingsTests
{
    private const string TestSettingsFile = "test-settings.json";

    [SetUp]
    public void Setup()
    {
        // Clean up the test settings file before each test
        if (File.Exists(TestSettingsFile))
            File.Delete(TestSettingsFile);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the test settings file after each test
        if (File.Exists(TestSettingsFile))
            File.Delete(TestSettingsFile);
    }

    [Test]
    public async Task Read_ExistingSettingsFile_ReturnsTrue()
    {
        // Arrange
        File.WriteAllText(TestSettingsFile, "{}");

        // Act
        bool result = await Settings<object>.Read(TestSettingsFile);

        // Assert
        Assert.That(result);
    }

    [Test]
    public async Task Read_NonExistingSettingsFile_ReturnsTrue()
    {
        // Act
        bool result = await Settings<object>.Read(TestSettingsFile);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Save_SettingsFileExists_ReturnsTrue()
    {
        // Arrange
        await Settings<object>.Read(TestSettingsFile);

        // Act
        bool result = await Settings<object>.Save();

        // Assert
        Assert.That(result);
    }

    [Test]
    public async Task Save_SettingsFileDoesNotExist_ReturnsTrue()
    {
        // Act
        bool result = await Settings<object>.Save();

        // Assert
        Assert.That(result);
    }
}