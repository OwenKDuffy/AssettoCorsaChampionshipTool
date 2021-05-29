using System;
using System.IO;

public class Event
{
    private int eventIndex;
    private string imageSrc;

    public Event()
	{
	}

    public Event(int i)
    {
        this.eventIndex = i;
    }

    internal async void CreateEventFolder(string parentDirectory)
    {
        string championshipDirectory = String.Format("{0}\\event{1}", parentDirectory, eventIndex + 1);
        Directory.CreateDirectory(championshipDirectory);
        await File.WriteAllTextAsync(String.Format("{0}\\event.ini", championshipDirectory), CreateString());
        if (imageSrc != null)
        {
            string ext = Path.GetExtension(imageSrc);
            File.Copy(imageSrc, String.Format("{0}\\preview.{1}", championshipDirectory, ext));
        }
    }

    private string CreateString()
    {
        return "temp text";
    }
}
