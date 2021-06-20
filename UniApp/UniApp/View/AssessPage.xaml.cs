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
    public partial class AssessPage : BasePage
    {
        AssessViewModel vm;

        public AssessPage()
        {
            InitializeComponent();

            vm = new AssessViewModel();
            BindingContext = vm;
        }

        private void AssessItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}