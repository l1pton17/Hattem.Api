﻿using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Parallel
{
    partial class ParallelTests
    {
        [Theory(DisplayName = "(Async ValueTask) Should execute all action in parallel and return ok if all responses are ok")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task AsyncValueTask_Parallel_ResponseAreOk_ExecuteAllActions(bool degreeOfParallelism)
        {
            var values = Enumerable
                .Range(0, 1_000)
                .ToImmutableArray();

            var mock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.OkAsyncValueTask());

            var response = degreeOfParallelism
                ? await values.Parallel(degreeOfParallelism: 3, mock.Object.Execute)
                : await values.Parallel(mock.Object.Execute);

            Assert.True(response.IsOk);

            foreach (var value in values)
            {
                mock.Verify(v => v.Execute(value), Times.Once());
            }
        }
    }
}
