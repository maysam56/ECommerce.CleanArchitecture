using MediatR;

public record CancelOrderCommand(int id) : IRequest;