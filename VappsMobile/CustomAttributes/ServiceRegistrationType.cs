namespace VappsMobile.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceRegistrationType : Attribute
    {
        public ServiceLifetime Lifetime { get; private set; }

        public ServiceRegistrationType(ServiceLifetime serviceLifetime) => Lifetime = serviceLifetime;
    }
}
