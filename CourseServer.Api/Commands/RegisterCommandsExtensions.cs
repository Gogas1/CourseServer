using CourseServer.Api.Commands.CommandsList;
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
            services.AddKeyedTransient<Command, AddIncomeCommand>("create_income");
            services.AddKeyedTransient<Command, AddIncomeProductSearchCommand>("product_searchfor_addincome");
            services.AddKeyedTransient<Command, IncomesSearchCommand>("incomes_search");
            services.AddKeyedTransient<Command, GetIncomeProductsCommand>("get_income_products");
            services.AddKeyedTransient<Command, SearchProductsCommand>("search_products");
            services.AddKeyedTransient<Command, GetOutgoingProductById>("get_outgoingproduct_id");
            services.AddKeyedTransient<Command, SearchOutgoingProductsCommand>("search_products_outgoing");
            services.AddKeyedTransient<Command, UpdateProductFeaturesCommand>("update_product_features");
            services.AddKeyedTransient<Command, SubmitOutgoingCommand>("submit_outgoing");
            services.AddKeyedTransient<Command, OutgoingsSearchCommand>("outgoings_search");

            services.AddSingleton<CommandController>();
        }
    }
}
