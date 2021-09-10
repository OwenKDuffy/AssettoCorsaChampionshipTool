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
        public string playerVehicle, carSkin;
        int[] pointsScale;
        int pointsGoal, goldGoal, silverGoal, bronzeGoal;
        List<Event> events;
        Opponents opponents;
        public Championship()
        {
            name = "Untitled Championship";
            playerVehicle = "";
            carSkin = "";
            events = new List<Event>();
            events.Add(new Event(0));
            events[0].setPlayerVehicle(playerVehicle, "");
            pointsScale = new int[6] { 10, 6, 4, 3, 2, 1 };
            pointsGoal = 50;
            opponents = new Opponents();
        }

        internal void addEvent()
        {
            events.Add(new Event(events.Count));
            events[events.Count - 1].setPlayerVehicle(playerVehicle, carSkin);
        }

        public Championship(string pathToDirectory)
        {
            events = new List<Event>();
            numberOfEvents = Directory.GetDirectories(pathToDirectory).Length;
            for (int i = 0; i < numberOfEvents; i++)
            {
                events.Add(new Event(i, String.Format("{0}\\event{1}", pathToDirectory, i + 1)));
            }
            string[] fileContents = File.ReadAllLines(pathToDirectory + "\\series.ini");
            string[] sectionHeaders = fileContents.Where(c => (new[] { "[", "]" }).Any(c.Contains)).ToArray();
            foreach (var item in sectionHeaders.Select((value, i) => (value, i)))
            {
                string sectionHeader = item.value;
                int thisSection = Array.IndexOf(fileContents, sectionHeader);
                int lengthToTake = (item.i + 1 < sectionHeaders.Length ? Array.IndexOf(fileContents, sectionHeaders[item.i + 1]) : fileContents.Length) - thisSection;
                foreach (string line in fileContents.Skip(thisSection).Take(lengthToTake).ToArray())
                {
                    string x = line.EndsWith("=") ? line + " " : line;
                    string[] z = x.Split('=');
                    if (sectionHeader.Equals("[SERIES]"))
                    {
                        switch (z[0])
                        {
                            case "CODE":
                                code = z[1];
                                break;
                            case "NAME":
                                name = z[1] + " Copy";
                                break;
                            case "DESCRIPTION":
                                description = z[1];
                                break;
                            case "MODEL":
                                playerVehicle = z[1];
                                break;
                            case "POINTS":
                                pointsScale = z[1].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (sectionHeader.Equals("[GOALS]"))
                    {
                        int res;
                        switch (z[0])
                        {
                            case "POINTS":
                                pointsGoal = int.Parse(z[1]);
                                break;
                            case "TIER1":
                                if (int.TryParse(z[1], out res))
                                {
                                    goldGoal = res;
                                }
                                break;
                            case "TIER2":
                                if (int.TryParse(z[1], out res))
                                {
                                    silverGoal = res;
                                }
                                break;
                            case "TIER3":
                                if (int.TryParse(z[1], out res))
                                {
                                    bronzeGoal = res;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            opponents = new Opponents(pathToDirectory);
            if (File.Exists($"{pathToDirectory}\\preview.png"))
            {
                imageSrc = $"{pathToDirectory}\\preview.png";
            }
        }

        internal void SetAllCars(string carToSet)
        {
            string[] skins = Globals.GetSkinsForCar(carToSet);
            SetPlayerCar(carToSet, skins[0]);
            foreach (Event e in events)
            {
                e.setPlayerVehicle(carToSet, skins[0]);
            }
            opponents.SetAllCars(carToSet, skins);
        }

        public Event getEvent(int index)
        {
            return events[index];
        }
        public void SetPlayerCar(string carName, string skin)
        {
            playerVehicle = carName;
            carSkin = skin;
            foreach (Event e in events)
            {
                e.setPlayerVehicle(carName, skin);
            }
        }


        public async void ExportChampionshipToFile(string acFolder)
        {
            string championshipDirectory = String.Format("{0}\\content\\career\\series_{1}", acFolder, name.ToLower().Replace(" ", "_"));
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
RANKING = 0", code, name, description, string.Join(",", pointsScale), playerVehicle, pointsGoal, goldGoal, silverGoal, bronzeGoal);
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