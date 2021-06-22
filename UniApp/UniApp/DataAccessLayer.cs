using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniApp.Model;

namespace UniApp
{
    public static class DataAccessLayer
    {
        private static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static Semester CurrentSemester { get; set; }
        public static Course CurrentCourse { get; set; }

        public static void SaveNew(int sem, int year)
        {
            int[] semYear = new int[2] { sem, year };
            foreach (string filename in LoadAll())
            {
                if (Enumerable.SequenceEqual(semYear, FilenameToSemYear(filename)))
                    throw new ApplicationException($"The profile <{FilenameToProfile(filename)}> already exists");
            }

            Semester semester = new Semester() { SemYear = semYear };
            string json = JsonConvert.SerializeObject(semester);
            File.WriteAllText(Path.Combine(folderPath, semester.Filename), json);
        }

        public static string[] LoadAll()
        {
            return Directory.GetFiles(folderPath)
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .OrderByDescending(f => FilenameToOrder(f))
                .ToArray();
        }

        public static List<string> LoadAllProfile()
        {
            string[] filenames = LoadAll();
            List<string> profiles = new List<string>(filenames.Length);
            foreach (string name in filenames)
            {
                profiles.Add(FilenameToProfile(name));
            }
            return profiles;
        }

        public static void Load()
        {
            string[] filenames = LoadAll();

            if (filenames.Length == 0)
                throw new ApplicationException("No saved data found");

            string json = File.ReadAllText(Path.Combine(folderPath, filenames[0]) + ".txt");
            CurrentSemester = JsonConvert.DeserializeObject<Semester>(json);
        }

        public static void Delete(string filename)
        {
            File.Delete(Path.Combine(folderPath, filename + ".txt"));
        }

        public static void DeleteProfile(string profile)
        {
            string filename = ProfileToFilename(profile);
            Delete(filename);
        }


        #region Private helper functions
        private static int[] FilenameToSemYear(string filename)
        {
            return new int[2] { Convert.ToInt32(filename[0]), Convert.ToInt32(filename.Substring(2)) };
        }

        private static string FilenameToProfile(string filename)
        {
            return $"Semester {filename[0]}, {filename.Substring(2)}";
        }

        private static string ProfileToFilename(string profile)
        {
            return $"{profile[9]}_{profile.Substring(12)}";
        }

        private static int FilenameToOrder(string filename)
        {
            return Convert.ToInt32($"{filename.Substring(2)}{filename[0]}");
        }
        #endregion
    }
}
