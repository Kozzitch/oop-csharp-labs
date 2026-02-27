namespace Itmo.ObjectOrientedProgramming.Lab2.ValueObjects;

public record Message(
    Guid Id,
    string Header,
    string Body,
    ImportanceLevel Importance);