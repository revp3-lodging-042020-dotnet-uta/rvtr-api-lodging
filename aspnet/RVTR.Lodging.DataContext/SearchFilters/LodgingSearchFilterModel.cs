namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class LodgingSearchFilterModel
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

        private int _ratingAtLeast;
        /// <summary>
        /// Minimum star rating.
        /// </summary>
        public int RatingAtLeast
        {
            get {
                if (_ratingAtLeast < 1) return 1;
                if (_ratingAtLeast > 5) return 5;
                return _ratingAtLeast;
            }
            set { _ratingAtLeast = value; }
        }

        private int _searchRadius;
        /// <summary>
        /// Search radius (in miles).
        /// </summary>
        public int SearchRadius
        {
            get { return _searchRadius < 0 ? 1 : _searchRadius; }
            set { _searchRadius = value; }
        }
        
        private int _bedsAtLeast;
        /// <summary>
        /// Minimum number of beds in a lodging.
        /// </summary>
        public int BedsAtLeast
        {
            get { return _bedsAtLeast < 0 ? 0 : _bedsAtLeast; }
            set { _bedsAtLeast = value; }
        }

        private int _bathsAtLeast;
        /// <summary>
        /// Minimum number of baths in a lodging.
        /// </summary>
        public int BathsAtLeast
        {
            get { return _bathsAtLeast < 0 ? 0 : _bathsAtLeast; }
            set { _bathsAtLeast = value; }
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
            get { return _sortOrder == "desc" ? "desc" : "asc"; }
            set { _sortOrder = value; }
        }
    }
}
