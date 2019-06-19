using DA_Layer.Interfaces;
using DA_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_Layer.Repositories
{
    public class ProfilesRepository : IRepository<Profile>
    {
        public List<Profile> Profiles { get; set; }

        public ProfilesRepository(List<Profile> profiles)
        {
            this.Profiles = profiles;
        }

        public void Create(Profile item)
        {
            this.Profiles.Add(item);
        }

        public Profile Get(Profile item)
        {
            return this.Profiles.Where(profile => profile.Login == item.Login && profile.Password == item.Password).FirstOrDefault();
        }

        public IEnumerable<Profile> GetAll()
        {
            return this.Profiles;
        }
    }
}
