namespace RVTR.Lodging.DataContext
{
  /// <summary>
  /// This class is used for storing URI request parameters
  /// Particularly search filters relevant to the Lodging DbSet
  /// </summary>
  public class LodgingQueryParamsModel : QueryParamsModel
    {
        /// <summary>
        /// Whether or not to include image URLs with the response.
        /// </summary>
        public bool IncludeImages { get; set; }
        
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
        public string HasBedType { get; set; }

        /// <summary>
        /// Offers this amenity.
        /// </summary>
        public string HasAmenity { get; set; }

        /// <summary>
        /// In this city.
        /// </summary>
        public string City { get; set; }
    }
}
