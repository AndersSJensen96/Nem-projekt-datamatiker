using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Models
{
	public class Mealtime
	{
		#region Properties
		public int MealtimeID { get; set; }
		public string Name { get; set; }
		// not used in our current prototype but its left in for future used if you want to pull all recipes based on mealtimes  
		public List<Recipe> Recipes { get; set; }
		#endregion
	}
}
