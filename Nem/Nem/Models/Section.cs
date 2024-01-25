using System.Collections.Generic;

namespace Nem.Models
{
	public class Section
	{
		#region Properties
		public int SectionID { get; set; }
		public string Name { get; set; }
		public List<Shelf> Shelfs { get; set; }
		#endregion Properties
	}
}