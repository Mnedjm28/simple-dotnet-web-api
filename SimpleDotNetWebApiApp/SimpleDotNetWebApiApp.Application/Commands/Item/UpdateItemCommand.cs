using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Commands.Item
{
    public record UpdateItemCommand(int Id, string? Name, double Price, string? Note, IFormFile? Image, int CategoryId, bool IgnoreImage = false) : IRequest<ItemDto>;
}
