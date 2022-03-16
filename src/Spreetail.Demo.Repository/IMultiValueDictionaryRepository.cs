using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Spreetail.Demo.Repository
{
    /// <summary>
    /// Provides repository services for the multi-value dictionary.
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> of the multi-value dictionary key.</typeparam>
    /// <typeparam name="TMember">The <see cref="Type"/> of the multi-value dictionary value.</typeparam>
    public interface IMultiValueDictionaryRepository<TKey, TMember> where TMember : IEquatable<TMember>
    {
        /// <summary>
        /// Gets a collection of all keys currently residing in the multi-value dictionary.
        /// </summary>
        /// <returns>A collection of zero or more keys.</returns>
        Task<IEnumerable<TKey>> GetKeysAsync();

        /// <summary>
        /// Gets a collection of all members currently residing in the multi-value dictionary.
        /// </summary>
        /// <param name="key">The key value associated with the members.</param>
        /// <returns>A collection of zero or more members.</returns>
        Task<MembersResponse<TMember>> GetMembersAsync(TKey key);

        /// <summary>
        /// Adds a member to the multi-value dictionary.
        /// </summary>
        /// <param name="key">The key associated with the member.</param>
        /// <param name="member">The member to add.</param>
        /// <returns>A <see cref="Response"/> instance containing the result of the operation.</returns>
        Task<Response> AddAsync(TKey key, TMember member);

        /// <summary>
        /// Removes a member associate with a key. If the last member is removed from a key, the key is removed from the multi-value dictionary.
        /// </summary>
        /// <param name="key">The key associated with the member.</param>
        /// <param name="member">The member to remove.</param>
        /// <returns>A <see cref="Response"/> instance containing the result of the operation.</returns>
        Task<Response> RemoveMemberAsync(TKey key, TMember member);

        /// <summary>
        /// Removes all members for a key and removes the key from the multi-value dictionary.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>A <see cref="Response"/> instance containing the result of the operation.</returns>
        Task<Response> RemoveAllAsync(TKey key);

        /// <summary>
        /// Removes all keys and all members from the multi-value dictionary.
        /// </summary>
        /// <returns>A <see cref="Response"/> instance containing the result of the operation.</returns>
        Task<Response> ClearAsync();

        /// <summary>
        /// Indicates whether a key exists in the multi-value dictionary.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>A value indicating whether the key exists in the multi-value dictionary.</returns>
        Task<bool> KeyExistsAsync(TKey key);

        /// <summary>
        /// Indicates whether a member exists within a key.
        /// </summary>
        /// <param name="key">The key associated with the member.</param>
        /// <param name="member">The member to check.</param>
        /// <returns>A value indicating whether the member exists within the key.</returns>
        Task<bool> MemberExistsAsync(TKey key, TMember member);

        /// <summary>
        /// Returns all the members in the multi-value dictionary.
        /// </summary>
        /// <returns>A collection of zero or more members currently residing within the multi-value dictionary.</returns>
        Task<IEnumerable<TMember>> GetAllMembersAsync();

        /// <summary>
        /// Returns all the keys and their associated members currently residing within the multi-value dictionary.
        /// </summary>
        /// <returns>A collection of zero or more <see cref="MultiValueItem{TKey, TMember}"/> instances currently residing in the multi-value repository.</returns>
        Task<IEnumerable<MultiValueItem<TKey, TMember>>> GetAllItemsAsync();
    }
}