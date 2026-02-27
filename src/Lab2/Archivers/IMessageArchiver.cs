using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archivers;

public interface IMessageArchiver
{
    ArchiveResult Archive(Message? message);
}
