using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Mediatek.Helpers
{
    public class Editable<T> : DynamicObject, IEditableObject
        where T : class, new()
    {
        private readonly T _original;
        private readonly T _current;
        private bool _isEditing;

        public Editable(T original)
        {
            _original = original;
            _current = new T();
        }

        protected virtual void TransferValues(T source, T target)
        {
            CopyHelper.Copy(source, target);
        }

        #region IEditableObject Membres

        public void BeginEdit()
        {
            if (_isEditing)
                throw new InvalidOperationException("The object is already being edited.");
            TransferValues(_original, _current);
            _isEditing = true;
        }

        public void CancelEdit()
        {
            if (!_isEditing)
                throw new InvalidOperationException("The object is not being edited.");
            TransferValues(_original, _current);
            _isEditing = false;
        }

        public void EndEdit()
        {
            if (!_isEditing)
                throw new InvalidOperationException("The object is not being edited.");
            TransferValues(_current, _original);
            _isEditing = false;
        }

        #endregion

        #region DynamicObject overrides

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Func<T, dynamic> getter;
            if (_propertyGetters.TryGetValue(binder.Name, out getter))
            {
                result = getter(_current);
                return true;
            }
            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Action<T, dynamic> setter;
            if (_propertySetters.TryGetValue(binder.Name, out setter))
            {
                setter(_current, value);
                return true;
            }
            return false;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (indexes.Length == 1)
            {
                Type idxType = indexes[0].GetType();
                if (_indexGetters.Contains(idxType))
                {
                    var getter = _indexGetters[idxType];
                    result = getter(_current, indexes[0]);
                    return true;
                }
            }
            result = null;
            return false;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            if (indexes.Length == 1)
            {
                Type idxType = indexes[0].GetType();
                if (_indexSetters.Contains(idxType))
                {
                    var setter = _indexSetters[idxType];
                    setter(_current, indexes[0], value);
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Getter/setter cache

        private static readonly Dictionary<string, Func<T, object>> _propertyGetters;
        private static readonly Dictionary<string, Action<T, object>> _propertySetters;
        private static readonly KeyedByTypeCollection<Func<T, object, object>> _indexGetters;
        private static readonly KeyedByTypeCollection<Action<T, object, object>> _indexSetters;

        static Editable()
        {
            var properties = typeof(T).GetProperties().Where(p => !p.GetIndexParameters().Any()).ToArray();

            _propertyGetters = new Dictionary<string, Func<T, object>>();
            foreach (var prop in properties.Where(p => p.CanRead))
            {
                _propertyGetters.Add(prop.Name, MakePropertyGetter(prop));
            }

            _propertySetters = new Dictionary<string, Action<T, object>>();
            foreach (var prop in properties.Where(p => p.CanWrite))
            {
                _propertySetters.Add(prop.Name, MakePropertySetter(prop));
            }

            var indexes = typeof(T).GetProperties().Where(p => p.GetIndexParameters().Length == 1);

            _indexGetters = new KeyedByTypeCollection<Func<T, object, object>>();
            foreach (var idx in indexes.Where(p => p.CanRead))
            {
                _indexGetters.Add(MakeIndexGetter(idx));
            }

            _indexSetters = new KeyedByTypeCollection<Action<T, object, object>>();
            foreach (var idx in indexes.Where(p => p.CanWrite))
            {
                _indexSetters.Add(MakeIndexSetter(idx));
            }
        }

        private static Action<T, object, object> MakeIndexSetter(PropertyInfo idx)
        {
            var @this = Expression.Parameter(typeof(T));
            var index = Expression.Parameter(typeof(object), "index");
            var value = Expression.Parameter(typeof(object), "value");
            var expr =
                Expression.Lambda<Action<T, object, object>>(
                    Expression.Assign(
                        Expression.Property(@this, idx, index),
                        Expression.Convert(value, idx.PropertyType)),
                    @this, index, value);
            return expr.Compile();
        }

        private static Func<T, object, object> MakeIndexGetter(PropertyInfo idx)
        {
            var @this = Expression.Parameter(typeof(T));
            var index = Expression.Parameter(typeof(object), "index");
            var expr =
                Expression.Lambda<Func<T, object, object>>(
                    Expression.Property(@this, idx, index),
                    @this, index);
            return expr.Compile();
        }

        private static Action<T, object> MakePropertySetter(PropertyInfo prop)
        {
            var @this = Expression.Parameter(typeof(T));
            var value = Expression.Parameter(typeof(object), "value");

            var expr =
                Expression.Lambda<Action<T, object>>(
                    Expression.Assign(
                        Expression.Property(@this, prop),
                        Expression.Convert(value, prop.PropertyType)),
                    @this, value);
            return expr.Compile();
        }

        private static Func<T, object> MakePropertyGetter(PropertyInfo prop)
        {
            var @this = Expression.Parameter(typeof(T));
            var expr =
                Expression.Lambda<Func<T, object>>(
                    Expression.Property(@this, prop),
                    @this);
            return expr.Compile();
        }

        #endregion

    }
}
