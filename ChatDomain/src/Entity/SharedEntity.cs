using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.src.Entity
{
	public class SharedEntity
	{
		[Key]
		public Guid id { get; set; }

	}
}
