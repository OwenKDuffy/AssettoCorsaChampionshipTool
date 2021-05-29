using System;
using System.IO;

public class Opponents
{
	public Opponents()
	{
	}

    internal void ExportToFile(string championshipDirectory)
    {
        File.WriteAllTextAsync(String.Format("{0}\\opponents.ini", championshipDirectory), "temp text");
    }
}
