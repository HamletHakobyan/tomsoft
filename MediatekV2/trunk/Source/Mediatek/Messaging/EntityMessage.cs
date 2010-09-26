namespace Mediatek.Messaging
{
    enum  EntityAction
    {
        Created,
        Deleted,
        Modified
    }

    class EntityMessage<TEntity>
    {
        public EntityMessage(TEntity entity, EntityAction action)
        {
            Entity = entity;
            Action = action;
        }

        public TEntity Entity { get; private set; }
        public EntityAction Action { get; private set; }
    }
}
