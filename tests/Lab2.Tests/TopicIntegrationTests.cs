using Itmo.ObjectOrientedProgramming.Lab2.Recipients;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Topics;

using Itmo.ObjectOrientedProgramming.Lab2.Users;
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

        var filteredRecipient2 = new ImportanceFilterRecipientDecorator(
            userRecipient2,
            ImportanceLevel.High);

        var topic = new Topic("Test Topic");
        topic.AddRecipient(userRecipient1);
        topic.AddRecipient(filteredRecipient2);

        var lowMessage = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Low);

        // Act
        IReadOnlyCollection<ReceiveResult> receiveResult = topic.Send(lowMessage);

        // Assert
        Assert.IsType<ReceiveResult.Success>(receiveResult.ElementAt(0));
        Assert.IsType<ReceiveResult.Filtered>(receiveResult.ElementAt(1));
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
        MarkAsReadResult firstResult = user.MarkAsRead(message.Id);
        Assert.IsType<MarkAsReadResult.Success>(firstResult);
        MarkAsReadResult secondMarkResult = user.MarkAsRead(message.Id);
        Assert.IsType<MarkAsReadResult.AlreadyRead>(secondMarkResult);
    }

    [Fact]
    public void Topic_NullRecipient_ThrowsArgumentNullException()
    {
        // Arrange
        var topic = new Topic("Test Topic");

        // Act
        ArchiveResult result = topic.AddRecipient(null);

        // Assert
        Assert.IsType<ArchiveResult.Failure>(result);
    }

    [Fact]
    public void Topic_NullMessage_ThrowsArgumentNullException()
    {
        // Arrange
        var topic = new Topic("Test Topic");
        topic.AddRecipient(new Mock<IMessageRecipient>().Object);

        // Act
        IReadOnlyCollection<ReceiveResult> result = topic.Send(null);

        // Assert
        Assert.IsType<ReceiveResult.Failure>(result.ElementAt(0));
    }

    [Fact]
    public void Topic_EmptyName_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Topic(string.Empty));
    }
}
