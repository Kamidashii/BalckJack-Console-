using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL_Layer.Interfaces
{
    public interface IProfile
    {
        string Login { get; set; }
        string Password { get; set; }

        IPlayer Player { get; set; }

        DA_Layer.Models.Profile GetProfileToDB();

        IProfile GetProfileFromDB(DA_Layer.Models.Profile DAprofile);
    }
}
