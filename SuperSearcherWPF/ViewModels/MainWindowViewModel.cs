using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherWPF.ViewModels
{
    /// <summary>
    /// View model for MainWindow.xaml.
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// View model for the search section.
        /// </summary>
        public SearchViewModel SearchViewModel { get; set; }
        /// <summary>
        /// View model for the statistics section.
        /// </summary>
        public StatisticsViewModel StatisticsViewModel { get; set; }

        /// <summary>
        /// Calls the base constructor to initialize the view model context.
        /// </summary>
        /// <param name="context">Contains information shared between view models.</param>
        public MainWindowViewModel(ViewModelContext context) : base(context)
        {
            SearchViewModel = new(context);
            StatisticsViewModel = new(context);
        }
    }
}
