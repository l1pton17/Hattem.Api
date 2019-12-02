using System.Linq;
using System.Threading.Tasks;
using Hattem.Api.Collections;
using Hattem.Api.Tests.Framework;
using Xunit;

namespace Hattem.Api.Tests.Collections
{
    [CategoryTrait("ObjectCache")]
    public sealed class ObjectCacheTests
    {
        [Fact(DisplayName = "Should get or create object in multi-thread environment")]
        public async Task Get()
        {
            static (int, Task<ApiResponse<Unit>>) ValueFactory(int s)
            {
                var data = Task.FromResult(new ApiResponse<Unit>(s, Unit.Default));

                return (s, data);
            }

            var cache = new ObjectCache<int, (int Expected, Task<ApiResponse<Unit>> Value)>(maxCapacity: 5);

            var tasks = Enumerable
                .Range(0, 100)
                .Select(v => v % 10)
                .Select(v => Task.Run(() => cache.GetOrAdd(v, ValueFactory)))
                .ToArray();

            await Task.WhenAll(tasks);

            Assert.All(
                tasks,
                t => Assert.Equal(
                    t.Result.Expected,
                    t.Result.Value.Result.StatusCode));

        }
    }
}
