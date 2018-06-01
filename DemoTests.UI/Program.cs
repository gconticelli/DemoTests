namespace DemoTests.UI
{
    using Castle.Windsor;
    using DemoTests.BLL;
    using DemoTests.UI.IoC.Configure;
    using System.Configuration;

    static class Program
    {
        private static IWindsorContainer container;
        private static ICourseService courseService;

        static void Main(string[] args)
        {
            InitIoC();
            InitDep();

            var conn = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;


            //var res = courseService.GetStudents();
        }

        private static void InitIoC()
        {
            container = new WindsorContainer();
            ConfigureIoC.Configure(container);
        }

        private static void InitDep()
        {
            courseService = container.Resolve<ICourseService>();
        }
    }
}
