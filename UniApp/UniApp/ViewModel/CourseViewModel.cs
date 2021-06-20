using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UniApp.Model;

namespace UniApp.ViewModel
{
    public class CourseViewModel : BaseViewModel
    {
        private ObservableCollection<Course> courseList;

        public CourseViewModel()
        {
            try
            {
                CourseList = new ObservableCollection<Course>(DataAccessLayer.CurrentSemester.Courses);
            }
            catch
            {
                CourseList = null;
            }
        }

        public ObservableCollection<Course> CourseList
        {
            get => courseList;
            set => SetProperty(ref courseList, value);
        }

        public bool ShowMsg => courseList is null;
        public bool ShowList => courseList != null;
        public string Title
        {
            get
            {
                if (DataAccessLayer.CurrentSemester is null)
                    return "No profile";
                return DataAccessLayer.CurrentSemester.SemYearStr;
            }
        }
    }
}
