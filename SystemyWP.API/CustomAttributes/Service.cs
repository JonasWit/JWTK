using System;

namespace SystemyWP.API.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientService : Attribute { }
    [AttributeUsage(AttributeTargets.Class)]  
    public class ScopedService : Attribute { }
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonService : Attribute { }
}