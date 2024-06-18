﻿using AutoMapper;
using MealMasterAPI.Data;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MealMasterAPI.Repository
{
    public class ProfileRepository : IProfile
    {
        private readonly AppDbContext _bd;
        private readonly IMapper _mapper;

        public ProfileRepository(AppDbContext bd, IMapper mapper)
        {
            _bd = bd;
            _mapper = mapper;

        }

        public string CreateProfile(Guid idu, ProfileDTO profile)
        {
            var c = _bd.Users.Find(idu);

            if (c == null)
            {
                return "User not found.";
            }

            UserProfile us = new UserProfile
            {
                id = idu,
                sex = profile.sex,
                stature = profile.stature,
                weight = profile.weight,
                protocol = profile.protocol,
                user = c
            };

            _bd.UsersProfile.Add(us);

            Guardar();
            return "Cambios guardados";

        }

        public ProfileDTO GetProfile(Guid userid)
        {
            var user = _bd.UsersProfile.Find(userid);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            ProfileDTO profile = _mapper.Map<ProfileDTO>(user);

            return profile;
        }

        public string UpdateProfile(Guid id, ProfileDTO profiledto)
        {
            var up = _bd.UsersProfile.Find(id);

            if (up == null)
            {
                return "User not found.";
            }

            up.sex = profiledto.sex;
            up.stature = profiledto.stature;
            up.weight = profiledto.weight;
            up.protocol = profiledto.protocol;

            _bd.UsersProfile.Update(up);
            Guardar();
            return "Update completed";

        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
