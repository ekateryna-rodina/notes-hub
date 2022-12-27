﻿using System;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notes.Commands
{
	public class RemoveCommentFromNote : IRequest
	{
		public Guid Id { get; set; }
		public Guid CommentId { get; set; }
	}
}
