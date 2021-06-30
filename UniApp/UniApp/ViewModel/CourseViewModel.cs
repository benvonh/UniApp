using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UniApp.Model;
using UniApp.View;
using Xamarin.Forms;

namespace UniApp.ViewModel
{
    public class CourseViewModel : BaseViewModel
    {
        private ObservableCollection<Course> courseList;
        private int courseIndex;
        private bool showMsg;
        private bool showList;
        private string title;

        public CourseViewModel()
        {
            UpdateView();
            AddCourseCommand = new Command(AddCourse);
        }

        private void UpdateView()
        {
            if (DataAccessLayer.CurrentSemester is null)
            {
                ShowMsg = true;
                ShowList = false;
                Title = "No profile";
            }
            else
            {
                CourseList = new ObservableCollection<Course>(DataAccessLayer.CurrentSemester.Courses);
                ShowMsg = false;
                ShowList = true;
                Title = DataAccessLayer.CurrentSemester.SemYearStr;
            }
            
        }

        public override void OnAppearing()
        {
            UpdateView();
            base.OnAppearing();
        }

        public ObservableCollection<Course> CourseList
        {
            get => courseList;
            set => SetProperty(ref courseList, value);
        }

        public int CourseIndex
        {
            get => courseIndex;
            set => SetProperty(ref courseIndex, value);
        }

        public bool ShowMsg
        {
            get => showMsg;
            set => SetProperty(ref showMsg, value);
        }

        public bool ShowList
        {
            get => showList;
            set => SetProperty(ref showList, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ICommand AddCourseCommand { get; }
        private async void AddCourse()
        {
            try
            {
                await OnNavigationForwardAsync(new NavigationPage(new CourseEditPage(null)));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
