using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConsole
{
    public class WeatherApp
    {
        /// <summary>
        /// http client static instance used to call weather api
        /// </summary>
        static HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(Constant.API_BASE_URI) };

        /// <summary>
        /// Fetches weather data for a city id
        /// </summary>
        /// <param name="id">city id</param>
        /// <param name="info"> Json object to hold fetched data</param>
        /// <returns>void</returns>
        static private async Task GetWeather(string id, JObject info)
        {
            HttpResponseMessage response = httpClient.GetAsync(string.Format("?id={0}&appid={1}", id, Constant.API_ACCESS_CODE)).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync();
                Console.WriteLine(dataObjects);
                JObject result = JObject.Parse(dataObjects);
                info.Add("weather_info", result);
            }
        }

        /// <summary>
        /// Process input file and generate output file with weather data for city ids present in input file
        /// </summary>
        /// <param name="args">If empty assumes input file name as "input.txt"</param>
        /// <returns> Status of process</returns>
        public string Process(string[] args)
        {
            string return_message = "File Processed";
            try
            {
                string date_today = DateTime.Now.ToString("ddMMyyyy");

                /*
                 * Check if input file is already processed (has today's datestamp)
                 */
                if (File.Exists(Constant.INPUT_FOLDER_PATH + "\\" + date_today + "_" + Constant.INPUT_FILE_NAME) && args.Length == 0)
                {
                    throw new Exception("File already processed");
                }

                int counter = 0;
                string line;
                JArray city_json = new JArray();

                //httpClient.BaseAddress = new Uri(Constant.API_BASE_URI);

                // Read the file and fetch city info line by line.  
                StreamReader file =
                    new StreamReader(Constant.INPUT_FOLDER_PATH + "\\" + (args.Length == 1 ? args[0] : Constant.INPUT_FILE_NAME));
                while ((line = file.ReadLine()) != null)
                {
                    // Ignore lines that are blank or do not follow file format
                    if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                    {
                        if (System.Diagnostics.Debugger.IsAttached)
                            Console.WriteLine(line);

                        counter++;
                        JObject city_obj = new JObject
                        {
                            { "id", line.Split('=')[0] },
                            { "city_name", line.Split('=')[1] }
                        };
                        city_json.Add(city_obj);
                        GetWeather(line.Split('=')[0], city_obj);
                    }
                }

                file.Close();
                File.WriteAllText(Constant.OUTPUT_FOLDER_PATH + "\\" + date_today + "_" + Constant.OUTPUT_FILE_BASE_NAME + ".json", city_json.ToString());

                if (args.Length == 0)
                    File.Move(Constant.INPUT_FOLDER_PATH + "\\" + Constant.INPUT_FILE_NAME, Constant.INPUT_FOLDER_PATH + "\\" + date_today + "_" + Constant.INPUT_FILE_NAME);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return_message = "File not found";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return_message = ex.Message;
            }

            return return_message;

            //// Suspend the screen.  
            //if (System.Diagnostics.Debugger.IsAttached)
            //    Console.ReadLine();

        }
    }
}
