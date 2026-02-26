using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class UserTests
{
    [Fact]
    public void Receive_Message_StatusIsUnread()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");
        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);

        // Act
        user.Receive(message);

        // Assert
        // Message is stored in user context with Unread status (verified by successful MarkAsRead)
        MarkAsReadResult result = user.MarkAsRead(message.Id);
        Assert.IsType<MarkAsReadResult.Success>(result);
    }

    [Fact]
    public void MarkAsRead_UnreadMessage_StatusChangesToRead()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");
        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);
        user.Receive(message);

        // Act
        MarkAsReadResult firstResult = user.MarkAsRead(message.Id);
        MarkAsReadResult secondResult = user.MarkAsRead(message.Id);

        // Assert
        Assert.IsType<MarkAsReadResult.Success>(firstResult);
        Assert.IsType<MarkAsReadResult.AlreadyRead>(secondResult);
    }

    [Fact]
    public void MarkAsRead_AlreadyReadMessage_ReturnsError()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");
        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);
        user.Receive(message);
        user.MarkAsRead(message.Id);

        // Act
        MarkAsReadResult markAsReadResult = user.MarkAsRead(message.Id);

        // Assert
        Assert.IsType<MarkAsReadResult.AlreadyRead>(markAsReadResult);
    }

    [Fact]
    public void MarkAsRead_NonExistentMessage_ReturnsNotFound()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");
        var nonExistentMessageId = Guid.NewGuid();

        // Act
        MarkAsReadResult result = user.MarkAsRead(nonExistentMessageId);

        // Assert
        Assert.IsType<MarkAsReadResult.NotFound>(result);
    }

    [Fact]
    public void Receive_NullMessage_ThrowsArgumentNullException()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");

        ReceiveResult result = user.Receive(null);

        // Act & Assert
        Assert.IsType<ReceiveResult.Failure>(result);
    }

    [Fact]
    public void Constructor_EmptyName_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), string.Empty));
    }

    [Fact]
    public void Constructor_EmptyId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new User(Guid.Empty, "Test User"));
    }
}
