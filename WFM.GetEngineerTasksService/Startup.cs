using D3K.Diagnostics.Unity;
using D3K.Diagnostics.NLogExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unity;
using Unity.Injection;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;

using WFM.GetEngineerTasksServiceLib.EngineerTasks;
using WFM.GetEngineerTasksServiceLib.Mapper;
using WFM.GetEngineerTasksServiceLib.SoRepositories;
using WFM.GetEngineerTasksServiceLib.SsRepositories;
using WFM.InventoryLib.Options;
using WFM.So.Provider;
using WFM.So.Repositories;
using WFM.Unity;

using SoServiceReference;
using SsServiceReference;

namespace WFM.GetEngineerTasksService
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
            services.AddControllers();

            //services.AddOptions<ServiceOptimizationServiceClientOptions>();
            services.Configure<ServiceOptimizationServiceClientOptions>(Configuration.GetSection("ServiceOptimizationServiceClient"));
            services.Configure<ScheduleServiceClientOptions>(Configuration.GetSection("ScheduleServiceClient"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(IUnityContainer container)
        {
            container
                .AddNewExtension<Interception>()
                .RegisterMethodLogInterceptionBehavior<NLogLogListenerFactory>("syncLog", "Debug")
                .RegisterMethodIdentityInterceptionBehavior<NLogLogContext>("syncPid", "pid")
                .RegisterHashCodeMethodLogInterceptionBehavior<NLogLogListenerFactory>("hashCodeLog", "Debug")
                .RegisterMethodLogAsyncInterceptionBehavior<NLogLogListenerFactory>("asyncLog", "Debug")
                .RegisterMethodIdentityAsyncInterceptionBehavior<NLogLogContext>("asyncPid", "pid")
            ;

            RegisterDependencies(container);
        }

        public void RegisterDependencies(IUnityContainer container)
        {
            var ii = new Interceptor<InterfaceInterceptor>();
            var ll = new InterceptionBehavior<MethodLogInterceptionBehavior>("syncLog");
            var hl = new InterceptionBehavior<MethodLogInterceptionBehavior>("hashCodeLog");
            var tb = new InterceptionBehavior<MethodIdentityInterceptionBehavior>("syncPid");
            var eb = new InterceptionBehavior<ErrorHandlingBehavior>();

            var soEngineerRepositoryFactoryExtension = new SoEngineerRepositoryFactoryExtension(ll);

            container
                .AddExtension(soEngineerRepositoryFactoryExtension)

                .RegisterType<IEngineerTasksRepository, ExceptionHandlerEngineerTasksRepository>(new InjectionProperty("Target", new ResolvedParameter<IEngineerTasksRepository>("corrDateEngineerTasksRepository")), ii, tb, ll)
                .RegisterType<IEngineerTasksRepository, CorrDateEngineerTasksRepository>("corrDateEngineerTasksRepository", new InjectionProperty("Target", new ResolvedParameter<IEngineerTasksRepository>("engineerTasksRepository")), ii, tb, ll)
                .RegisterType<IEngineerTasksRepository, EngineerTasksRepository>("engineerTasksRepository", ii, tb, ll)
                .RegisterType<ISoEngineerRepository, ValidatingSoEngineerRepository>(new InjectionProperty("Target", new ResolvedParameter<ISoEngineerRepository>("soEngineerRepository")), ii, tb, ll)
                .RegisterType<ISoEngineerRepository, SoEngineerRepository>("soEngineerRepository", ii, tb, ll)
                .RegisterType<ISoAssignmentRepository, SoAssignmentRepository>(ii, tb, ll)
                .RegisterType<ISsAssignmentRepository, SsAssignmentRepository>(ii, tb, ll)
                .RegisterType<ISoAssignmentMapperFactory, SoAssignmentMapperFactory>()
                .RegisterType<ISoTaskRepository, CorrUnavailabilitiesSoTaskRepository>(new InjectionProperty("Target", new ResolvedParameter<ISoTaskRepository>("soTaskRepository")), ii, tb, ll)
                .RegisterType<ISoTaskRepository, SoTaskRepository>("soTaskRepository", ii, tb, ll)

                .RegisterType<IRepository<SoServiceReference.TaskStatus>, Repository<SoServiceReference.TaskStatus>>(ii, tb, ll)
                .RegisterType<IProvider<SoServiceReference.TaskStatus>, Provider<SoServiceReference.TaskStatus>>(ii, tb, ll)
                .RegisterType<IProvider<SoServiceReference.Task>, Provider<SoServiceReference.Task>>(ii, tb, ll)

                .RegisterType<ServiceOptimizationService, ServiceOptimizationServiceClient>(ii, tb, ll)
                .RegisterType<IServiceOptimizationServiceClientSettings, JsonServiceOptimizationServiceClientSettings>(ii, ll)

                .RegisterType<ScheduleService, ScheduleServiceClient>(ii, tb, ll)
                .RegisterType<IScheduleServiceClientSettings, JsonScheduleServiceClientSettings>(ii, ll)
            ;
        }
    }
}
