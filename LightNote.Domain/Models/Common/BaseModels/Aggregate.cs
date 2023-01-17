using System;
namespace LightNote.Domain.Models.Common.BaseModels
{
	public class Aggregate<TId> : Entity<TId>
	{
		protected Aggregate(TId id) : base(id)
		{
		}
        protected Aggregate()
        {
        }
    }
}

