using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.ViewModel
{
    /// <summary>
    /// Action sheet parameters.
    /// </summary>
    public class ActionSheetParameters
    {
        /// <summary>
        /// Title of the displayed action sheet. Must not be null.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Text to be displayed in the 'Cancel' button. Can be null to hide the cancel action.
        /// </summary>
        public string Cancel { get; set; }

        /// <summary>
        /// Text to be displayed in the 'Destruct' button. Can be null to hide the destructive option.
        /// </summary>
        public string Destruction { get; set; }

        /// <summary>
        /// Text labels for additional buttons. Must not be null.
        /// </summary>
        public string[] Buttons { get; set; }
    }
}
