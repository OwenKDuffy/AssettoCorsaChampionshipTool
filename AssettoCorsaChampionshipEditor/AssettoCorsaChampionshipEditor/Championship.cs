using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssettoCorsaChampionshipEditor
{
    public class Championship
    {
        int numberOfEvents = 1;
        string code, name, description, imageSrc;
        public string playerVehicle;
        int[] pointsScale;
        int pointsGoal, goldGoal, silverGoal, bronzeGoal;
        List<Event> events;
        Opponents opponents;
        public Championship()
        {
            name = "Untitled Championship";
            playerVehicle = "";
            events = new List<Event>();
            events.Add(new Event(0));
            events[0].setPlayerVehicle(playerVehicle);
            pointsScale = new int[6] { 10, 6, 4, 3, 2, 1 };
            pointsGoal = 50;
            opponents = new Opponents();
        }

        internal void addEvent()
        {
            events.Add(new Event(events.Count));
            events[events.Count - 1].setPlayerVehicle(playerVehicle);
        }

        public Championship(string pathToDirectory)
        {
            numberOfEvents = Directory.GetDirectories(pathToDirectory).Length;
            for (int i = 0; i < numberOfEvents; i++)
            {
                events.Add(new Event(i, String.Format("{0}\\event{1}", pathToDirectory, i + 1)));
            }
            string[] fileContents = File.ReadAllLines(pathToDirectory + "\\series.ini");
            string[] sectionHeaders = fileContents.Where(c => (new[] { "[", "]" }).Any(c.Contains)).ToArray();
            Dictionary<string, Dictionary<string, string>> champProps = new Dictionary<string, Dictionary<string, string>>();
            for (int i = 0; i < sectionHeaders.Length; i++)
            {
                string sectionHeader = sectionHeaders[i];
                int thisSection = Array.IndexOf(fileContents, sectionHeader);
                int lengthToTake = (i + 1 < sectionHeaders.Length ? Array.IndexOf(fileContents, sectionHeaders[i + 1]) : fileContents.Length) - thisSection;
                champProps.Add(sectionHeader, PopulateDictionaryWithValues(sectionHeader, fileContents.Skip(thisSection).Take(lengthToTake).ToArray()));
            }

            code = champProps["[SERIES]"]["CODE"];
            name = champProps["[SERIES]"]["NAME"];
            description = champProps["[SERIES]"]["DESCRIPTION"];
            playerVehicle = champProps["[SERIES]"]["MODEL"];
            pointsGoal = int.Parse(champProps["[GOALS]"]["POINTS"]);
            pointsScale = champProps["[SERIES]"]["POINTS"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            goldGoal = int.Parse(champProps["[GOALS]"]["TIER1"]);
            silverGoal = int.Parse(champProps["[GOALS]"]["TIER2"]);
            bronzeGoal = int.Parse(champProps["[GOALS]"]["TIER3"]);
            opponents = new Opponents();
        }

        public Event getEvent(int index)
        {
            return events[index];
        }
        public void SetPlayerCar(string carName)
        {
            playerVehicle = carName;
            foreach (Event e in events)
            {
                e.setPlayerVehicle(carName);
            }
        }
        private Dictionary<string, string> PopulateDictionaryWithValues(string preFix, string[] fileContents)
        {
            string[] values = fileContents.Where(c => c.Contains("=")).ToArray();
            Array.ForEach(values, x => x = x.EndsWith("=") ? x + " " : x);
            Dictionary<string, string> seriesDetails = values
                                           .Select(x => x.Split('='))
                                           .ToDictionary(x => x[0], x => x[1]);
            return seriesDetails;
        }

        public async void ExportChampionshipToFile(string acFolder)
        {
            string championshipDirectory = String.Format("{0}\\content\\career\\series_{1}", acFolder, "testingName");// name.Replace(" ", "_"));
            Directory.CreateDirectory(championshipDirectory);
            await File.WriteAllTextAsync(String.Format("{0}\\series.ini", championshipDirectory), CreateString());
            foreach (Event e in events)
            {
                e.CreateEventFolder(championshipDirectory);
            }
            if (imageSrc != null)
            {
                string ext = Path.GetExtension(imageSrc);
                File.Copy(imageSrc, String.Format("{0}\\preview.{1}", championshipDirectory, ext));
            }
            opponents.ExportToFile(championshipDirectory);
            Console.WriteLine("Exported");
        }

        private string CreateString()
        {
            return String.Format(@"[SERIES]
CODE = {0}
NAME = {1}
DESCRIPTION = {2}
REQUIRES =
POINTS = {3}
MODEL = {4}
REQUIRESANY = 0

[GOALS]
POINTS = {5}
TIER1 = {6}
TIER2 = {7}
TIER3 = {8}
RANKING = 0", code, name, description, string.Join(", ", pointsScale), playerVehicle, pointsGoal, goldGoal, silverGoal, bronzeGoal);
        }
        public void PrettyPrintChampionshipDetails()
        {
            Console.WriteLine("Name: {0}\nDescription: {1}\nVehicle: {2}\nNumber of Events: {3}\nAchieve {4} Points", name, description, playerVehicle, numberOfEvents, pointsGoal);
            for (int i = 0; i < pointsScale.Length; i++)
            {
                Console.WriteLine("P{0}: {1} points", i + 1, pointsScale[i]);
            }
        }
    }

}