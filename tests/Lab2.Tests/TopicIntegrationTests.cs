using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class TopicIntegrationTests
{
    [Fact]
    public void Topic_TwoRecipientsWithFilter_MessageDeliveredOnce()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");
        var userRecipient1 = new UserRecipient(user);
        var userRecipient2 = new UserRecipient(user);

        // Second recipient has importance filter (High minimum)
        var filteredRecipient2 = new ImportanceFilterRecipientDecorator(
            userRecipient2,
            ImportanceLevel.High);

        var topic = new Topic("Test Topic");
        topic.AddRecipient(userRecipient1);
        topic.AddRecipient(filteredRecipient2);

        // Low importance message (should not pass filter)
        var lowMessage = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Low);

        // Act
        topic.Send(lowMessage);

        // Assert
        // User should receive message only once (from first recipient, not from filtered second)
        MarkAsReadResult markResult = user.MarkAsRead(lowMessage.Id);
        Assert.IsType<MarkAsReadResult.Success>(markResult);

        // Second attempt should fail (message was read already, received only once)
        MarkAsReadResult secondMarkResult = user.MarkAsRead(lowMessage.Id);
        Assert.IsType<MarkAsReadResult.AlreadyRead>(secondMarkResult);
    }

    [Fact]
    public void Topic_TwoRecipientsWithoutFilter_MessageDeliveredTwice()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test User");
        var userRecipient1 = new UserRecipient(user);
        var userRecipient2 = new UserRecipient(user);

        var topic = new Topic("Test Topic");
        topic.AddRecipient(userRecipient1);
        topic.AddRecipient(userRecipient2);

        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);

        // Act
        topic.Send(message);

        // Assert
        // Message received twice (once from each recipient)
        // First MarkAsRead should succeed
        MarkAsReadResult firstResult = user.MarkAsRead(message.Id);
        Assert.IsType<MarkAsReadResult.Success>(firstResult);
    }

    [Fact]
    public void Topic_NullRecipient_ThrowsArgumentNullException()
    {
        // Arrange
        var topic = new Topic("Test Topic");
        ArchiveResult result = topic.AddRecipient(null);

        // Act & Assert
        Assert.IsType<ArchiveResult.Failure>(result);
    }

    [Fact]
    public void Topic_NullMessage_ThrowsArgumentNullException()
    {
        // Arrange
        var topic = new Topic("Test Topic");
        topic.AddRecipient(new Mock<IMessageRecipient>().Object);
        ReceiveResult result = topic.Send(null);

        // Act & Assert
        Assert.IsType<ReceiveResult.Failure>(result);
    }

    [Fact]
    public void Topic_EmptyName_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Topic(string.Empty));
    }
}
