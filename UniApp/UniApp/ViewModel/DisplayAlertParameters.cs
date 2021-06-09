using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.ViewModel
{
    /// <summary>
    /// Presents an alert dialog to the application user with a single cancel button.
    /// </summary>
    public class DisplayAlertParameters
    {
        /// <summary>
        /// The title of the alert dialog.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The body text of the alert dialog.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Text to be displayed on the 'Cancel' button.
        /// </summary>
        public string Cancel { get; set; }
    }
}
