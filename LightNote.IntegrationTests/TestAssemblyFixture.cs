using System;
using LightNote.IntegrationTests.Setup;

namespace LightNote.IntegrationTests
{
    using static TestUtils;
    [SetUpFixture]
    public class TestAssemblyFixture
    {
        [OneTimeSetUp]
        public async Task RunBeforeAllTestsAsync()
        {
            await RunBeforeAnyTests();
        }

        [OneTimeTearDown]
        public async Task RunAfterAllTestsAsync()
        {
            await RunAfterAllTests();
        }
    }
}

