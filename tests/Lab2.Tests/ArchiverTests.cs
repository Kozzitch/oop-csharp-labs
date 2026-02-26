using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class ArchiverTests
{
    [Fact]
    public void FormattingArchiver_Archive_CallsFormatterAndInnerArchiver()
    {
        // Arrange
        var mockFormatter = new Mock<IMessageFormatter>();
        var mockInnerArchiver = new Mock<IMessageArchiver>();
        var formattingArchiver = new FormattingArchiver(mockFormatter.Object, mockInnerArchiver.Object);
        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);

        mockFormatter.Setup(f => f.Format(message)).Returns(new FormatResult.Success("# Header\n\nBody", message));

        // Act
        formattingArchiver.Archive(message);

        // Assert
        mockFormatter.Verify(f => f.Format(message), Times.Once);
        mockInnerArchiver.Verify(a => a.Archive(message), Times.Once);
    }

    [Fact]
    public void FormattingArchiver_NullFormatter_ThrowsArgumentNullException()
    {
        // Arrange
        var mockInnerArchiver = new Mock<IMessageArchiver>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new FormattingArchiver(null, mockInnerArchiver.Object));
    }

    [Fact]
    public void FormattingArchiver_NullInnerArchiver_ThrowsArgumentNullException()
    {
        // Arrange
        var mockFormatter = new Mock<IMessageFormatter>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new FormattingArchiver(mockFormatter.Object, null));
    }

    [Fact]
    public void FormattingArchiver_NullMessage_ThrowsArgumentNullException()
    {
        // Arrange
        var mockFormatter = new Mock<IMessageFormatter>();
        var mockInnerArchiver = new Mock<IMessageArchiver>();
        var formattingArchiver = new FormattingArchiver(mockFormatter.Object, mockInnerArchiver.Object);
        ArchiveResult result = formattingArchiver.Archive(null);

        // Act & Assert
        Assert.IsType<ArchiveResult.Failure>(result);
    }

    [Fact]
    public void InMemoryArchiver_Archive_MessageStoredInCollection()
    {
        // Arrange
        var archiver = new InMemoryArchiver();
        var message = new Message(Guid.NewGuid(), "Header", "Body", ImportanceLevel.Normal);

        // Act
        archiver.Archive(message);

        // Assert
        IReadOnlyCollection<Message> messages = archiver.GetMessages();
        Assert.Single(messages);
        Assert.Contains(messages, m => m.Id == message.Id);
    }

    [Fact]
    public void InMemoryArchiver_NullMessage_ThrowsArgumentNullException()
    {
        // Arrange
        var archiver = new InMemoryArchiver();

        ArchiveResult result = archiver.Archive(null);

        // Act & Assert
        Assert.IsType<ArchiveResult.Failure>(result);
    }
}
