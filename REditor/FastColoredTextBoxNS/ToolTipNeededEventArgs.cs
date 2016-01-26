using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{

    /// <summary>
    /// ToolTipNeeded event args
    /// </summary>
    public class ToolTipNeededEventArgs : EventArgs
    {
        public ToolTipNeededEventArgs(Place place, string hoveredWord)
        {
            HoveredWord = hoveredWord;
            Place = place;
        }

        public string HoveredWord { get; private set; }

        public Place Place { get; private set; }

        public ToolTipIcon ToolTipIcon { get; set; }

        public string ToolTipText { get; set; }

        public string ToolTipTitle { get; set; }
    }

}
