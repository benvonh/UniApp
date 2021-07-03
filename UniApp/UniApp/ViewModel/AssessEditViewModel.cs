using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UniApp.Model;
using Xamarin.Forms;

namespace UniApp.ViewModel
{
    public class AssessEditViewModel : BaseViewModel
    {
        private string name;
        private string weight;
        private string mark;
        private string hurdle;
        private bool showBtn;
        private int _AssessIndex;

        public AssessEditViewModel(int? assessIndex)
        {
            DelAssessCommand = new Command(DelAssess);
            SaveAssessCommand = new Command(SaveAssess);
            BackCommand = new Command(Back);

            if (assessIndex != null)
            {
                _AssessIndex = assessIndex.Value;
                Assessment assessment = DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Assessments[_AssessIndex];
                Name = assessment.Name.ToString();
                Weight = assessment.Weight.ToString();
                Mark = (assessment.Mark ?? 0).ToString();
                Hurdle = assessment.Hurdle.ToString();
                ShowBtn = true;
            }
            else
            {
                Weight = "0";
                Mark = "0";
                Hurdle = "0";
                ShowBtn = false;
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }

        public string Mark
        {
            get => mark;
            set => SetProperty(ref mark, value);
        }

        public string Hurdle
        {
            get => hurdle;
            set => SetProperty(ref hurdle, value);
        }

        public bool ShowBtn
        {
            get => showBtn;
            set => SetProperty(ref showBtn, value);
        }

        public ICommand DelAssessCommand { get; }
        private async void DelAssess()
        {
            try
            {
                if (await DisplayYesNo("Deleting assessment", "Are you sure?"))
                {
                    DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Assessments.RemoveAt(_AssessIndex);
                    DataAccessLayer.Save();
                    await OnNavigationBackAsync();
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public ICommand SaveAssessCommand { get; }
        private async void SaveAssess()
        {
            try
            {
                int? mark;
                if (Mark == "0")
                    mark = null;
                else
                    mark = Convert.ToInt32(Mark);
                // BECAUSE I'M USING AN OLD-VERSION OF C# >:(

                if (ShowBtn)
                {
                    Assessment assessment = DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Assessments[_AssessIndex];
                    assessment.Name = Name;
                    assessment.Weight = Convert.ToInt32(Weight);
                    assessment.Mark = mark;
                    assessment.Hurdle = Convert.ToInt32(Hurdle);
                }
                else
                    DataAccessLayer.CurrentSemester
                        .Courses[DataAccessLayer.CurrentCourseIndex.Value]
                        .AddAssessment(Name, Convert.ToInt32(Weight), Convert.ToInt32(Hurdle));

                DataAccessLayer.Save();
                await OnNavigationBackAsync();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public ICommand BackCommand { get; }
        private async void Back()
        {
            await OnNavigationBackAsync();
        }
    }
}
