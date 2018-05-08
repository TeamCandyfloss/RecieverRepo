using System.Data;
using Microsoft.SqlServer.Server;

namespace WeatherRest.model
{
    /// <summary>
    /// The Temperature model class is used because of the database requiring it for using the HTTP requests.
    /// This means we'd not be able to call for temperatures from the database unless we specify the model class.
    /// </summary>
    public class MTemperature
    {
        /// <summary>
        /// Instance fields for the class
        /// Temperature = Measured temperature from the raspberry Pi
        /// Time = The time the temperature was measured
        /// Place = The location from where the measurement took place.
        /// </summary>
        /// 
        private int _id;
        private int _temperature;
        private string _time;
        private string _place;

        public MTemperature()
        {

        }

        /// <summary>
        /// Constructor that takes our parameters and puts them into a
        /// new instance of the object when it is instantiated.
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="time"></param>
        /// <param name="place"></param>
        public MTemperature(int temperature, string time, string place)
        {
            _temperature = temperature;
            _time = time;
            _place = place;
        }

        public MTemperature(int temperature, string time, string place, int id)
        {
            _temperature = temperature;
            _time = time;
            _place = place;
            _id = id;
        }

        /// <summary>
        /// Properties for the class to make our instancefields useable.
        /// Temperature = Measured temperature from the raspberry Pi
        /// Time = The time the temperature was measured
        /// Place = The location from where the measurement took place.
        /// </summary>
        public int ID
        {
            get => _id;
            set => _id = value;
        }
        public int Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }
        public string Time
        {
            get => _time;
            set => _time = value;
        }
        public string Place
        {
            get => _place;
            set => _place = value;
        }
    }
}