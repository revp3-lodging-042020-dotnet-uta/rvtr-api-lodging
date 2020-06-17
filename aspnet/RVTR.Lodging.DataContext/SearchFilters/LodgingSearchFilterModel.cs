namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class LodgingSearchFilterModel : SearchFilter
    {
        private double _ratingAtLeast;
        /// <summary>
        /// Minimum star rating.
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

        private int _bedRoomsAtLeast;
        /// <summary>
        /// Minimum number of bedrooms in a lodging.
        /// </summary>
        public int BedRoomsAtLeast
        {
            get { return _bedRoomsAtLeast < 0 ? 0 : _bedRoomsAtLeast; }
            set { _bedRoomsAtLeast = value; }
        }

        /// <summary>
        /// Offers this type of bed.
        /// </summary>
        private string _hasBedType;
        public string HasBedType
        {
            get { return _hasBedType; }
            set { _hasBedType = value; }
        }

        /// <summary>
        /// Offers this amenity.
        /// </summary>
        private string _hasAmenity;
        public string HasAmenity
        {
            get { return _hasAmenity; }
            set { _hasAmenity = value; }
        }
    }
}
