using API.Data.Models;

namespace API.Data.ModelViews
{
	public class DriverView
	{
		public string Position { get; set; }
		public string Name { get; set; }
		public string Nationality { get; set; }
		public string Team { get; set; }
		public float Points { get; set; }

		public DriverView(string position, string name, 
			string nationality, string team, float points)
		{
			Position = position;
			Name = name;
			Nationality = nationality;
			Team = team;
			Points = points;
		}
	}
}
