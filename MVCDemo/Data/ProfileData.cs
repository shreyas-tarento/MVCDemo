using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Context;
using MVCDemo.Interfaces;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Data
{
    public class ProfileData : IProfile
    {
        private readonly SqlDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileData(SqlDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<CommonResult> CreateProfile(Profile profile)
        {
            return await PhotoSave(profile, CRUDType.CREATE);
        }

        public async Task<bool> DeleteProfile(Profile profile)
        {
            try
            {
                if (profile.PhotoPath != null)
                    DeletePhoto(profile.PhotoPath);

                _context.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public async Task<List<Profile>> GetAllProfiles()
        {
            return await _context.Profile.ToListAsync();
        }

        public async Task<Profile> GetProfile(int id)
        {
            return await _context.Profile.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CommonResult> UpdateProfile(Profile exisitng, Profile profile)
        {
            exisitng.FirstName = profile.FirstName;
            exisitng.LastName = profile.LastName;
            if(profile.PhotoName != null)
            {
                exisitng.PhotoName = profile.PhotoName;
            }
            if(profile != null && exisitng.PhotoPath != null)
            {
                DeletePhoto(exisitng.PhotoPath);
            }
            exisitng.Photo = profile.Photo;
            return await PhotoSave(exisitng, CRUDType.UPDATE);
        }

        public String PhotoNameParser(String name)
        {
            return $"{name}_{Guid.NewGuid()}";
        }

        public enum CRUDType
        {
            CREATE,
            UPDATE
        }
        public async Task<CommonResult> PhotoSave(Profile profile, CRUDType type)
        {
            if (profile.Photo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Images\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Images\\");
                    }
                    String photoName = "";
                    if (profile.PhotoName == null)
                    {
                        photoName = PhotoNameParser(profile.Photo.FileName);
                    }
                    else
                    {
                        photoName = PhotoNameParser(profile.PhotoName);
                    }
                    var filepath = _webHostEnvironment.WebRootPath + "\\Images\\" + photoName;
                    using (FileStream fileStream =
                        File.Create(filepath))
                    {
                        await profile.Photo.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();

                        profile.PhotoPath = filepath;
                        profile.Photo = null;
                        if(type == CRUDType.CREATE)
                        {
                            await _context.Profile.AddAsync(profile);
                        }
                        else if(type == CRUDType.UPDATE)
                        {
                        }
                        else
                        {

                        }
                        await _context.SaveChangesAsync();
                        return new CommonResult()
                        {
                            Error = new List<string>() { },
                            Success = profile,
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new CommonResult()
                    {
                        Error = new List<string>()
                    {
                        ex.ToString(),
                    }
                    };
                }
            }
            else
            {
                if (type == CRUDType.CREATE)
                {
                    await _context.Profile.AddAsync(profile);
                }
                else if (type == CRUDType.UPDATE)
                {
                }
                else
                {

                }
                await _context.SaveChangesAsync();
                return new CommonResult()
                {
                    Error = new List<string>() { },
                    Success = profile,
                };
            }
        }
   
        public void DeletePhoto(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
