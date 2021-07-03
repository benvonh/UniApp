using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniApp.Model
{
    public class Semester
    {
        private int[] semYear;
        private List<Course> courses;

        public Semester()
        {
            courses = new List<Course>();
        }

        public int[] SemYear
        {
            get => semYear;
            set
            {
                if (value[0] != 1 && value[0] != 2)
                    throw new ArgumentException("Semester must be 1 or 2");

                semYear = value;
            }
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public string SemYearStr => $"Sem {semYear[0]}, {semYear[1]}";

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public List<Course> Courses => courses;

        public int GetGPA()
        {
            return Convert.ToInt32(courses.Average(item => item.Grade));
        }

        public void AddCourse(string code)
        {
            Course course = new Course()
            {
                Code = code
            };
            courses.Add(course);
        }
    }
}
