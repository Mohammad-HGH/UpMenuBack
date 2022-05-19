using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpMenu.DataLayer.Context;

namespace UpMenu.Core.Utilities.Extensions.Connection
{
    public static class ConnectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<UpMenuDBContext>(options =>
            {
                var connectionString = "ConnectionStrings:UpMenuConnection:Development";
                options.UseSqlServer(configuration[connectionString]);
            });
            return service;
        }
    }
}
