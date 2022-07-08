using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phonebook.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using System.Net.Http;
using PhoneBook.API.Services;
using PhoneBook.Domain.Service;
using PhoneBook.Application.Service;
using PhoneBook.Domain.Mapping;
using PhoneBook.Domain.Repositories;
using Phonebook.Data.Repositories;

namespace PhoneBook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhonebookContext>(e =>
            {
               e.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PhoneBook.API",
                    Description = "PhoBook.API Gets and Add phonebook entries ",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });
            MapRepositories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseRouting();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
          
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint(Configuration["AppSettings:VirtualDirectory"] + "/swagger/v1/swagger.json", "Phone.Book.Entry");
                });

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );
        }

        private void MapRepositories(IServiceCollection services)
        {
            services.AddScoped<IPhonebookRepository, PhonebookRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IPhonebookService, PhonebookService>();
        }
    }
}
