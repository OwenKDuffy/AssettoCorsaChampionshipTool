using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssettoCorsaChampionshipEditor
{
    public class Opponents
    {
        private class Opponent
        {
            /// <summary>
            /// Create an opponent with the id provided given the attributes specified in input
            /// </summary>
            /// <param name="id"></param>
            /// <param name="input"></param>
            public Opponent(int id, string[] input)
            {
                this.id = id;
                input = input.Where(c => c.Contains("=")).ToArray();
                foreach (string line in input)
                {
                    string x = line.EndsWith("=") ? line + " " : line;
                    string[] z = x.Split('=');
                    switch (z[0])
                    {
                        case "MODEL":
                            vehicle = z[1];
                            break;
                        case "SETUP":
                            setup = z[1];
                            break;
                        case "AI_LEVEL":
                            level = int.Parse(z[1]);
                            break;
                        case "SKIN":
                            skin = z[1];
                            break;
                        case "DRIVER_NAME":
                            driverName = z[1];
                            break;
                        case "NATIONALITY":
                            nationality = z[1];
                            break;
                        default:
                            break;
                    }

                }
            }

            #region opponentattributes
            public int id { get; set; }
            public string vehicle { get; set; }
            public string setup { get; set; }
            public int level { get; set; }
            public string skin { get; set; }
            public string driverName { get; set; }
            public string nationality { get; set; }
            #endregion
            public override string ToString()
            {
                return $"[AI{id + 1}]\nMODEL = {vehicle}\nSETUP = {setup}\nAI_LEVEL = {level}\nSKIN = {skin}\nDRIVER_NAME = {driverName}\nNATIONALITY = {nationality}\n";
            }
        }
        
        private List<Opponent> opponents;
        
        /// <summary>
        /// Creates an unitialised opponents object
        /// </summary>
        public Opponents()
        {
        }
        
        
        
        /// <summary>
        /// Creates an opponents object from an existing opponents file
        /// </summary>
        /// <param name="pathToDirectory"></param>
        public Opponents(string pathToDirectory)
        {
            string[] fileContents = File.ReadAllLines(pathToDirectory + "\\opponents.ini");
            string[] sectionHeaders = fileContents.Where(c => (new[] { "[", "]" }).Any(c.Contains)).ToArray();
            opponents = new List<Opponent>();
            foreach (var sectionHeader in sectionHeaders.Select((value, i) => (value, i)))
            {
                int thisSection = Array.IndexOf(fileContents, sectionHeader.value);
                int lengthToTake = (sectionHeader.i + 1 < sectionHeaders.Length ? Array.IndexOf(fileContents, sectionHeaders[sectionHeader.i + 1]) : fileContents.Length) - thisSection;
                opponents.Add(new Opponent(sectionHeader.i, fileContents.Skip(thisSection).Take(lengthToTake).ToArray()));
            }
        }

        internal void ExportToFile(string championshipDirectory)
        {
            var sb = new System.Text.StringBuilder();
            foreach (Opponent opp in opponents)
            {
                sb.AppendLine(opp.ToString());
            }
            File.WriteAllTextAsync($"{championshipDirectory}\\opponents.ini", sb.ToString());
        }

        internal void SetAllCars(string carToSet, string[] skins)
        {
            foreach (Opponent opp in opponents)
            {
                opp.vehicle = carToSet;
                opp.skin = skins[opp.id + 1];
            }
        }
    }
}