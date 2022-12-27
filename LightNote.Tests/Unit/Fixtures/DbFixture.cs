using System;
using Npgsql;
using Respawn;

namespace LightNote.Tests.Unit.Fixtures
{
    public abstract class DbFixture : IAsyncLifetime
    {
        public abstract Task DisposeAsync();

        public abstract Task InitializeAsync();
    }
}

