using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BlackJackDataAccess.Interfaces;
using BlackJackDataAccess.Interfaces.Repositories;
using BlackJackDataAccess.Interfaces.Services;
using BlackJackDataAccess.Models;
using BlackJackDataAccess.Repositories;
using Common.Constants;
using Newtonsoft.Json;

namespace BlackJackDataAccess.Services
{
    public class JsonService : IJsonService
    {
        private GameResultsRepository _gameResultsRepository;
        private ProfilesRepository _profilesRepository;

        public IRepository<GameResult> GameResultsRepository
        {
            get
            {
                if (_gameResultsRepository == null)
                {

                    CreateResultsFolder();
                    CreateResultsFile();

                    LoadGameResults();
                }

                return _gameResultsRepository;
            }
        }

        public IRepository<Profile> ProfilesRepository
        {
            get
            {
                if(_profilesRepository==null)
                {
                    _profilesRepository = new ProfilesRepository(new List<Profile> {
                        new Profile("login1", "password1", new User("Vasya", 200)),
                        new Profile("login2", "password2", new User("Petya", 350))});
                }
                return _profilesRepository;
            }
        }

        private void LoadGameResults()
        {
            try
            {
                using (StreamReader reader = new StreamReader(GameService_Constants.ResultsPath, Encoding.Default))
                {
                    string jSon = reader.ReadToEnd();

                    _gameResultsRepository = new GameResultsRepository(JsonConvert.DeserializeObject<List<GameResult>>(jSon));
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

                IEnumerable<GameResult> gameResults = _gameResultsRepository.GetAll();
                string jSon = JsonConvert.SerializeObject(gameResults);

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
