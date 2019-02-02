using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bnaya.Samples.GraphQLs;
using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Services;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
            services.AddSingleton<TodoType>();
            services.AddSingleton<UserType>();
            services.AddSingleton<AddressType>();
            services.AddSingleton<CompanyType>();
            services.AddSingleton<GeoType>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseGraphiQl(GraphQlPath);
            /*app.UseGraphiQl(GraphQlPath, "/v1/something");*/
            app.UseMvc();
        }
    }
}
