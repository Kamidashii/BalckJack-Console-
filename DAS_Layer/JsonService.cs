using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BlackJack_DA.Interfaces;
using BlackJack_DA.Models;
using BlackJack_DA.Repositories;
using Common.Constants;
using Newtonsoft.Json;

namespace BlackJack_DA
{
    public class JsonService : IDataService
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
                using (StreamReader reader = new StreamReader(GameService_Constants.ResultsPath, Encoding.Default))
                {
                    string jSon = reader.ReadToEnd();

                    this.gameResultsRepository = new GameResultsRepository(JsonConvert.DeserializeObject<List<GameResult>>(jSon));
                }
            }
            catch (Exception exc)
            {
                Common.LogWriter.WriteLog(exc.Message);
            }
        }

        public void Save()
        {
            try
            {
                CreateResultsFolder();

                string jSon = JsonConvert.SerializeObject(this.gameResultsRepository.GetAll());
                using (StreamWriter writer = new StreamWriter(GameService_Constants.ResultsPath, false, Encoding.Default))
                {
                    writer.Write(jSon);
                }
            }
            catch (Exception exc)
            {
                Common.LogWriter.WriteLog(exc.Message);
            }
        }


        private void CreateResultsFolder()
        {
            if (!Directory.Exists(GameService_Constants.ResultsFolder))
            {
                Directory.CreateDirectory(GameService_Constants.ResultsFolder);
            }
        }

        private void CreateResultsFile()
        {
            if (!File.Exists(GameService_Constants.ResultsPath))
            {
                File.Create(GameService_Constants.ResultsPath);
            }
        }
    }
}
