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
            ProfileNames = new ObservableCollection<string>(DataAccessLayer.LoadAllProfile());
            SemNum = DateTime.Now.Month < 6 ? "1" : "2";
            YearNum = DateTime.Now.Year.ToString();
            AddSemCommand = new Command(AddSem);
            DelSemCommand = new Command(DelSem);
            LoadSemCommand = new Command(LoadSem);
        }

        private void UpdateView()
        {
            //ProfileNames = null;
            ProfileNames = new ObservableCollection<string>(DataAccessLayer.LoadAllProfile());
            //ProfileNames.Clear();
            //DataAccessLayer.LoadAllProfile().ForEach(profile => ProfileNames.Add(profile));

            SemNum = ProfileNames[SelectedProfile][DataAccessLayer.ProfileSemIndex].ToString();
            YearNum = ProfileNames[SelectedProfile].Substring(DataAccessLayer.ProfileYearIndex);
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
            set
            {
                SetProperty(ref selectedProfile, value);
                string name = ProfileNames[value];

                if (name[0].Equals('<'))
                    return;
            }
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
                await HandleError(ex.StackTrace);
            }
        }

        public ICommand DelSemCommand { get; }
        private async void DelSem()
        {
            try
            {
                DataAccessLayer.DeleteProfile(ProfileNames[SelectedProfile]);
                UpdateView();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
                await HandleError(ex.StackTrace);
            }
        }

        public ICommand LoadSemCommand { get; }
        private async void LoadSem()
        {
            try
            {
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
