
using OnlineShop.Domain.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Domain.SeedWork
{
	public class BaseEntity
	{
		public int Id { get; protected set; }
		[NotMapped]
		internal List<DomainError> Errors { get; set; }
		public BaseEntity()
		{
			Errors = new List<DomainError>();
		}
		public void AddError(string error)
		{
			Errors.Add(new DomainError(error));
		}
	}
}