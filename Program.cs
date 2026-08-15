using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using MsmePortal.Services;
using System;
using System.IO;

namespace MsmePortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add Distributed Memory Cache (REQUIRED for HttpContext.Session!)
            builder.Services.AddDistributedMemoryCache();

            // 2. Configure DataProtection keys folder to writable /tmp directory for Docker non-root users
            try
            {
                var keysFolder = Path.Combine(Path.GetTempPath(), "aspnet-keys");
                Directory.CreateDirectory(keysFolder);
                builder.Services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                    .SetApplicationName("MsmePortal");
            }
            catch
            {
                builder.Services.AddDataProtection()
                    .SetApplicationName("MsmePortal");
            }

            // 3. Add MVC controllers with views
            builder.Services.AddControllersWithViews();

            // 4. Register Data Store singleton service
            builder.Services.AddSingleton<PortalDataStore>();

            // 5. Configure Session Management
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".MsmePortal.Session";
            });

            // 6. Configure Forwarded Headers for Reverse Proxies (Render, Nginx, Cloudflare)
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            var app = builder.Build();

            // 7. Enable Developer Exception Page for clear diagnostics
            app.UseDeveloperExceptionPage();

            app.UseForwardedHeaders();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
