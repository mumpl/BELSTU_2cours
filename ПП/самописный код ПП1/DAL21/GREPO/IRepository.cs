// IRepository.cs
using System;
using System.Collections.Generic;

namespace GREPO
{
    /// <summary>
    /// Generic interface for repository operations with two type parameters.
    /// </summary>
    /// <typeparam name="T1">Type for WSRef entities</typeparam>
    /// <typeparam name="T2">Type for Comment entities</typeparam>
    public interface IRepository<T1, T2> : IDisposable
    {
        /// <summary>
        /// Retrieves all WSRef entities.
        /// </summary>
        /// <returns>List of T1 objects</returns>
        List<T1> getAllWSRef();

        /// <summary>
        /// Retrieves all Comment entities.
        /// </summary>
        /// <returns>List of T2 objects</returns>
        List<T2> getAllComment();

        /// <summary>
        /// Retrieves a Comment entity by its ID.
        /// </summary>
        /// <param name="Id">The ID of the comment</param>
        /// <returns>T2 object if found, null otherwise</returns>
        T2? GetCommentById(int Id);

        /// <summary>
        /// Adds a new WSRef entity.
        /// </summary>
        /// <param name="wsRef">The WSRef entity to add</param>
        /// <returns>True if successful, false otherwise</returns>
        bool addWSRef(T1 wsRef);

        /// <summary>
        /// Adds a new Comment entity.
        /// </summary>
        /// <param name="comment">The Comment entity to add</param>
        /// <returns>True if successful, false otherwise</returns>
        bool addComment(T2 comment);
    }
}