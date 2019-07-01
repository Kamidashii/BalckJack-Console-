using System;
using BlackJackDataAccess.Interfaces;
using BlackJackDataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using BlackJackDataAccess.Interfaces.Repositories;

namespace BlackJackDataAccess.Repositories
{
    public class ProfilesRepository : IRepository<Profile>
    {
        public List<Profile> Profiles { get; set; }

        public ProfilesRepository(List<Profile> profiles)
        {
            Profiles = profiles;
        }

        public void Create(Profile item)
        {
            Profiles.Add(item);
        }

        public Profile Get(Profile item)
        {
            return Profiles.Where(profile => profile.Login == item.Login && profile.Password == item.Password).FirstOrDefault();
        }

        public IEnumerable<Profile> GetAll()
        {
            return Profiles;
        }
    }
}
