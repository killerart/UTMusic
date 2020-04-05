﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.DataAccess.Interfaces;
using UTMusic.DataAccess.EFContexts;
using UTMusic.DataAccess.Entities;

namespace UTMusic.DataAccess.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly MusicContext db;

        public UserRepository(MusicContext context) => db = context;

        public IEnumerable<User> GetAll() => db.Users;

        public User Get(int id) => db.Users.Find(id);

        public void Create(User user) => db.Users.Add(user);

        public void Update(User user) => db.Entry(user).State = EntityState.Modified;

        public IEnumerable<User> Find(Func<User, Boolean> predicate) => db.Users.Where(predicate).ToList();

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
