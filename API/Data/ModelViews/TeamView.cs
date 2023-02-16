using API.Data.Models;

namespace API.Data.ModelViews;

public class TeamView
{
	public string Position { get; set; }
	public string Name { get; set; }
	public float Points { get; set; }

	public TeamView(string position, string name, float points)
	{
		Position = position;
		Name = name;
		Points = points;
	}
}
