using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Mediatek.Helpers
{
    public static class CopyHelper
    {
        public static void Copy<T>(T source, T target) where T : class
        {
            Copy(source, target, false);
        }

        public static void Copy<T>(T source, T target, bool cloneCollections) where T : class
        {
            CopyHelper<T>.Copy(source, target);
        }
    }

    public static class CopyHelper<T> where T : class
    {
        private static readonly Action<T, T> _copyDelegate;

        static CopyHelper()
        {
            var dm = new DynamicMethod(string.Empty, typeof(void), new[] { typeof(T), typeof(T) }, true);
            var il = dm.GetILGenerator();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanWrite || !prop.CanRead)
                    continue;

                if (prop.GetIndexParameters().Any())
                    continue;

                if (prop.PropertyType.GetInterface("IList") != null || prop.PropertyType.GetInterface("ICollection`1") != null)
                {
                    var ctor = prop.PropertyType.GetConstructor(new Type[0]);
                    MethodInfo copyMethod = null;
                    if (ctor != null)
                    {
                        Type elementType = GetElementType(prop.PropertyType);
                        if (elementType != null)
                            copyMethod = typeof(CopyHelper<T>).GetMethod("CopyGenericCollection").MakeGenericMethod(elementType);
                        else
                            copyMethod = typeof(CopyHelper<T>).GetMethod("CopyList");
                    }
                    else
                    {
                        Type genericCollection = prop.PropertyType.GetInterface("ICollection`1");
                        if (genericCollection != null)
                        {
                            Type elementType = GetElementType(genericCollection);
                            Type collectionType = typeof(ObservableCollection<>).MakeGenericType(elementType);
                            if (prop.PropertyType.IsAssignableFrom(collectionType))
                            {
                                ctor = collectionType.GetConstructor(new Type[0]);
                                copyMethod = typeof(CopyHelper<T>).GetMethod("CopyGenericCollection").MakeGenericMethod(elementType);
                            }
                        }
                        else
                        {
                            Type collection = prop.PropertyType.GetInterface("IList");
                            if (collection != null)
                            {
                                Type collectionType = typeof(ArrayList);
                                if (prop.PropertyType.IsAssignableFrom(collectionType))
                                {
                                    ctor = collectionType.GetConstructor(new Type[0]);
                                    copyMethod = typeof(CopyHelper<T>).GetMethod("CopyList");
                                }
                            }
                        }

                    }

                    if (ctor != null)
                    {
                        var local = il.DeclareLocal(prop.PropertyType);
                        il.Emit(OpCodes.Newobj, ctor);
                        il.Emit(OpCodes.Stloc, local.LocalIndex);
                        il.Emit(OpCodes.Ldloc, local.LocalIndex);
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Callvirt, prop.GetGetMethod());
                        il.Emit(OpCodes.Call, copyMethod);
                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Callvirt, prop.GetSetMethod());
                        continue;
                    }
                }

                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, prop.GetGetMethod());
                il.Emit(OpCodes.Callvirt, prop.GetSetMethod());
            }
            il.Emit(OpCodes.Ret);
            _copyDelegate = (Action<T, T>)dm.CreateDelegate(typeof(Action<T, T>));
        }

        static Type GetElementType(Type collectionType)
        {
            return collectionType.GetGenericArguments().FirstOrDefault();
        }

        static void CopyGenericCollection<TItem>(ICollection<TItem> original, ICollection<TItem> copy)
        {
            if (original == null)
                return;

            foreach (var item in original)
            {
                copy.Add(item);
            }
        }

        static void CopyList(IList original, IList copy)
        {
            if (original == null)
                return;

            foreach (var item in original)
            {
                copy.Add(item);
            }
        }

        public static void Copy(T source, T target)
        {
            _copyDelegate(source, target);
        }
    }
}
