using MediatR;

public record DeleteProductCommand(int id) : IRequest;
