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
        private int eventIndex;
        private string name, description, weather, track, layout, vehicle, trackSkin, carSkin, imageSrc;

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
            Dictionary<string, Dictionary<string, string>> eventProps = new Dictionary<string, Dictionary<string, string>>();
            for (int i = 0; i < sectionHeaders.Length; i++)
            {
                string sectionHeader = sectionHeaders[i];
                int thisSection = Array.IndexOf(fileContents, sectionHeader);
                int lengthToTake = (i + 1 < sectionHeaders.Length ? Array.IndexOf(fileContents, sectionHeaders[i + 1]) : fileContents.Length) - thisSection;
                eventProps.Add(sectionHeader, PopulateDictionaryWithValues(sectionHeader, fileContents.Skip(thisSection).Take(lengthToTake).ToArray()));
            }
            name = eventProps["[EVENT]"]["NAME"];
            description = eventProps["[EVENT]"]["DESCRIPTION"];
            vehicle = eventProps["[RACE]"]["MODEL"];
            track = eventProps["[RACE]"]["TRACK"];
            try
            {
                layout = eventProps["[RACE]"]["CONFIG_TRACK"];
            }
            catch (KeyNotFoundException)
            {
                layout = "";
            }
            carSkin = eventProps["[CAR_0]"]["SKIN"];
            //pointsGoal = int.Parse(eventProps["[GOALS]"]["POINTS"]);
            //pointsScale = eventProps["[EVENT]"]["POINTS"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            //goldGoal = int.Parse(eventProps["[GOALS]"]["TIER1"]);
            //silverGoal = int.Parse(eventProps["[GOALS]"]["TIER2"]);
            //bronzeGoal = int.Parse(eventProps["[GOALS]"]["TIER3"]);
            //opponents = new Opponents();

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

        public void setPlayerVehicle(string playerVehicle)
        {
            this.vehicle = playerVehicle;
        }
        internal async void CreateEventFolder(string parentDirectory)
        {
            string championshipDirectory = String.Format("{0}\\event{1}", parentDirectory, eventIndex + 1);
            Directory.CreateDirectory(championshipDirectory);
            await File.WriteAllTextAsync(String.Format("{0}\\event.ini", championshipDirectory), CreateString());
            if (imageSrc != null)
            {
                string ext = Path.GetExtension(imageSrc);
                if (File.Exists(imageSrc)) { 
                    File.Copy(imageSrc, String.Format("{0}\\preview.{1}", championshipDirectory, ext));
                }
            }
        }

        //TODO: Move this to somewhere can be accessed by multiple classes
        private Dictionary<string, string> PopulateDictionaryWithValues(string preFix, string[] fileContents)
        {
            string[] values = fileContents.Where(c => c.Contains("=")).ToArray();
            Array.ForEach(values, x => x = x.EndsWith("=") ? x + " " : x);
            Dictionary<string, string> seriesDetails = values
                                           .Select(x => x.Split('='))
                                           .ToDictionary(x => x[0], x => x[1]);
            return seriesDetails;
        }

        private string CreateString()
        {
            return String.Format(eventTemplate, name, description, track, layout, vehicle, carSkin);
        }
    }
}