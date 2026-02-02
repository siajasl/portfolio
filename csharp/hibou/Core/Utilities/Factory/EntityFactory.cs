using Keane.CH.Framework.Core.Utilities.Factory;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Utilities.Factory
{
    /// <summary>
    /// Encapsulates entity factory operations.
    /// </summary>
    public sealed class EntityFactory
    {
        /// <summary>
        /// Creates an instance of an entity corresponding to the passed type.
        /// </summary>
        /// <typeparam name="T">A type inheriting from EntityBase.</typeparam>
        /// <param name="entityId">The entity id.</param>
        public static E CreateForTest<E>(int entityId)
            where E : EntityBase, new()
        {
            E result =
                (E)InstanceFactory.CreateForTest(typeof(E), true);
            result.Id = entityId;
            return result;
        }

        /// <summary>
        /// Generic factory method to create a list of test instances.
        /// </summary>
        /// <typeparam name="E">A sub-class of Entity.</typeparam>
        /// <returns>A test instance.</returns>
        public static EntityBaseCollection<E> CreateCollection<E>(int count)
            where E : EntityBase, new()
        {
            EntityBaseCollection<E> result = new EntityBaseCollection<E>(count);
            for (int i = 0; i < count; i++)
            {
                E instance = CreateForTest<E>(0);
                result.Add(instance);
            }
            return result;
        }
    }
}
