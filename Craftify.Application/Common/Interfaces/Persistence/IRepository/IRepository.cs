using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Craftify.Application.Common.Interfaces.Persistence
{
    /// <summary>
    /// Generic repository interface for performing common data access operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply.</param>
        /// <param name="includedProperties">Optional comma-separated list of navigation properties to include.</param>
        /// <returns>An enumerable collection of entities.</returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includedProperties = null);

        /// <summary>
        /// Retrieves a single entity from the repository based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter expression to apply.</param>
        /// <param name="includedProperties">Optional comma-separated list of navigation properties to include.</param>
        /// <param name="tracked">Specifies whether the entity should be tracked by the context.</param>
        /// <returns>The matching entity, if found; otherwise, null.</returns>
        T Get(Expression<Func<T, bool>> filter, string? includedProperties = null, bool tracked = true);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(T entity);

        /// <summary>
        /// Removes a collection of entities from the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to remove.</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
