using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScreenplayApp.API.Services;
using ScreenplayApp.Application.Handlers.CommandHandlers;
using ScreenplayApp.Core.Repositories;
using ScreenplayApp.Core.Repositories.Base;
using ScreenplayApp.Core.Services;
using ScreenplayApp.Infrastructure.Data;
using ScreenplayApp.Infrastructure.Repositories;
using ScreenplayApp.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ScreenplayApp.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Handlers
            services.AddMediatR(typeof(CreateBookingHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateScreenplayHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateRatingHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateAccountHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(LoginAccountHandler).GetTypeInfo().Assembly);

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IScreenplayRepository, ScreenplayRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();

            // Services
            services.AddScoped<ITokenService, TokenService>();

            // Database
            services.AddDbContext<DataContext>(
                m => m.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

            services.AddAutoMapper(typeof(Startup));

            return services;
        }
    }
}
