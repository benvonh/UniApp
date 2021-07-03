using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniApp.Model;
using UniApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessEditPage : BasePage
    {
        public AssessEditPage(int? assessIndex = null)
        {
            InitializeComponent();
            BindingContext = new AssessEditViewModel(assessIndex);
        }
    }
}