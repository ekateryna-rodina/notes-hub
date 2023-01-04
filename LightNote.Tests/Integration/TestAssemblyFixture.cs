using System;
using NUnit.Framework;

namespace LightNote.Tests.Integration
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

