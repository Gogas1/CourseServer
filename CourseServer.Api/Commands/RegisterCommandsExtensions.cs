using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands
{
    public static class RegisterCommandsExtensions
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddKeyedTransient<Command, TestCommand>("test");
        }
    }
}
