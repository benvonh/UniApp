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
    public partial class CoursePage : BasePage
    {
        CourseViewModel vm;

        public CoursePage()
        {
            InitializeComponent();
            vm = new CourseViewModel();
            BindingContext = vm;
        }

        private void CourseItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}