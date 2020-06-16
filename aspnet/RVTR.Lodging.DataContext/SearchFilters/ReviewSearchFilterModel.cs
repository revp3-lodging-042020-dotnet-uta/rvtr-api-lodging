namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class ReviewSearchFilterModel : SearchFilter
    {
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

        private int? _lodgingId;
        /// <summary>
        /// Get reviews for this Lodging ID.
        /// </summary>
        public int? LodgingId
        {
            get { return _lodgingId; }
            set { _lodgingId = value; }
        }

        private int? _accountId;
        /// <summary>
        /// Get reviews for this Account ID.
        /// </summary>
        /// <value></value>
        public int? AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        

        /// <summary>
        /// Key to use for sorting.
        /// </summary>
        public string SortKey { get; set; }
    }
}
