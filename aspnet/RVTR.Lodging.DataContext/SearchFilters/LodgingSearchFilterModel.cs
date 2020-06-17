namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class LodgingSearchFilterModel : SearchFilter
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
    }
}
