using System;
using LightNote.Api.Contracts.Notebook.Response;
using LightNote.Api.Contracts.Reference.Response;
using LightNote.Api.Contracts.Tag.Response;
using LightNote.Application.BusinessLogic.Tags.Queries;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using Mapster;

namespace LightNote.Api.Utils
{
	public class MappingProfiles
	{
		public static void Init() {
            TypeAdapterConfig<Tag, TagResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<Reference, ReferenceResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
            TypeAdapterConfig<Notebook, NotebookResponse>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id.Value);
        }

    }
}

