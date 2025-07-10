using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Commands.Item
{
    public record CreateItemCommand(string? Name, double Price, string? Note, IFormFile? Image, int CategoryId) : IRequest<ItemDto>;
}
