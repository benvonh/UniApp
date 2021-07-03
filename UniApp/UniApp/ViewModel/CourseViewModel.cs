using System;
using System.Collections.ObjectModel;
using System.Threading;
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
        private int? _CourseIndex;
        private Timer _Timer;

        public CourseViewModel()
        {
            UpdateView();
            AddCourseCommand = new Command(AddCourse);
            _Timer = new Timer(DoubleTapFalse, null, Timeout.Infinite, Timeout.Infinite);
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
                _CourseIndex = null;
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

        public async void ItemTapped(ItemTappedEventArgs e)
        {
            try
            {
                if (_CourseIndex is null || _CourseIndex != e.ItemIndex)
                {
                    DataAccessLayer.CurrentCourseIndex = e.ItemIndex;
                    _CourseIndex = e.ItemIndex;
                    _Timer.Change(500, Timeout.Infinite);
                }
                else
                {
                    await OnNavigationForwardAsync(new NavigationPage(new CourseEditPage((Course)e.Item)));
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void DoubleTapFalse(object state)
        {
            _CourseIndex = null;
        }
    }
}
