using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Layer.Interfaces;
using DA_Layer.Models;
using DA_Layer.Repositories;
using HelpfulValues.Constants;
using Newtonsoft.Json;

namespace DA_Layer
{
    public class JSonUnitOfWork : IUnitOfWork
    {
        private GameResultsRepository gameResultsRepository;
        private ProfilesRepository profilesRepository;

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

        public IRepository<Profile> ProfilesRepository
        {
            get
            {
                if(this.profilesRepository==null)
                {
                    this.profilesRepository = new ProfilesRepository(new List<Profile> { new Profile("login1", "password1", new User("Vasya", 200)) });
                }
                return this.profilesRepository;
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
