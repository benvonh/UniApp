using UniApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UniApp.View
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            try
            {
                DataAccessLayer.Load();
            }
            catch (Exception ex)
            {
                DisplayAlert("Warning", ex.Message, "Close");
            }

            NavigationPage coursePage = new NavigationPage(new CoursePage()) { Title = "Courses" };
            NavigationPage assessPage = new NavigationPage(new AssessPage()) { Title = "Assessments" };

            Children.Add(coursePage);
            Children.Add(assessPage);
        }
        private async void OpenSemesterPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SemesterPage());
        }

        private async void OpenSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }
    }
}
