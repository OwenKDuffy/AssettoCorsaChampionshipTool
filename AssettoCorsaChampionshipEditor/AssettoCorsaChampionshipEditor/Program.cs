using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssettoCorsaChampionshipEditor
{

    // static class to hold global variables, etc.
    static class Globals
    {
        // global int
        public static string acDir = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\assettocorsa";

        public static Dictionary<string, string> PopulateDictionaryWithValues(string[] fileContents)
        {
            string[] values = fileContents.Where(c => c.Contains("=")).ToArray();
            Array.ForEach(values, x => x = x.EndsWith("=") ? x + " " : x);
            Dictionary<string, string> seriesDetails = values
                                           .Select(x => x.Split('='))
                                           .ToDictionary(x => x[0], x => x[1]);
            return seriesDetails;
        }

        private static string listAndNumber(string[] list)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var item in list.Select((value, i) => (value, i)))
            {
                sb.AppendLine($"{item.i}: {item.value}");
            }
            return sb.ToString();
        }

        public static void prettyPrintList(string[] list, int numPerLine = 3)
        {
            for (int i = 0; i < list.Length - numPerLine; i += numPerLine)
            {
                Console.WriteLine($"{list[i],20}\t{list[i + 1],20}\t{list[i + 2],20}");
            }
        }
        public static string[] GetListOfCars()
        {
            return Directory.GetDirectories(acDir + "\\content\\cars").Select(Path.GetFileName).ToArray();
        }

        public static string[] GetSkinsForCar(string carName)
        {
            return Directory.GetDirectories($"{acDir}\\content\\cars\\{carName}\\skins").Select(Path.GetFileName).ToArray();
        }

        public static string[] GetListOfTracks()
        {
            return Directory.GetDirectories(acDir + "\\content\\tracks").Select(Path.GetFileName).ToArray();
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(Globals.acDir);
            string[] cars = Globals.GetListOfCars();
            string[] tracks = Globals.GetListOfTracks();

            //Championship emptyChamp = new Championship();
            //emptyChamp.SetPlayerCar(cars[96]);
            //var e = emptyChamp.getEvent(0);
            //e.setTrack(tracks[6]);

            //emptyChamp.addEvent();
            //emptyChamp.getEvent(1).setTrack(tracks[13]);


            //emptyChamp.PrettyPrintChampionshipDetails();
            //emptyChamp.ExportChampionshipToFile(Globals.acDir);

            //File.WriteAllTextAsync($"C:\\Users\\Owen\\Desktop\\AssettoExamples\\carsList.txt", listAndNumber(cars));

            Championship c = new Championship(Globals.acDir + "\\content\\career\\series_f1_2020_championship");
            //////c.getEvent(0)
            c.SetAllCars(cars[221]);
            c.PrettyPrintChampionshipDetails();
            c.ExportChampionshipToFile(Globals.acDir);
            //Console.WriteLine("Exporting");
            //Console.WriteLine("\nCars:\n");
            //Array.ForEach(cars, Console.WriteLine);
            //prettyPrintList(cars);
            //Console.WriteLine("\n\nTracks:\n");
            //Array.ForEach(tracks, Console.WriteLine);
        }


        
    }
}
