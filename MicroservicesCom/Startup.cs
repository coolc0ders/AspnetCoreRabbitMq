using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompaniesService;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UsersService.Consumers;
using UsersService.Helpers;

namespace MicroservicesCom
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
            services.Configure<RabbitmqConfig>(Configuration.GetSection("Rabbitmq"));
            services.AddSingleton<FakeStore>();

            services.AddMassTransit(x =>
            {
                var configSections = Configuration.GetSection("Rabbitmq");
                var host = configSections["Host"];
                var userName = configSections["UserName"];
                var password = configSections["Password"];
                var virtualHost = configSections["VirtualHost"];
                var port = Convert.ToUInt16(configSections["Port"]);

                x.AddConsumer<SubscriptionSuccessfulConsumer>();

                x.AddBus(provider =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        // configure health checks for this bus instance
                        cfg.UseHealthCheck(provider);

                        cfg.Host(host, port, virtualHost, host =>
                        {
                            host.Username(userName);
                            host.Password(password);
                        });

                        cfg.ReceiveEndpoint(configSections["Endpoint"], ep =>
                        {
                            ep.PrefetchCount = Convert.ToUInt16(configSections["PrefetchCount"]);
                            //ep.UseMessageRetry(r => r.Interval(2, 100));

                            ep.ConfigureConsumer<SubscriptionSuccessfulConsumer>(provider);
                        });
                    });

                    bus.Start();

                    return bus;
                });
            });

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });

            services.AddMassTransitHostedService();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
