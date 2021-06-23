using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UniApp.Model
{
    public class Course
    {
        private string code;
        private List<Assessment> assessments;
        private readonly int[] GradeReq = new int[] { 85, 75, 65, 50, 0 };

        public Course()
        {
            assessments = new List<Assessment>();
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
            set => assessments = value;
        }

        public int Progress
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

        public int TotalWeight => assessments.Sum(item => item.Weight);

        public int TotalMark => assessments.Sum(item => item.Mark.Value * item.Weight / 100);

        public int[] GradePredict
        {
            get
            {
                int[] gradePredict = new int[5];
                int totalMark = TotalMark;

                for (int grade = 0; grade < 5; grade++)
                {
                    gradePredict[grade] = Math.Max(GradeReq[grade] - totalMark, 0);
                }

                return gradePredict;
            }
        }

        public int Grade
        {
            get
            {
                if (TotalWeight != 100 || Progress != 100)
                    return 0;

                for (int grade = 0; grade < 5; grade++)
                {
                    if (TotalMark > GradeReq[grade])
                        return 7 - grade;
                }
                throw new ApplicationException("Total marks not operable with grade requirements");
            }
        }

        public void AddAssessment(string name, int weight, int hurdle)
        {
            Assessment assessment = new Assessment()
            {
                Name = name,
                Weight = weight,
                Hurdle = hurdle,
                Mark = null
            };

            if (weight > 100 - TotalWeight)
                throw new ArgumentException("Assessment weight cannot exceed 100% total weight");

            assessments.Add(assessment);
        }

        public void RemoveAssessment(Assessment assessment)
        {
            if (!assessments.Remove(assessment))
                throw new ArgumentException("Assessment item not found");
        }
    }
}
