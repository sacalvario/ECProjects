﻿using ECN.Contracts.Services;
using ECN.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace ECN
{
    // For more inforation about application lifecyle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

    // WPF UI elements use language en-US by default.
    // If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
    // Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
    public partial class App : Application
    {
        private IApplicationHostService _host;
        public IServiceProvider ServiceProvider { get; set; }
        public IConfigurationRoot Configuration { get; }

        public ViewModelLocator Locator
            => Resources["Locator"] as ViewModelLocator;

        public App()
        {
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            AddConfiguration(e.Args);
            _host = SimpleIoc.Default.GetInstance<IApplicationHostService>();
            await _host.StartAsync();
        }

        private void AddConfiguration(string[] args)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Locator.AddConfiguration(configuration);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped(_ => new ecnEntities(Configuration.GetConnectionString("EcnEntities")));
        }


        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host = null;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
        }
    }
}
