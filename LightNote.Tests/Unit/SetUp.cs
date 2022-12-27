using LightNote.Tests.Unit.Fixtures;

namespace LightNote.Tests.Unit
{
    [CollectionDefinition("SetupFixture")]
    public class SetUpFixture : ICollectionFixture<LightNoteDbFixture>
    {
    }       
}

