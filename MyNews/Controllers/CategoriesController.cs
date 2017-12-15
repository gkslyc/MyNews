﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNews.Core.Models;
using Microsoft.EntityFrameworkCore;
using MyNews.Core.Data;
using MyNews.Models;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MyNews.Controllers
{
    public class CategoriesController : Controller
    {
        private AplicationDbContext context;

        public CategoriesController(AplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var news = context.News.Include(i => i.Category).Where(n => n.IsPublished == true)
                            .OrderByDescending(o => o.PublishDate).Take(10).ToList();

            var categories = context.Categories.OrderBy(c => c.Name).Select(s => new CategoryViewModel { Id = s.Id, Name = s.Name, Count = s.News.Count }).ToList();

            ViewBag.Categories = categories;

            return View(news);
        }
    }
}