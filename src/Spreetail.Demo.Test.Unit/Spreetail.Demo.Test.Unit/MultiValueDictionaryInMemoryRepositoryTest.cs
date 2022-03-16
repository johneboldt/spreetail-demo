using FluentAssertions;
using Spreetail.Demo.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spreetail.Demo.Test.Unit
{
    public class MultiValueDictionaryInMemoryRepositoryTest
    {
        private const string Foo = "foo";
        private const string Bar = "bar";
        private const string Baz = "baz";
        private const string Fizz = "Fizz";
        private const string Buzz = "Buzz";
        private const string Pop = "Pop";
        private readonly MultiValueDictionaryInMemoryRepository<string, string> _sut;

        public MultiValueDictionaryInMemoryRepositoryTest()
        {
            _sut = new MultiValueDictionaryInMemoryRepository<string, string>();
        }

        [Fact]
        public async Task AddAsyncWithNonExistentKeyShouldAddKeyAndMember()
        {
            var response = await _sut.AddAsync(Foo, Bar);
            response.HasError.Should().BeFalse();
            (await _sut.MemberExistsAsync(Foo, Bar)).Should().BeTrue();
        }

        [Fact]
        public async Task AddAsyncWithExistingKeyAndUniqueMemberShouldAddMember()
        {
            var response = await _sut.AddAsync(Foo, Bar);
            response.HasError.Should().BeFalse();
            response = await _sut.AddAsync(Foo, Baz);
            response.HasError.Should().BeFalse();
            var members = await _sut.GetMembersAsync(Foo);
            members.Members.Should().Contain(Bar);
            members.Members.Should().Contain(Baz);
        }

        [Fact]
        public async Task AddAsyncWithDuplicateMemberShouldReturnError()
        {
            var response = await _sut.AddAsync(Foo, Bar);
            response = await _sut.AddAsync(Foo, Bar);
            response.HasError.Should().BeTrue();
            response.Error.Should().Be(ErrorConstants.DuplicateMember);
        }

        [Fact]
        public async Task ClearShouldRemoveAllMultiValueDictionaryEntries()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Fizz, Buzz);
            (await _sut.GetAllItemsAsync()).Count().Should().Be(2);
            await _sut.ClearAsync();
            (await _sut.GetAllItemsAsync()).Count().Should().Be(0);
        }

        [Fact]
        public async Task GetKeysAsyncShouldReturnAllKeys()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            var keys = await _sut.GetKeysAsync();
            keys.Should().HaveCount(2);
            keys.Any(k => k.Equals(Foo)).Should().BeTrue();
            keys.Any(k => k.Equals(Fizz)).Should().BeTrue();
        }

        [Fact]
        public async Task GetMembersAsyncShouldReturnMembers()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            var response = await _sut.GetMembersAsync(Foo);
            response.Members.Count().Should().Be(2);
            response.Members.Any(m => m.Equals(Bar)).Should().BeTrue();
            response.Members.Any(m => m.Equals(Baz)).Should().BeTrue();
            response = await _sut.GetMembersAsync(Fizz);
            response.Members.Count().Should().Be(2);
            response.Members.Any(m => m.Equals(Buzz)).Should().BeTrue();
            response.Members.Any(m => m.Equals(Pop)).Should().BeTrue();
        }

        [Fact]
        public async Task GetMembersAsyncWithNonExistentKeyShouldReturnError()
        {
            var response = await _sut.GetMembersAsync(Foo);
            response.HasError.Should().BeTrue();
            response.Error.Should().Be(ErrorConstants.KeyDoesNotExist);
            response.Members.Any().Should().BeFalse();
        }

        [Fact]
        public async Task RemoveMemberAsyncShouldRemoveMember()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Foo, Pop);
            var response = await _sut.RemoveMemberAsync(Foo, Bar);
            response.HasError.Should().BeFalse();
            var membersResponse = await _sut.GetMembersAsync(Foo);
            membersResponse.Members.Count().Should().Be(3);
            membersResponse.Members.Any(m => m == Bar).Should().BeFalse();
        }

        [Fact]
        public async Task RemoveMemberAsyncWithNonExistentKeyShouldReturnError()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Foo, Pop);
            var response = await _sut.RemoveMemberAsync(Fizz, Bar);
            response.HasError.Should().BeTrue();
            response.Error.Should().Be(ErrorConstants.KeyDoesNotExist);
        }

        [Fact]
        public async Task RemoveAllAsyncShouldRemoveItemAndMembers()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Foo, Pop);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            var response = await _sut.RemoveAllAsync(Foo);
            response.HasError.Should().BeFalse();
            var memberResponse = await _sut.GetMembersAsync(Foo);
            memberResponse.HasError.Should().BeTrue();
            memberResponse.Error.Should().Be(ErrorConstants.KeyDoesNotExist);
        }

        [Fact]
        public async Task RemoveAllWithNonExistentKeyShouldReturnError()
        {
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            var response = await _sut.RemoveAllAsync(Foo);
            response.HasError.Should().BeTrue();
            response.Error.Should().Be(ErrorConstants.KeyDoesNotExist);
        }

        [Fact]
        public async Task KeyExistsWithExistingKeyShouldReturnTrue()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Foo, Pop);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            (await _sut.KeyExistsAsync(Fizz)).Should().BeTrue();
        }

        [Fact]
        public async Task KeyExistsWithNonExistentKeyShouldReturnFalse()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Foo, Pop);
            (await _sut.KeyExistsAsync(Fizz)).Should().BeFalse();
        }

        [Fact]
        public async Task MemberExistsWithExistingMemberShouldReturnTrue()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Foo, Pop);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            (await _sut.MemberExistsAsync(Foo, Bar)).Should().BeTrue();
        }

        [Fact]
        public async Task MemberExistsWithNonExistentMemberShouldReturnFalse()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            (await _sut.MemberExistsAsync(Foo, Pop)).Should().BeFalse();
        }

        [Fact]
        public async Task MemberExistsWithNonExistentKeyShouldReturnFalse()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            (await _sut.MemberExistsAsync(Fizz, Buzz)).Should().BeFalse();
        }

        [Fact]
        public async Task AllMembersShouldReturnAllMembers()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            var members = await _sut.GetAllMembersAsync();
            members.Count().Should().Be(5);
            members.Count(m => m == Bar).Should().Be(1);
            members.Count(m => m == Baz).Should().Be(1);
            members.Count(m => m == Buzz).Should().Be(2);
            members.Count(m => m == Pop).Should().Be(1);
        }

        [Fact]
        public async Task AllMembersShouldReturnEmptyListWhenRepositoryIsEmpty()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            await _sut.ClearAsync();
            var members = await _sut.GetAllMembersAsync();
            members.Count().Should().Be(0);
        }

        [Fact]
        public async Task ItemsShouldReturnAllItems()
        {
            await _sut.AddAsync(Foo, Bar);
            await _sut.AddAsync(Foo, Baz);
            await _sut.AddAsync(Foo, Buzz);
            await _sut.AddAsync(Fizz, Buzz);
            await _sut.AddAsync(Fizz, Pop);
            var items = await _sut.GetAllItemsAsync();
            items.Count().Should().Be(2);
            var foo = items.Where(i => i.Key == Foo).First();
            foo.Members.Count().Should().Be(3);
            foo.Members.Any(m => m == Bar).Should().BeTrue();
            foo.Members.Any(m => m == Baz).Should().BeTrue();
            foo.Members.Any(m => m == Buzz).Should().BeTrue();
            var fizz = items.Where(i => i.Key == Fizz).First();
            fizz.Members.Count().Should().Be(2);
            fizz.Members.Any(m => m == Buzz).Should().BeTrue();
            fizz.Members.Any(m => m == Pop).Should().BeTrue();
        }

        [Fact]
        public async Task ItemsShouldReturnEmptyListWhenRepositoryIsEmpty()
        {
            var items = await _sut.GetAllItemsAsync();
            items.Count().Should().Be(0);
        }
    }
}