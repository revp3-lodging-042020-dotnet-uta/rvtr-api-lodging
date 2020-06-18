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

        private int _offset;

        /// <summary>
        /// Return results starting from this value.
        /// </summary>
        public int Offset
        {
            get { return _offset < 0 ? 0 : _offset; }
            set { _offset = value; }
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

        /// <summary>
        /// Key to use for sorting.
        /// </summary>
        private string _sortKey;
        public string SortKey
        {
            get { return _sortKey; }
            set { _sortKey = value; }
        }
    }
}
