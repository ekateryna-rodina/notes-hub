using LightNote.Api.Contracts.Insight.Response;
using LightNote.Api.Contracts.Notebook.Response;
using LightNote.Api.Contracts.PermanentNote.Response;
using LightNote.Api.Contracts.Question.Response;
using LightNote.Api.Contracts.Reference.Response;
using LightNote.Api.Contracts.SlipNote.Response;
using LightNote.Api.Contracts.Tag.Response;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using Mapster;

namespace LightNote.Api.Utils
{
    public class MappingProfiles
    {
        public static void Init()
        {
            TypeAdapterConfig<Tag, TagResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<Reference, ReferenceResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<Notebook, NotebookResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value)
               .Map(dest => dest.Title, src => src.Title.Value);
            TypeAdapterConfig<SlipNote, SlipNoteResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<PermanentNote, PermanentNoteResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<Insight, InsightResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<Question, QuestionResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
        }

    }
}

