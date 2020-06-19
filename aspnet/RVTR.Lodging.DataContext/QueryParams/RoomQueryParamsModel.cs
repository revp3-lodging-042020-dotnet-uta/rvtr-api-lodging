namespace RVTR.Lodging.DataContext
{
  /// <summary>
  /// Base query parameter class for entities that support rooms.
  /// </summary>
  public class RoomQueryParamsModel : QueryParamsModel
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
        public string HasBedType { get; set; }

        /// <summary>
        /// Offers this amenity.
        /// </summary>
        public string HasAmenity { get; set; }
    }
}
