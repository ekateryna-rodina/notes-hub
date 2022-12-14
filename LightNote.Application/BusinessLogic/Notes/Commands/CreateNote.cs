using System;
using System.Xml.Linq;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using MediatR;
using Microsoft.VisualBasic;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class CreateNote : IRequest<Note>
	{
        public Guid UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public bool IsPublic { get; set; }
        public Guid ReferenceId { get; set; }
    }
}

