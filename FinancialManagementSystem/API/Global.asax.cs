using Application.Handlers;
using Autofac;
using Autofac.Integration.WebApi;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helper;
using Infrastructure.Repositories;
using Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace API
{
       public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            // Register API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register DbContext
            builder.RegisterType<FinancialDbContext>()
                   .AsSelf()
                   .InstancePerRequest(); 

            // Register Handlers
            builder.RegisterType<JwtTokenService>().AsSelf();
            builder.RegisterType<JwtAuthorizeAttribute>().AsSelf();

            builder.RegisterType<CreateChartOfAccountHandler>().AsSelf();
            builder.RegisterType<GetActiveChartOfAccountsHandler>().AsSelf();
            builder.RegisterType<UpdateChartOfAccountHandler>().AsSelf();
            builder.RegisterType<GetChartOfAccountByIdHandler>().AsSelf();
            builder.RegisterType<DeleteChartOfAccountHandler>().AsSelf();
            builder.RegisterType<GetActiveChartOfAccountsReportHandler>().AsSelf();


            builder.RegisterType<CreateJournalEntryHandler>().AsSelf();
            builder.RegisterType<GetActiveJournalEntriesHandler>().AsSelf();
            builder.RegisterType<UpdateJournalEntryHandler>().AsSelf();
            builder.RegisterType<GetJournalEntryByIdHandler>().AsSelf();
            builder.RegisterType<DeleteJournalEntryHandler>().AsSelf();
            builder.RegisterType<GetJournalEntriesByDateHandler>().AsSelf();


            builder.RegisterType<DataAccessHelper>().AsSelf().InstancePerLifetimeScope();

            // Register Repositories
            builder.RegisterType<ChartOfAccountRepository>().As<IChartOfAccountRepository>();
            builder.RegisterType<JournalEntryRepository>().As<IJournalEntryRepository>();

            builder.RegisterType<FinancialDbContext>().AsSelf();

            // Build the container
            var container = builder.Build();
           
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {         
            if (Request.Url.AbsolutePath == "/")
            {
                // Redirect to the Swagger UI URL
                Response.Redirect("~/swagger/ui/index", true);
            }
        }
    }
}

