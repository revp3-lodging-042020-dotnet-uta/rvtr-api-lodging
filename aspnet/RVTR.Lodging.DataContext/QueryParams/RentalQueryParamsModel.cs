namespace RVTR.Lodging.DataContext
{
  /// <summary>
  /// This class is used for storing URI request parameters
  /// Particularly search filters relevant to the Rentals DbSet
  /// </summary>
  public class RentalQueryParamsModel : QueryParamsModel
    {
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
