using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;
using UniApp.Model;
using UniApp.View;
using Xamarin.Forms;

namespace UniApp.ViewModel
{
    public class AssessViewModel : BaseViewModel
    {
        private ObservableCollection<Assessment> assessList;
        private bool showMsg;
        private bool showList;
        private string title;
        private int? _AssessIndex;
        private Timer _Timer;

        public AssessViewModel()
        {
            UpdateView();
            AddAssessCommand = new Command(AddAssess);
            _Timer = new Timer(DoubleTapFalse, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void UpdateView()
        {
            if (DataAccessLayer.CurrentCourseIndex is null || DataAccessLayer.CurrentCourseIndex >= DataAccessLayer.CurrentSemester.Courses.Count)
            {
                ShowMsg = true;
                ShowList = false;
                Title = "Select course";
            }
            else
            {
                AssessList = new ObservableCollection<Assessment>(DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Assessments);
                ShowMsg = false;
                ShowList = true;
                Title = DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Code;
            }
        }

        public override void OnAppearing()
        {
            UpdateView();
            base.OnAppearing();
        }

        public ObservableCollection<Assessment> AssessList
        {
            get => assessList;
            set => SetProperty(ref assessList, value);
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

        public ICommand AddAssessCommand { get; }
        private async void AddAssess()
        {
            try
            {
                await OnNavigationForwardAsync(new NavigationPage(new AssessEditPage()));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (_AssessIndex is null || _AssessIndex != e.ItemIndex)
                {
                    _AssessIndex = e.ItemIndex;
                    _Timer.Change(500, Timeout.Infinite);
                }
                else
                {
                    await OnNavigationForwardAsync(new NavigationPage(new AssessEditPage(e.ItemIndex)));
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void DoubleTapFalse(object state)
        {
            _Timer.Change(Timeout.Infinite, Timeout.Infinite);
            _AssessIndex = null;
        }
    }
}
