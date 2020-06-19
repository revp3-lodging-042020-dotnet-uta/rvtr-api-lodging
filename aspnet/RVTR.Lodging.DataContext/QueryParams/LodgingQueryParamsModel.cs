namespace RVTR.Lodging.DataContext
{
  /// <summary>
  /// This class is used for storing URI request parameters
  /// Particularly search filters relevant to the Lodging DbSet
  /// </summary>
  public class LodgingQueryParamsModel : RoomQueryParamsModel
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

        /// <summary>
        /// In this city.
        /// </summary>
        public string City { get; set; }
    }
}
