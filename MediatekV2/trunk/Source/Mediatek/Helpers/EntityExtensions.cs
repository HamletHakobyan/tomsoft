using System;
using Mediatek.Entities;
using System.Linq.Expressions;

namespace Mediatek.Helpers
{
    public static class EntityExtensions
    {
        public static TEntity LoadProperty<TEntity>(this TEntity entity, Expression<Func<TEntity, object>> selector)
            where TEntity : IEntity
        {
            var s = selector.Compile();
            if (s(entity) == null)
                App.Repository.LoadProperty(entity, selector);
            return entity;
        }
    }
}
