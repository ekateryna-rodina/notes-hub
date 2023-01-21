using System;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
	public class ReferenceTag
	{
		public ReferenceId ReferenceId { get; set; }
        public Reference Reference { get; set; }

		public TagId TagId { get; set; }
        public Tag Tag { get; set; }
    }
}

