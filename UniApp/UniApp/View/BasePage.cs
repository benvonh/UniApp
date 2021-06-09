using Scheduler.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.View
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetupBinding(BindingContext);
        }

        protected override void OnDisappearing()
        {
            TearDownBinding(BindingContext);
            base.OnDisappearing();
        }

        protected virtual void SetupBinding(object bindingContext)
        {
            if (bindingContext is BaseViewModel vm)
            {
                vm.DoNavigationForward += OnNavigationForward;
                vm.DoNavigationBack += OnNavigationBack;
                vm.DoDisplayAction += OnDisplayAction;
                vm.DoDisplayAlert += OnDisplayAlert;
                vm.DoBeginInvokeOnMainThread += OnBeginInvokeOnMainThread;
                vm.OnAppearing();
            }
        }

        protected virtual void TearDownBinding(object bindingContext)
        {
            if (bindingContext is BaseViewModel vm)
            {
                vm.OnDisappearing();
                vm.DoBeginInvokeOnMainThread -= OnBeginInvokeOnMainThread;
                vm.DoDisplayAlert -= OnDisplayAlert;
                vm.DoDisplayAction -= OnDisplayAction;
                vm.DoNavigationBack -= OnNavigationBack;
                vm.DoNavigationForward -= OnNavigationForward;
            }
        }

        public Task OnNavigationForward(Page p)
        {
            return Navigation.PushModalAsync(p);
        }

        public Task OnNavigationBack()
        {
            return Navigation.PopModalAsync();
        }

        public Task<string> OnDisplayAction(ActionSheetParameters p)
        {
            return DisplayActionSheet(p.Title, p.Cancel, p.Destruction, p.Buttons);
        }

        public Task OnDisplayAlert(DisplayAlertParameters p)
        {
            return DisplayAlert(p.Title, p.Message, p.Cancel);
        }

        public Task HandleException(Exception ex)
        {
            return OnDisplayAlert(new DisplayAlertParameters() { Title = "Error", Message = ex.Message, Cancel = "OK" });
        }

        public void OnBeginInvokeOnMainThread(Action action)
        {
            Device.BeginInvokeOnMainThread(action);
        }
    }
}