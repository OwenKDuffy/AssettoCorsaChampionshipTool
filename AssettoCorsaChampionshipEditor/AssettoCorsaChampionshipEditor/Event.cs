using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssettoCorsaChampionshipEditor
{
    public class Event
    {
        private string eventTemplate = @"[EVENT]
NAME = {0}
DESCRIPTION = {1}

[RACE]
TRACK = {2}
CONFIG_TRACK = {3}
MODEL = {4}
CARS = 20
AI_LEVEL = 100
FIXED_SETUP = 0
PENALTIES = 1
DRIFT_MODE = 0
RACE_LAPS = 22
ARM_FIRST_LAP = 0
SKIN =

[CAR_0]
SETUP =
MODEL = {4}
SKIN = {5}
DRIVER_NAME =
NATIONALITY = ";
        public int eventIndex;
        public string name, vehicle;
        private string description, weather, track, layout, 
            trackSkin, carSkin, imageSrc;

        /// <summary>
        /// Creates an Event object based on a specified input file
        /// </summary>
        /// <param name="eventNumber"></param>
        /// <param name="pathToDirectory"></param>
        public Event(int eventNumber, string pathToDirectory)
        {
            eventIndex = eventNumber;
            string[] fileContents = File.ReadAllLines(pathToDirectory + "\\event.ini");
            string[] sectionHeaders = fileContents.Where(c => (new[] { "[", "]" }).Any(c.Contains)).ToArray();
            foreach (var item in sectionHeaders.Select((value, i) => (value, i)))
            {
                string sectionHeader = new string(item.value.Where(c => !Char.IsWhiteSpace(c)).ToArray()); 
                int thisSection = Array.IndexOf(fileContents, sectionHeader);
                int lengthToTake = (item.i + 1 < sectionHeaders.Length ? Array.IndexOf(fileContents, sectionHeaders[item.i + 1]) : fileContents.Length) - thisSection;
                foreach (string line in fileContents.Skip(thisSection).Take(lengthToTake).ToArray())
                {
                    string x = line.EndsWith("=") ? line + " " : line;
                    string[] z = x.Split('=');
                    if (sectionHeader.Equals("[EVENT]"))
                    {
                        switch (z[0])
                        {
                            case "NAME":
                                name = z[1];
                                break;
                            case "DESCRIPTION":
                                description = z[1];
                                break;
                            default:
                                break;
                        }
                    }
                    else if (sectionHeader.Equals("[RACE]"))
                    {
                        switch (z[0])
                        {
                            case "MODEL":
                                vehicle = z[1];
                                break;
                            case "TRACK":
                                track = z[1];
                                break;
                            case "CONFIG_TRACK":
                                layout = z[1];
                                break;
                            //case "TIER2":
                            //    silverGoal = int.Parse(z[1]);
                            //    break;
                            //case "TIER3":
                            //    bronzeGoal = int.Parse(z[1]);
                            //    break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (File.Exists($"{pathToDirectory}\\preview.png"))
            {
                imageSrc = $"{pathToDirectory}\\preview.png";
            }
        }

        internal void setTrack(string inputTrack)
        {
            track = inputTrack;
            if (String.IsNullOrEmpty(imageSrc))
            {
                imageSrc = $"{Globals.acDir}\\content\\tracks\\{inputTrack}\\ui\\preview.png";
            }
        }

        /// <summary>
        /// Creates a brand new Event Item
        /// </summary>
        /// <param name="i"></param>
        public Event(int i)//, string playerVehicle)
        {
            this.eventIndex = i;
            this.name = "Untitled Event";
            //this.vehicle = playerVehicle;
        }

        public void setPlayerVehicle(string playerVehicle, string skin)
        {
            this.vehicle = playerVehicle;
            this.carSkin = skin;
        }
        internal async void CreateEventFolder(string parentDirectory)
        {
            string championshipDirectory = String.Format("{0}\\event{1}", parentDirectory, eventIndex + 1);
            Directory.CreateDirectory(championshipDirectory);
            await File.WriteAllTextAsync(String.Format("{0}\\event.ini", championshipDirectory), CreateString());
            if (imageSrc != null)
            {
                string ext = Path.GetExtension(imageSrc);
                if (File.Exists(imageSrc))
                {
                    File.Copy(imageSrc, String.Format("{0}\\preview{1}", championshipDirectory, ext));
                }
            }
        }

        private string CreateString()
        {
            return String.Format(eventTemplate, name, description, track, layout, vehicle, carSkin);
        }
    }
}