using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Wpf.Ui.Controls;

namespace MetBench_Client.Helpers
{
    [Serializable]
    public class MyCustomContentState : CustomContentState
    {
        string dateCreated;
        string PageTitle;

        public MyCustomContentState(string dateCreated, string PageTitle)
        {
            this.dateCreated = dateCreated;
            this.PageTitle = PageTitle;
        }

        public override string JournalEntryName
        {
            get
            {
                return "Journal Entry " + this.dateCreated;
            }
        }

        public override void Replay(NavigationService navigationService, NavigationMode mode)
        {
            this.PageTitle += this.dateCreated;
        }
    }
}
