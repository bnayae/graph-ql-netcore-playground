using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bnaya.Samples.Repositories;
using Bnaya.Samples.GraphQLs.Definitions;
using Bnaya.Samples.GraphQLs.DTOs;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bnaya.Samples.GraphQLs;
using GraphiQl;

// credit: https://graphql-dotnet.github.io/docs/getting-started/mutations/

namespace Bnaya.Samples
{
    public class Startup
    {
        public const string GraphQlPath = "/graphql";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // ----------- Data Loader support  -------------------
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<IDocumentExecutionListener, DataLoaderDocumentListener>();
            // ----------- Data Loader support  -------------------


            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<Queries>();

            RegisterTypes(services);

            services.AddSingleton<ISchema>(
                            s => new MainSchema(
                                            new FuncDependencyResolver(
                                                        type => (IGraphType)s.GetRequiredService(type))));
        }

        private void RegisterTypes(IServiceCollection services)
        {
            services.AddSingleton<QuestionType>();
            services.AddSingleton<UserType>();
            services.AddSingleton<ReviewType>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseGraphiQl(GraphQlPath);
            /*app.UseGraphiQl(GraphQlPath, "/v1/something");*/
            app.UseMvc();
        }
    }
}
