using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Interfaces
{
    public interface IProfile
    {
        Task<List<Profile>> GetAllProfiles();
        Task<Profile> GetProfile(int id);
        Task<CommonResult> CreateProfile(Profile profile);
        Task<CommonResult> UpdateProfile(Profile exisitng, Profile profile);
        Task<bool> DeleteProfile(Profile profile);
    }
}
