using System;
using System.Collections.Generic;

namespace Mediatek.Entities
{
    public class Role : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Predefined { get; set; }
        public Guid? SymbolId { get; set; }

        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual Image Symbol { get; set; }

        public static readonly Guid DirectorRoleId;
        public static readonly Guid ProducerRoleId;
        public static readonly Guid ActorRoleId;
        public static readonly Guid AuthorRoleId;
        public static readonly Guid PerformerRoleId;
        public static readonly Guid ComposerRoleId;
        public static readonly Guid GuestRoleId;

        static Role()
        {
            // Ids for predefined roles
            DirectorRoleId = Guid.Parse("a3cb6aad-3149-4606-902a-f04317249a1d");
            ProducerRoleId = Guid.Parse("b6076138-d39a-4792-acf3-33f52510134e");
            ActorRoleId = Guid.Parse("54ad8bca-2e3b-4c80-a892-8a1962a96237");
            AuthorRoleId = Guid.Parse("f14d1d45-4d32-4ce1-ab9d-33439f392a96");
            PerformerRoleId = Guid.Parse("f6e2c152-7ea7-4521-be81-51a02d44dfa3");
            ComposerRoleId = Guid.Parse("70ca9ec7-f73c-407a-83c1-00fe39aa663a");
            GuestRoleId = Guid.Parse("8d96e05c-2190-40a7-8e16-8139dadd3be1");
        }
    }
}
