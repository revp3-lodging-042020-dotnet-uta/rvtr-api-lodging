namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class SearchFilterModel
    {
        private int? _limit;

        /// <summary>
        /// Max number of results to be returned.
        /// </summary>
        public int? Limit
        {
            get { return _limit; }
            set {
              if (value == null) return;
              _limit = (int)value < 0 ? 1 : value;
            }
        }

        private int? _paginate;

        /// <summary>
        /// Return results starting from this value.
        /// </summary>
        public int? Paginate
        {
            get { return _paginate; }
            set {
              if (value == null) return;
              _paginate = (int)value < 0 ? 0 : value;
            }
        }

        /// <summary>
        /// Key to use for sorting.
        /// </summary>
        public string SortKey { get; set; }

        private string _sortOrder;
        /// <summary>
        /// The order to sort results, must be "asc" or "desc", defaulting to "asc".
        /// </summary>
        public string SortOrder
        {
            get { return _sortOrder; }
            set
            {
                _sortOrder = value == "desc" ? "desc" : "asc";
            }
        }
    }
}
