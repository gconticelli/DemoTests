namespace DemoTests.UI.IoC.Installer
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using DAL;
    using DAL.Implementations;
    using DemoTests.BLL;
    using DemoTests.BLL.Implementations;
    using System.Configuration;

    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Register(container);
        }

        private void Register(IWindsorContainer container)
        {
            container.Register(Component.For<ICourseDataService>().ImplementedBy<CourseDataService>()
                .DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString)));

            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());

            container.Register(Component.For<ICourseService>().ImplementedBy<CourseService>());
        }
    }
}
