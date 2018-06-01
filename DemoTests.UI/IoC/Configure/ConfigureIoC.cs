namespace DemoTests.UI.IoC.Configure
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class ConfigureIoC
    {
        public static void Configure(IWindsorContainer container)
        {
            container.Install(FromAssembly.This());
        }
    }
}
