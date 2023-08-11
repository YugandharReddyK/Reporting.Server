using Mapster;
using System;
using System.Linq;
using System.Reflection;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class TypeConverter
    {
        private static readonly object CreateMappingLocker = new object();

        public static T DynamicMap<T>(object src)
        {
            if (src == null)
                return default(T);
            try
            {
                // TODO: Suhail - Need to remove when Mapster is finalized  
                //Type type = src.GetType();
                //Mapper mapper = InitializeAutomapper(src.GetType(), typeof(T));
                //return mapper.Map<T>(src);
                return src.Adapt<T>();
            }
            catch (Exception ex)
            {
                try
                {
                    return ShallowCopy<T>(src);
                }
                catch
                {
                    throw ex;
                }
            }
        }

        private static T ShallowCopy<T>(object source)
        {
            if (source == null)
                return default(T);
            Type typeSource = source.GetType();
            var target = Activator.CreateInstance<T>();

            var sourcePropertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in propertyInfo)
            {
                if (!property.CanWrite)
                    continue;
                var foundSource = sourcePropertyInfo.FirstOrDefault(item => item.Name == property.Name);
                if (foundSource == null)
                    continue;
                property.SetValue(target, foundSource.GetValue(source));
            }
            return target;
        }

        // TODO: Suhail - Need to remove when Mapster is finalized  
        //private static void InitializeAutomapper(Type t1, Type t2)
        //{
        //    if (_mapper == null)
        //    {
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap(t1, t2);
        //        });
        //        _mapper = new Mapper(config);
        //    }
        //}
    }
}
