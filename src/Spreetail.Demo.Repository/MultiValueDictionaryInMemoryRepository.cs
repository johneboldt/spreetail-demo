using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spreetail.Demo.Repository
{
    public class MultiValueDictionaryInMemoryRepository<TKey, TMember> : IMultiValueDictionaryRepository<TKey, TMember> where TMember : IEquatable<TMember>
    {
        private readonly Dictionary<TKey, List<TMember>> _dictionary;
        private readonly SemaphoreSlim _semaphoreSlim;
        private static readonly Response Ok = new();

        public MultiValueDictionaryInMemoryRepository()
        {
            _dictionary = new Dictionary<TKey, List<TMember>>();
            _semaphoreSlim = new SemaphoreSlim(1);
        }

        public async Task<Response> AddAsync(TKey key, TMember member)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (!_dictionary.TryGetValue(key, out var value))
                {
                    value = new List<TMember>();
                }

                if (value.Any(v => v.Equals(member))) return new Response(ErrorConstants.DuplicateMember);
                value.Add(member);
                _dictionary[key] = value;
                return await Task.FromResult(Ok);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<Response> ClearAsync()
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                _dictionary.Clear();
                return await Task.FromResult(Ok);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<IEnumerable<MultiValueItem<TKey, TMember>>> GetAllItemsAsync()
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                var keys = _dictionary.Keys;
                return keys.Select(k => new MultiValueItem<TKey, TMember>(k, _dictionary[k]));
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<IEnumerable<TMember>> GetAllMembersAsync()
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                return _dictionary.Values.SelectMany(v => v);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<IEnumerable<TKey>> GetKeysAsync()
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                return _dictionary.Keys.Select(k => k);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<MembersResponse<TMember>> GetMembersAsync(TKey key)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (!_dictionary.TryGetValue(key, out var members)) return new MembersResponse<TMember>(ErrorConstants.KeyDoesNotExist);
                return new MembersResponse<TMember>(await Task.FromResult(members));
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> KeyExistsAsync(TKey key)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                return _dictionary.ContainsKey(key);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> MemberExistsAsync(TKey key, TMember member)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (!_dictionary.TryGetValue(key, out var value)) return false;
                return await Task.FromResult(value.Any(m => m.Equals(member)));
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<Response> RemoveAllAsync(TKey key)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (!_dictionary.ContainsKey(key)) return new Response(ErrorConstants.KeyDoesNotExist);
                _dictionary.Remove(key);
                return Ok;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<Response> RemoveMemberAsync(TKey key, TMember member)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (!_dictionary.TryGetValue(key, out var members)) return new Response(ErrorConstants.KeyDoesNotExist);
                if (!members.Any(m => m.Equals(member))) return new Response(ErrorConstants.MemberDoesNotExist);
                members.Remove(member);
                if (!members.Any()) _dictionary.Remove(key);
                return Ok;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
