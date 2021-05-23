using System;

namespace SystemyWP.API.CustomAttributes
{
    public class TransientService : Attribute { }
    public class ScopedService : Attribute { }
    public class SingletonService : Attribute { }
}