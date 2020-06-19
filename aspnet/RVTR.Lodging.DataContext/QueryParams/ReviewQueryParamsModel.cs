namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// Particularly search filters relevant to the Reviews DbSet
    /// </summary>
    public class ReviewQueryParamsModel : QueryParamsModel
    {
        private double _ratingAtLeast;
        /// <summary>
        /// Minimum rating (out of 10).
        /// </summary>
        public double RatingAtLeast
        {
            get {
                if (_ratingAtLeast < 0) return 0;
                if (_ratingAtLeast > 10) return 10;
                return _ratingAtLeast;
            }
            set { _ratingAtLeast = value; }
        }

        /// <summary>
        /// Get reviews for this Lodging ID.
        /// </summary>
        public int? LodgingId { get; set; }

        /// <summary>
        /// Get reviews for this Account ID.
        /// </summary>
        /// <value></value>
        public int? AccountId { get; set; }
    }
}
