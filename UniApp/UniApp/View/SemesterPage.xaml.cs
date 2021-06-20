using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemesterPage : BasePage
    {
        SemesterViewModel vm;

        public SemesterPage()
        {
            InitializeComponent();

            vm = new SemesterViewModel();
            BindingContext = vm;
        }
    }
}