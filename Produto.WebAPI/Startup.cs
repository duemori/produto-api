using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Produto.Application.Command.Handlers;
using Produto.Application.Command.Mapping;
using Produto.Application.Command.Validators;
using Produto.Application.Query.DAOs;
using Produto.Application.Query.Handlers;
using Produto.Application.Query.Validators;
using Produto.Domain.Context;
using Produto.Domain.Repositories;
using Produto.Infrastructure.Command.Repositories;
using Produto.Infrastructure.Query.DAOs;
using Produto.WebAPI.Validation;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace Produto.WebAPI
{
    public class Startup
    {
        private readonly string _allowedOrigins = "_allowedOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_allowedOrigins, builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new CustomExceptionFilter());
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Produto API",
                    Description = "API com CRUD de Produtos"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            var connectionString = Configuration.GetConnectionString("SqlServerConn");

            services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));

            services.AddScoped<IProdutoDao, ProdutoDao>();

            services.AddDbContext<ProdutoContext>(f => f.UseSqlServer(connectionString));

            services.AddScoped<IProdutoContext>(f => f.GetService<ProdutoContext>());

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddMediatR(typeof(CriarProdutoCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ProdutoQueryHandler).GetTypeInfo().Assembly);

            AssemblyScanner
                .FindValidatorsInAssembly(typeof(CriarProdutoCommandValidator).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            AssemblyScanner
                .FindValidatorsInAssembly(typeof(ProdutoQueryValidator).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddSingleton(new MapperConfiguration(c => c.AddProfile<ProdutoProfile>()).CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_allowedOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Produto API");
            });
        }
    }
}
