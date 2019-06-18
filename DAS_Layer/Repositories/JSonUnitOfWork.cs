using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Layer.Interfaces;
using DA_Layer.Models;
using HelpfulValues.Constants;
using Newtonsoft.Json;

namespace DA_Layer.Repositories
{
    public class JSonUnitOfWork : IUnitOfWork
    {
        private GameResultsRepository gameResultsRepository;

        public IRepository<GameResult> GameResultsRepository
        {
            get
            {
                if (this.gameResultsRepository == null)
                {

                    CreateResultsFolder();
                    CreateResultsFile();

                    LoadGameResults();
                }

                return this.gameResultsRepository;
            }
        }

        private void LoadGameResults()
        {
            try
            {
                using (StreamReader reader = new StreamReader(GameService_Constants.RESULTS_PATH, Encoding.Default))
                {
                    string jSon = reader.ReadToEnd();

                    this.gameResultsRepository = new GameResultsRepository(JsonConvert.DeserializeObject<List<GameResult>>(jSon));
                }
            }
            catch (Exception exc)
            {
                HelpfulValues.LogWriter.WriteLog(exc.Message);
            }
        }

        public void Save()
        {
            try
            {
                CreateResultsFolder();

                string jSon = JsonConvert.SerializeObject(this.gameResultsRepository.GetAll());
                using (StreamWriter writer = new StreamWriter(GameService_Constants.RESULTS_PATH, false, Encoding.Default))
                {
                    writer.Write(jSon);
                }
            }
            catch (Exception exc)
            {
                HelpfulValues.LogWriter.WriteLog(exc.Message);
            }
        }


        private void CreateResultsFolder()
        {
            if (!Directory.Exists(GameService_Constants.RESULTS_FOLDER))
            {
                Directory.CreateDirectory(GameService_Constants.RESULTS_FOLDER);
            }
        }

        private void CreateResultsFile()
        {
            if (!File.Exists(GameService_Constants.RESULTS_PATH))
            {
                File.Create(GameService_Constants.RESULTS_PATH);
            }
        }
    }
}
