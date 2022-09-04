using System.Linq.Expressions;
using System.Reflection;

namespace VappsMobile.Expressions
{
    internal static class PropExtensions
    {
        public static T GetPropertyValue<T>(this Expression<Func<T>> lamba)
        {
            return lamba.Compile().Invoke();
        }

        public static void SetPropertyValue<T>(this Expression<Func<T>> lamba, T value)
        {
            var expression = lamba.Body as MemberExpression;

            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            propertyInfo.SetValue(target, value);
        }
    }
}
