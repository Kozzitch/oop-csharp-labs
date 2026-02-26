using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class RecipientDecoratorTests
{
    [Fact]
    public void ImportanceFilter_LowImportance_MessageNotDelivered()
    {
        // Arrange
        var mockRecipient = new Mock<IMessageRecipient>();
        var decorator = new ImportanceFilterRecipientDecorator(mockRecipient.Object, ImportanceLevel.High);
        var lowMessage = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Low);

        // Act
        decorator.Receive(lowMessage);

        // Assert
        mockRecipient.Verify(r => r.Receive(It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public void ImportanceFilter_HighImportance_MessageDelivered()
    {
        // Arrange
        var mockRecipient = new Mock<IMessageRecipient>();
        var decorator = new ImportanceFilterRecipientDecorator(mockRecipient.Object, ImportanceLevel.Low);
        var highMessage = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.High);

        // Act
        decorator.Receive(highMessage);

        // Assert
        mockRecipient.Verify(r => r.Receive(highMessage), Times.Once);
    }

    [Fact]
    public void LoggingDecorator_Receive_LogsMessage()
    {
        // Arrange
        var mockRecipient = new Mock<IMessageRecipient>();
        var mockLogger = new Mock<ILogger>();
        var decorator = new LoggingRecipientDecorator(mockRecipient.Object, mockLogger.Object);
        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);

        // Act
        decorator.Receive(message);

        // Assert
        mockLogger.Verify(l => l.Log(It.IsAny<string>()), Times.Once);
        mockRecipient.Verify(r => r.Receive(message), Times.Once);
    }

    [Fact]
    public void LoggingDecorator_NullLogger_ThrowsArgumentNullException()
    {
        // Arrange
        var mockRecipient = new Mock<IMessageRecipient>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new LoggingRecipientDecorator(mockRecipient.Object, null));
    }

    [Fact]
    public void ImportanceFilterDecorator_NullDecoratee_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ImportanceFilterRecipientDecorator(null, ImportanceLevel.Normal));
    }
}