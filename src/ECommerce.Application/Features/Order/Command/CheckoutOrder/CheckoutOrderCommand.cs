using ECommerce.Application.Features.Order.DTOs;
using MediatR;
public record CheckoutOrderCommand(CreateOrderDto dto) : IRequest<CheckoutResponseDto>;   