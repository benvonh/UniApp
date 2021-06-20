using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniApp.Model
{
    public class Semester
    {
        private int[] semYear;
        private int? gpa;
        private List<Course> courses;

        public Semester(int sem, int year)
        {
            SemYear = new int[] { sem, year };
            gpa = null;
            courses = new List<Course>();
        }

        public int[] SemYear
        {
            get => semYear;
            set
            {
                if (value[0] != 1 || value[0] != 2)
                    throw new ArgumentException("Semester must be 1 or 2");

                semYear = value;
            }
        }

        public string SemYearStr => $"Sem {semYear[0]}, Year {semYear[1]}";

        public string Filename => $"{semYear[0]}_{semYear[1]}.txt";

        public List<Course> Courses => courses;

        public int GPA
        {
            get
            {
                if (gpa is null)
                    gpa = Convert.ToInt32(courses.Average(item => item.Grade));
                return gpa.Value;
            }
        }

        public void AddCourse(string code)
        {
            Course course = new Course()
            {
                Code = code
            };
            courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            if (!courses.Remove(course))
                throw new ArgumentException("Error: Course not found");
        }
    }
}
