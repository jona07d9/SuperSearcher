using SuperSearcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperSearcherWPF.ViewModels
{
    /// <summary>
    /// View model for ISearchResult
    /// </summary>
    public class SearchResultViewModel : ViewModel
    {
        /// <summary>
        /// The search result this view model is for.
        /// </summary>
        private readonly ISearchResult _searchResult;
        /// <summary>
        /// Command that opens the search result with it's default program.
        /// </summary>
        private ICommand _openCommand;

        /// <summary>
        /// Command that opens the search result with it's default program.
        /// </summary>
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand(
                        parameter =>
                        {
                            _searchResult.Open();
                        },

                        parameter =>
                        {
                            return true;
                        }
                    );
                }

                return _openCommand;
            }
        }
        /// <summary>
        /// The name of the search result.
        /// </summary>
        public string Name => _searchResult.Name;

        /// <summary>
        /// Call base constructor
        /// </summary>
        /// <param name="context">Information shared between view models.</param>
        public SearchResultViewModel(ViewModelContext context, ISearchResult searchResult)
            : base(context)
        {
            _searchResult = searchResult;
        }
    }
}
