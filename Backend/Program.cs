using Backend;

CreateHostBuilder(args).Build().Run();
return;

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webHostBuilder =>
            webHostBuilder.UseStartup<Startup>());
}