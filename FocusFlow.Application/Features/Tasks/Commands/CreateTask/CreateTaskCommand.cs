using FocusFlow.Application.DTOs.Tasks;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(string Title, string Priority) : IRequest<TaskDto>;
