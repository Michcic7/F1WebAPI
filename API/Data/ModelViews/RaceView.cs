using API.Data.Models;

namespace API.Data.ModelViews;

public class RaceView
{
	public string Name { get; set; }
	public string Date { get; set; }
	public string WinnerName { get; set; }
	public string Car { get; set; }
	public int Laps { get; set; }
	public string Time { get; set; }

	public RaceView(string name, string date, string winnerName, string car, int laps, string time)
	{
		Name = name;
		Date = date;
		WinnerName = winnerName;
		Car = car;
		Laps = laps;
		Time = time;
	}
}
