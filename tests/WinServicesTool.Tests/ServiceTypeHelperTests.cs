using System.Diagnostics.CodeAnalysis;
using Shouldly;
using WinServicesTool.Models;
using Xunit;

namespace WinServicesTool.Tests;

[ExcludeFromCodeCoverage]
public sealed class ServiceTypeHelperTests
{
    [Fact]
    public void Describe_KnownType_ReturnsNonEmptyString()
    {
        // Arrange
        const int knownType = (int)ServiceTypeEx.FileSystemDriver;

        // Act
        var desc = ServiceTypeHelper.Describe(knownType);

        // Assert
        desc.ShouldNotBeNullOrEmpty();
    }
}
