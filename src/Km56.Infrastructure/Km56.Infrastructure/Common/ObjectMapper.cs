using System.Reflection;

namespace Km56.Infrastructure.Common
{
    public class ObjectMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            if (source == null) throw new ArgumentNullException("source");

            TTarget? target = Activator.CreateInstance(typeof(TTarget)) as TTarget;

            if (target == null) throw new Exception("Unable to create target type");

            foreach (PropertyInfo sourceProperty in source.GetType().GetProperties())
            {
                PropertyInfo? targetProperty = target.GetType().GetProperties().Where(p => p.Name == sourceProperty.Name).FirstOrDefault();
                if (targetProperty != null && targetProperty.GetType().Name == sourceProperty.GetType().Name)
                {
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
                }
            }

            return target;
        }
    }
}
