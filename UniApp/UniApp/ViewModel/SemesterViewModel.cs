using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace UniApp.ViewModel
{
    public class SemesterViewModel : BaseViewModel
    {
        private List<string> profileNames;
        private int selectedProfile;
        private string semNum;
        private string yearNum;

        public SemesterViewModel()
        {
            ProfileNames = DataAccessLayer.LoadAllProfile();
            if (ProfileNames.Count == 0)
                ProfileNames.Add("<No Semester Profiles>");
            SemNum = "1";
            YearNum = DateTime.Now.Year.ToString();
            AddSemCommand = new Command(AddSem);
            DelSemCommand = new Command(DelSem);
        }

        private void UpdateView()
        {
            ProfileNames = DataAccessLayer.LoadAllProfile();
        }

        public List<string> ProfileNames
        {
            get => profileNames;
            set => SetProperty(ref profileNames, value);
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

                SemNum = name[9].ToString();
                YearNum = name.Substring(11);
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
                await HandleError(ex.Message);
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
                await HandleError(ex.Message);
            }
        }
    }
}
