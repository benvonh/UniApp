using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace UniApp.ViewModel
{
    public class SemesterViewModel : BaseViewModel
    {
        const string emptyProfile = "<No Semester Profiles>";

        private ObservableCollection<string> profileNames;
        private int selectedProfile;
        private string semNum;
        private string yearNum;

        public SemesterViewModel()
        {
            UpdateView();
            AddSemCommand = new Command(AddSem);
            DelSemCommand = new Command(DelSem);
            LoadSemCommand = new Command(LoadSem);
        }

        private void UpdateView()
        {
            ProfileNames = new ObservableCollection<string>(DataAccessLayer.LoadAllProfile());
            SemNum = DateTime.Now.Month < 6 ? "1" : "2";
            YearNum = DateTime.Now.Year.ToString();
        }

        public ObservableCollection<string> ProfileNames
        {
            get => profileNames;
            set
            {
                if (value.Count == 0)
                    value.Add(emptyProfile);
                SetProperty(ref profileNames, value);
            }
        }

        public int SelectedProfile
        {
            get => selectedProfile;
            set => SetProperty(ref selectedProfile, value);
        }

        public string SemNum
        {
            get => semNum;
            set => SetProperty(ref semNum, value);
        }

        public string YearNum
        {
            get => yearNum;
            set => SetProperty(ref yearNum, value);
        }

        public ICommand AddSemCommand { get; }
        private async void AddSem()
        {
            try
            {
                DataAccessLayer.SaveNew(Convert.ToInt32(semNum), Convert.ToInt32(yearNum));
                UpdateView();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public ICommand DelSemCommand { get; }
        private async void DelSem()
        {
            try
            {
                if (SelectedProfile < 0)
                    throw new Exception("Please select a profile");

                DataAccessLayer.DeleteProfile(ProfileNames[SelectedProfile]);
                UpdateView();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public ICommand LoadSemCommand { get; }
        private async void LoadSem()
        {
            try
            {
                if (SelectedProfile < 0)
                    throw new Exception("Please select a profile");

                DataAccessLayer.Load(ProfileNames[SelectedProfile]);
                await OnNavigationBackAsync();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
