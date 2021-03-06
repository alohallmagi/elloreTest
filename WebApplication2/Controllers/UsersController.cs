﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsersController : ApiController
    {
        List<UserModel> users;
        public UsersController()
        {
            CarModel carModel = new CarModel();
            List<CarModel> cars = carModel.FillCars();

            users = new List<UserModel>
            {
                new UserModel {id=0,Name="Teet",Cars=cars[0]},
                new UserModel {id=1,Name="Teet",Cars=cars[2]},
                new UserModel {id=2,Name="Simmo",Cars=cars[1]}
            };
        }
        public IHttpActionResult Get()
        {
            return Ok(users);
        }
        public IHttpActionResult Get(string find)
        {
            var result = users.Where(x => x.Name.Equals(find));

            return Ok(result);
        }
        public IHttpActionResult Get(string find, string sort)
        {
            var result = users.Where(x => x.Name.Equals(find)).ToList();
            var splitSort = sort.Split(':');
            var sortBy = splitSort[0];
            var OrderBy = splitSort[1];
            bool isDesc = true;
            switch (OrderBy)
            {
                case "asc":
                    isDesc = false;
                    break;
                default:
                    isDesc = true;
                    break;
            }
            switch (sortBy)
            {
                case "name":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.Name).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.Name).ToList();
                    }
                    break;
                case "cars":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.Cars).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.Cars).ToList();
                    }
                    break;
                case "id":
                    if (isDesc)
                    {
                        result = result.OrderByDescending(x => x.id).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(x => x.id).ToList();
                    }
                    break;
                default:
                    break;
            }
            return Ok(result);
        }
        public IHttpActionResult Get(int id)
        {
            if (id>users.Count-1)
            {
                return NotFound();
            }
            return Ok(users[id]);
        }
    }
}
