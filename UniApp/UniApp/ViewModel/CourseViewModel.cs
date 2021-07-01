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
                await OnNavigationForwardAsync(new NavigationPage(new CourseEditPage()));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (DataAccessLayer.CurrentCourseIndex is null || DataAccessLayer.CurrentCourseIndex.Value != e.ItemIndex)
            {
                DataAccessLayer.CurrentCourseIndex = e.ItemIndex;
            }
            else
            {
                await OnNavigationForwardAsync(new NavigationPage(new CourseEditPage(DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value])));
            }                
        }
    }
}
