using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherWPF
{
    /// <summary>
    /// Base class for view models.
    /// </summary>
    public class ViewModel : PropertyNotifier
    {
        /// <summary>
        /// Contains information shared between view models.
        /// </summary>
        protected ViewModelContext _context;

        /// <summary>
        /// Initialize the <see cref="ViewModelContext"/>
        /// </summary>
        /// <param name="context"></param>
        public ViewModel(ViewModelContext context)
        {
            _context = context;
        }
    }
}
