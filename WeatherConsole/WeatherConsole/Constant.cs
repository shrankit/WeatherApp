using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherConsole
{
    public class Constant
    {
        /// <summary>
        /// Weather api uri
        /// </summary>
        internal static string API_BASE_URI = "https://api.openweathermap.org/data/2.5/weather";
        
        /// <summary>
        /// Access code for calling api uri
        /// </summary>
        internal static string API_ACCESS_CODE = "aa69195559bd4f88d79f9aadeb77a8f6";

        /// <summary>
        /// Path of input folder
        /// </summary>
        internal static string INPUT_FOLDER_PATH = "C:\\Weather\\Input";

        /// <summary>
        /// Default name for the input file
        /// </summary>
        internal static string INPUT_FILE_NAME = "input.txt";

        /// <summary>
        /// Path of output folder
        /// </summary>
        internal static string OUTPUT_FOLDER_PATH = "C:\\Weather\\Output";

        /// <summary>
        /// suffix of output file
        /// </summary>
        internal static string OUTPUT_FILE_BASE_NAME = "weather_info";
    }
}
