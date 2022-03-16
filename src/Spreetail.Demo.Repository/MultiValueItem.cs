using System.Collections.Generic;

namespace Spreetail.Demo.Repository
{
    /// <summary>
    /// Represents a multi-value entry.
    /// </summary>
    /// <typeparam name="TKey">The <see cref="System.Type"/> associated with the key.</typeparam>
    /// <typeparam name="TMember">The <see cref="System.Type"/> associated with the member.</typeparam>
    /// <param name="Key">The multi-value item key.</param>
    /// <param name="Members">A collection of zero or more multi-value item members.</param>
    public record MultiValueItem<TKey, TMember>(TKey Key, IEnumerable<TMember> Members);
}
