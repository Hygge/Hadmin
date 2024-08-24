using infrastructure.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SqlSugar;



namespace infrastructure.Db
{
    [Component(ServiceLifetime.Singleton)]
    public class DbClientFactory
    {
        private string ConnectionStringSettings;
        private readonly ILogger<DbClientFactory> _logger;
        public DbClientFactory(IConfiguration configuration, ILogger<DbClientFactory> _logger) 
        {
            this.ConnectionStringSettings = configuration.GetConnectionString("pgsql");
            this._logger = _logger;
        }

        public  ISqlSugarClient GetSqlSugarClient()
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.PostgreSQL,
           //     ConnectionString = "PORT=5432;DATABASE=Hadmin;Server=localhost;PASSWORD=123456;USER ID=hygge;",
                ConnectionString = ConnectionStringSettings,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityService = (x, p) => //处理列名
                    {
                        //要排除DTO类，不然MergeTable会有问题
                        p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName);//ToUnderLine驼峰转下划线方法
                    },
                    EntityNameService = (x, p) => //处理表名
                    {
                        //最好排除DTO类
                        p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName);//ToUnderLine驼峰转下划线方法
                    }
                }
            });
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine($"正在执行sql：{sql} \t，参数：{JsonConvert.SerializeObject(pars)}");

            };
            db.Aop.OnError = (exp) =>//SQL报错
            {
                Console.WriteLine($"{exp}");
            };  
            return db;
        }

    }
}
