using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UniApp.Model
{
    public class Course
    {
        private string code;
        private List<Assessment> assessments;
        
        public Course(string code, List<Assessment> assessments)
        {

        }

        public string Code
        {
            get => code;
            set
            {
                try
                {
                    string codeAlp = value.Substring(0, 4);
                    string codeNum = value.Substring(4, 4);

                    if (!Regex.IsMatch(codeAlp, "^[a-zA-z]+$") || !Regex.IsMatch(codeNum, "^[0-9]+$"))
                        throw new ArgumentException("Course code must have 4 letters followed by 4 numbers (e.g. ELEC3004)");

                    code = value;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentException("Course code must have 8 characters");
                }
            }
        }

        public List<Assessment> Assessments
        {
            get => assessments;
        }

        public int GetProgress
        {
            get
            {
                int progress = 0;
                foreach (Assessment assessment in assessments)
                {
                    if (assessment.IsComplete)
                        progress += assessment.Weight;
                }
                return progress;
            }
        }

        public void AddAssessment(Assessment assessment)
        {

        }

    }
}
