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
            if (ProfileNames.Count == 0)
                ProfileNames.Add(emptyProfile);
            SemNum = DateTime.Now.Month < 6 ? "1" : "2";
            YearNum = DateTime.Now.Year.ToString();
            AddSemCommand = new Command(AddSem);
            DelSemCommand = new Command(DelSem);
        }

        private void UpdateView()
        {
            //ProfileNames = null;
            //ProfileNames = new ObservableCollection<string>(DataAccessLayer.LoadAllProfile());
            ProfileNames.Clear();
            DataAccessLayer.LoadAllProfile().ForEach(profile => ProfileNames.Add(profile));
        }

        public ObservableCollection<string> ProfileNames
        {
            get => profileNames;
            set
            {
                SetProperty(ref profileNames, value);
                if (ProfileNames.Count == 0)
                    ProfileNames.Add(emptyProfile);
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
    }
}
