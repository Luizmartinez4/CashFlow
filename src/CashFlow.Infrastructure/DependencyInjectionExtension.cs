using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
        }

        // Método para registrar os repositórios
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
        }

        // Método para registrar o DbContext com a string de conexão
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Obtém a string de conexão do arquivo de configuração
            var connectionString = configuration.GetConnectionString("Connection");

            // Configura a versão do MySQL (aqui você define qual versão está usando)
            var version = new Version(8, 0, 41);
            var serverVersion = new MySqlServerVersion(version);

            // Registra o DbContext com a string de conexão e a versão do MySQL
            services.AddDbContext<CashFlowDbContext>(config =>
                config.UseMySql(connectionString, serverVersion));
        }
    }
}
