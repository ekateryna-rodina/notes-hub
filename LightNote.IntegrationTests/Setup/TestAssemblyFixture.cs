using System;
namespace LightNote.IntegrationTests.Setup
{
    using static TestBase;
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

