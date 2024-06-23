using FluentValidation;
using Backend.Cors;
using Backend.Extensions;
using Backend.Session;
using Backend.Validators;

namespace Backend;

public class Startup
{
    private readonly IConfiguration _configuration;
    private IServiceProvider _serviceProvider;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        try
        {
            services.AddOptions<CorsSettings>().Bind(_configuration.GetSection(nameof(CorsSettings)));
            services.AddSingleton(typeof(ISessionProvider), typeof(SessionManager));
            services.AddSingleton(typeof(IValidator<Models.Session>), typeof(SessionValidator));
            services.AddSingleton(typeof(CorsValidator));
            services.AddValidators();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            // own validator implementation
                            .SetIsOriginAllowed(origin =>
                            {
                                var prov = _serviceProvider
                                    .GetService<CorsValidator>();
                                return prov
                                    .IsOriginAllowed(origin);
                            })
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
            });

            services.AddAuthorization();
            services.AddMvc();
            services.AddControllers(options =>
                    {
                        options.Filters.Add<HttpResponseExceptionFilter>();//for correct exceptions
                    })
                    .AddJsonOptions(opts =>
                    {
                        opts.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());//for correct serialize\deserialize enum values as a strings
                        opts.JsonSerializerOptions.PropertyNamingPolicy = null;//by default response JSON data will be formatted into camel case; we don't want that
                    });
        }
        catch (Exception ex)
        {
            Console.Write($"Failed to start. Details: {ex}");
            Environment.Exit(-1);
        }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostApplicationLifetime applicationLifetime, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        app.UseRouting();
        app.UseCors();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        _serviceProvider = app.ApplicationServices;
    }
}