using Microsoft.Build.Framework;
using Portal.BIZ.HelperModel;
using Serilog;
using Serilog.Configuration;
using Serilog.Exceptions;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogWeb.Classic.Enrichers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Portal.BIZ
{
    public class SerilogAuditTrail
    {
        // static logger object
        private static readonly Serilog.ILogger _logger;

        /// <summary>
        /// static constructor
        ///
        /// static constructor is called once that call the SerilogWriter with configurations.
        /// </summary>
        [Obsolete]
        static SerilogAuditTrail()
        {
            // get the connection string from the web.config file
            var connectionString =
                System.Configuration.ConfigurationManager.ConnectionStrings["application"].ToString();


            var columnOptions = new ColumnOptions();
            // Remove all the StandardColumn
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Add(StandardColumn.LogEvent);


            // Add all the custom columns
            columnOptions.AdditionalDataColumns = new List<DataColumn>
            {
                new DataColumn { DataType = typeof(int), ColumnName = "UserID" },
                new DataColumn { DataType = typeof(string), ColumnName = "UserName", MaxLength = 50},
                new DataColumn { DataType = typeof(string), ColumnName = "Email", MaxLength = 200},
                new DataColumn { DataType = typeof(int), ColumnName = "UserRoleID"},
                new DataColumn { DataType = typeof(string), ColumnName = "UserRole", MaxLength = 50},
                new DataColumn { DataType = typeof(string), ColumnName = "Module", MaxLength = 100},
                new DataColumn { DataType = typeof(string), ColumnName = "Action", MaxLength = 100},
                new DataColumn { DataType = typeof(string), ColumnName = "Description"},
                new DataColumn { DataType = typeof(int), ColumnName = "ModuleID"},
                new DataColumn { DataType = typeof(string), ColumnName = "TableName", MaxLength = 100},
            };

            // Serilog configurations
            // All the serilog configurations are define here except minimum logging level. default logging level get from the web.config
            _logger = new Serilog.LoggerConfiguration()
                // get the configurations from web.config
                .ReadFrom.AppSettings()
                // Writes Serilog events to System.Diagnostics.Trace. further reference : https://github.com/serilog/serilog-sinks-trace
                .WriteTo.Trace()
                // Writes log events to the Windows Console or an ANSI terminal.Themes can be specified. further reference : https://github.com/serilog/serilog-sinks-console
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                // Writes Serilog events to text files Files write as per day. further reference : https://github.com/serilog/serilog-sinks-file
                // if you want to write as JSON format refer this :  https://github.com/serilog/serilog-formatting-compact
                .WriteTo.File(HttpContext.Current.Server.MapPath("~/audit_logs/log-.txt"), rollingInterval: RollingInterval.Day, fileSizeLimitBytes: null)
                // Writes events to Microsoft SQL Server. If the table is auto created. further reference: https://github.com/serilog/serilog-sinks-mssqlserver
                .WriteTo.MSSqlServer(connectionString, "AuditTrail", columnOptions: columnOptions, autoCreateSqlTable: true)
                // Supplemented with detailed exception information and even custom exception properties. further reference : https://github.com/RehanSaeed/Serilog.Exceptions
                .Enrich.WithExceptionDetails()
                // adds a property HttpRequestId with a GUID used to identify requests. further reference : https://github.com/serilog-web/classic
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.FromLogContext()
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine("serilog_error - " + msg));
        }

        /// <summary>
        /// Information type audit trail data
        /// </summary>
        /// <param name="data"></param>
        public static void LogInfo(AuditTrailDataModel data)
        {

            try
            {
                _logger.Information("UserID : {UserID}, UserName : {UserName}, Email : {Email}" +
                                    "UserRoleID : {UserRoleID}, UserRole : {UserRole}, Module : {Module}, " +
                                    "Action : {Action}, Description : {Description}, ModuleID : {ModuleID}, TableName : {TableName}",
                    data.UserID, data.UserName, data.Email, data.UserRoleID, data.UserRole, data.Module, data.Action, data.Description, data.ModuleID, data.TableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Warning type audit trail data
        /// ex : delete data
        /// </summary>
        /// <param name="data"></param>
        public static void LogWarning(AuditTrailDataModel data)
        {

            try
            {
                _logger.Warning("UserID : {UserID}, UserName : {UserName}, Email : {Email}" +
                                "UserRoleID : {UserRoleID}, UserRole : {UserRole}, Module : {Module}, " +
                                "Action : {Action}, Description : {Description}, ModuleID : {ModuleID}, TableName : {TableName}",
                    data.UserID, data.UserName, data.Email, data.UserRoleID, data.UserRole, data.Module, data.Action, data.Description, data.ModuleID, data.TableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// All the Errors are log here
        /// </summary>
        /// <param name="data"></param>
        public static void LogError(AuditTrailDataModel data)
        {

            try
            {
                _logger.Error("UserID : {UserID}, UserName : {UserName}, Email : {Email}" +
                                "UserRoleID : {UserRoleID}, UserRole : {UserRole}, Module : {Module}, " +
                                "Action : {Action}, Description : {Description}, ModuleID : {ModuleID}, TableName : {TableName}, Exception : {Exception}",
                    data.UserID, data.UserName, data.Email, data.UserRoleID, data.UserRole, data.Module, data.Action, data.Description, data.ModuleID, data.TableName, data.Exception);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
