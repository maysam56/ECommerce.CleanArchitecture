using MediatR;

public record UpgradeToVidCommand(int CustomerId) : IRequest; 