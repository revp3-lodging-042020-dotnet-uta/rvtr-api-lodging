namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class SearchFilter
    {
        private int _limit;

        /// <summary>
        /// Max number of results to be returned.
        /// </summary>
        public int Limit
        {
            get { return _limit <= 0 ? 50 : _limit; }
            set { _limit = value; }
        }

        private int _paginate;

        /// <summary>
        /// Return results starting from this value.
        /// </summary>
        public int Paginate
        {
            get { return _paginate < 0 ? 0 : _paginate; }
            set { _paginate = value; }
        }

        private string _sortOrder;
        /// <summary>
        /// The order to sort results, must be "asc" or "desc", defaulting to "asc".
        /// </summary>
        public string SortOrder
        {
            get { return _sortOrder == "desc" ? "desc" : "asc"; }
            set { _sortOrder = value; }
        }
    }
}
