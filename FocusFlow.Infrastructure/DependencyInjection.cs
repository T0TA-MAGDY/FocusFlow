using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Domain.Interfaces;
using FocusFlow.Infrastructure.Auth;
using FocusFlow.Infrastructure.Persistence;
using FocusFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FocusFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection is missing.");
        services.AddDbContext<FocusFlowDbContext>(o => o.UseSqlServer(conn));
        services.AddScoped<IUserRepository, UserRepository>(); services.AddScoped<ITaskRepository, TaskRepository>(); services.AddScoped<IFocusSessionRepository, FocusSessionRepository>(); services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtProvider, JwtProvider>(); services.AddScoped<IPasswordHasherService, PasswordHasherService>(); services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor(); services.Configure<JwtOptions>(options => configuration.GetSection(JwtOptions.SectionName).Bind(options));
        return services;
    }
}
