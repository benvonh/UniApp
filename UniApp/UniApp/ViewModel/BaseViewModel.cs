using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        protected bool IsBusyValue;

        public bool IsBusy
        {
            get => IsBusyValue;
            set => SetProperty(ref IsBusyValue, value, onChanged: () => OnPropertyChanged(nameof(IsNotBusy)));
        }

        public bool IsNotBusy => !IsBusy;

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        internal event Func<Page, Task> DoNavigationForward;

        public Task OnNavigationForwardAsync(Page p)
        {
            try
            {
                return DoNavigationForward?.Invoke(p) ?? Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        internal event Func<Task> DoNavigationBack;

        public Task OnNavigationBackAsync()
        {
            try
            {
                return DoNavigationBack?.Invoke() ?? Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        internal event Func<ActionSheetParameters, Task<string>> DoDisplayAction;

        public Task<string> DisplayActionAsync(ActionSheetParameters p)
        {
            try
            {
                return DoDisplayAction?.Invoke(p) ?? Task.FromResult<string>(p.Cancel);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return Task.FromResult<string>(p.Cancel);
            }
        }

        /// <summary>
        /// Displays a native platform action sheet, allowing the application user to choose from several buttons.
        /// </summary>
        /// <param name="title">Title of the displayed action sheet. Must not be null.</param>
        /// <param name="cancel">Text to be displayed in the 'Cancel' button. Can be null to hide the cancel action.</param>
        /// <param name="destruction">Text to be displayed in the 'Destruct' button. Can be null to hide the destructive option.</param>
        /// <param name="buttons">Text labels for additional buttons. Must not be null.</param>
        /// <returns>An awaitable Task that displays an action sheet and returns the Text of the button pressed by the user.</returns>
        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return DisplayActionAsync(new ActionSheetParameters() { Title = title, Cancel = cancel, Destruction = destruction, Buttons = buttons });
        }

        internal event Func<DisplayAlertParameters, Task> DoDisplayAlert;

        public Task DisplayAlertAsync(DisplayAlertParameters p)
        {
            try
            {
                return DoDisplayAlert?.Invoke(p) ?? Task.CompletedTask;
            }
            catch (Exception)// ex
            {
                //Error message?
                return Task.CompletedTask;
            }
        }

        public Task DisplayMessage(string title, string msg, string cancel = "OK")
        {
            return DisplayAlertAsync(new DisplayAlertParameters() { Title = title, Message = msg, Cancel = cancel });
        }

        public Task HandleError(string msg)
        {
            return DisplayAlertAsync(new DisplayAlertParameters() { Title = "Error", Message = msg, Cancel = "OK" });
        }

        public Task HandleException(Exception ex)
        {
            return DisplayAlertAsync(new DisplayAlertParameters() { Title = "Error", Message = ex.Message, Cancel = "OK" });
        }

        internal event Action<Action> DoBeginInvokeOnMainThread;

        public void BeginInvokeOnMainThread(Action action)
        {
            try
            {
                DoBeginInvokeOnMainThread?.Invoke(action);
            }
            catch (Exception)// ex
            {
                //Error message?
            }
        }
    }
}
