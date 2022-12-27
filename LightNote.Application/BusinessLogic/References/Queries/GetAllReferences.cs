﻿using System;
using LightNote.Domain.Models.Note;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.Queries
{
	public class GetAllReferences : IRequest<IEnumerable<Reference>>
	{
		
	}
}
