﻿using h37.Models;
using h37.Models.Entities;
using h37.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;


namespace h37.Services
{

    public class UserServices
    {
        private ApplicationDbContext db;

        public UserServices()
        {
            db = new ApplicationDbContext();
        }

        public void subscribeUser(int projectID, string userName)
        {
            /* Todo */
        }
        public void unsubscribeUser(int projectID, string userName)
        {
            /* Todo */
        }
    }
}