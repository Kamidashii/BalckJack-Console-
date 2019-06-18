using BlackJack.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;

namespace BlackJack.Managers
{
    public class ResultsService
    {
        public void SaveResults(List<GameResult>gameResults)
        {
            try
            {
                CreateResultsFolder();

                string jSon = JsonConvert.SerializeObject(gameResults);
                using (StreamWriter writer = new StreamWriter(Constants.GameController_Constants.RESULTS_PATH, false, Encoding.Default))
                {
                    writer.Write(jSon);
                }
            }
            catch (Exception exc)
            {
                MainView.ShowExceptionMessage(exc.Message);
            }
        }

        public void CreateResultsFolder()
        {
            if (!Directory.Exists(Constants.GameController_Constants.RESULTS_FOLDER))
            {
                Directory.CreateDirectory(Constants.GameController_Constants.RESULTS_FOLDER);
            }
        }

        public void CreateResultsFile()
        {
            if (!File.Exists(Constants.GameController_Constants.RESULTS_PATH))
            {
                File.Create(Constants.GameController_Constants.RESULTS_PATH);
            }
        }

        public List<GameResult> LoadResults()
        {
            try
            {
                CreateResultsFolder();
                CreateResultsFile();

                using (StreamReader reader = new StreamReader(Constants.GameController_Constants.RESULTS_PATH, Encoding.Default))
                {
                    string jSon = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<List<GameResult>>(jSon);
                }
            }
            catch (Exception exc)
            {
                MainView.ShowExceptionMessage(exc.Message);
                return null;
            }
        }
    }
}
