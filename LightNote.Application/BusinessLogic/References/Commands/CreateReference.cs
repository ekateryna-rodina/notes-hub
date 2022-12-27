﻿using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Commands
{
	public class CreateReference : IRequest<Reference>
	{
		public string Name { get; set; } = default!;
	}
}
