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
            Semester semester = new Semester(sem, year);
            string json = JsonConvert.SerializeObject(semester);
            File.WriteAllText(Path.Combine(folderPath, semester.Filename), json);
        }

        public static string[] LoadAll()
        {
            return Directory.GetFiles(folderPath)
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .OrderByDescending(f => f)
                .ToArray();
        }

        public static void Load()
        {
            string[] filenames = LoadAll();

            if (filenames.Length == 0)
                throw new ApplicationException("No saved data found");

            string json = File.ReadAllText(Path.Combine(folderPath, filenames[0]));
            CurrentSemester = JsonConvert.DeserializeObject<Semester>(json);
        }

        public static void Delete(string filename)
        {
            File.Delete(Path.Combine(folderPath, filename + ".txt"));
        }
    }
}
