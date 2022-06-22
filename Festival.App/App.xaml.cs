using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using Festival.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Festival.App.Factories;
using Festival.App.Services;
using Festival.App.ViewModels;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.DAL.Entities;
using Festival.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Nemesis.Essentials.Runtime;

namespace Festival.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs");

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<BandFacade>();
            services.AddSingleton<EventFacade>();
            services.AddSingleton<StageFacade>();

            services.AddSingleton<EventRepository>();
            services.AddSingleton<RepositoryBase<StageEntity>>();
            services.AddSingleton<RepositoryBase<BandEntity>>();

            
            services.AddSingleton<IMediator, Mediator>();

            services.AddTransient<IBandDetailViewModel, BandDetailViewModel>();
            services.AddTransient<IStageDetailViewModel, StageDetailViewModel>();
            services.AddTransient<IEventDetailViewModel, EventDetailViewModel>();
            services.AddSingleton<IStageListViewModel, StageListViewModel>();
            services.AddSingleton<IEventListViewModel, EventListViewModel>();
            services.AddSingleton<IBandListViewModel, BandListViewModel>();

            services.AddSingleton<IDbContextFactory<FestivalDbContext>>(provider => new SqlServerDbContextFactory());
            services.AddAutoMapper(cfg=> cfg.AddProfile(new BL.Mappers.MapperProfile()));

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            var dbContextFactory = _host.Services.GetRequiredService<IDbContextFactory<FestivalDbContext>>();
#if DEBUG
            await using (var dbx = dbContextFactory.CreateDbContext())
            {
                await dbx.Database.MigrateAsync();
            }
#endif
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
