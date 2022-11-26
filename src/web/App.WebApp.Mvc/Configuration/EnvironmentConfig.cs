namespace App.WebApp.Mvc.Configuration
{
    public static class EnvironmentConfig
    {
        public static void AddEnviromentConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();
        }
    }
}