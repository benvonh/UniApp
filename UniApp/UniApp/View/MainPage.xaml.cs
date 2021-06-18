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

            Title = "TEMP";

            NavigationPage coursePage = new NavigationPage(new CoursePage());
            NavigationPage assessPage = new NavigationPage(new AssessPage());

            Children.Add(coursePage);
            Children.Add(assessPage);
        }
    }
}
