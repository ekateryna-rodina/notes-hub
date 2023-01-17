using System;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
	public class InsightPermanentNote
	{
        public InsightId InsightId { get; set; }
        public Insight Insight { get; set; }

        public PermanentNoteId PermanentNoteId { get; set; }
        public PermanentNote PermanentNote { get; set; }
    }
}

