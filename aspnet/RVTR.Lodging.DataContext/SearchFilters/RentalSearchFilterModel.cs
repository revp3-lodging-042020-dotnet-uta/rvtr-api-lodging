namespace RVTR.Lodging.DataContext
{
    /// <summary>
    /// This class is used for storing URI request parameters
    /// </summary>
    public class RentalSearchFilterModel : SearchFilter
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
    }
}
