﻿using System;
using AutoMapper;
using BELibrary.Core.Entity;
using BELibrary.DbContext;
using BELibrary.Models.View;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CinemaOnline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var workScope = new UnitOfWork(new CinemaOnlineDbContext()))
            { 
                var calenderFilmIds = workScope.MovieCalendars.Query(x => !x.IsDelete)
                    .OrderByDescending(mc => mc.DateTimeDetail)
                    .Take(6)
                    .Select(x => x.FilmId);

                var filmsInCalendar = workScope.Films.Query(x => calenderFilmIds.Contains(x.Id));

                var otherFilms = workScope.Films.Query(x => !calenderFilmIds.Contains(x.Id))
                    .OrderBy(x => Guid.NewGuid()) // Randomize the order
                    .Take(10)
                    .ToList();

                var films = filmsInCalendar.Union(otherFilms).ToList();
                //TODAY BEST CHOICE

                var filmViews = Mapper.Map<List<FilmView>>(films);

                foreach (var filmView in filmViews)
                {
                    filmView.FilmMovieTypes = workScope.FilmMovieTypes.GetListFilmMovieType(filmView.Id);
                    filmView.FilmMovieDisplayTypes = workScope.FilmMovieDisplayTypes.GetListFilmMovieDisplayType(filmView.Id);
                    filmView.Name = filmView.Name + " (" + string.Join(", ", filmView.FilmMovieDisplayTypes.ToArray()) + ")";
                }
                //filmViews.AddRange(filmViews);
                // filmViews.AddRange(filmViews);

                ViewBag.BestChoices = filmViews.Take(6).ToList();

                //NOW IN THE CINEMA

                ViewBag.Films = filmViews.Take(8).ToList();

                //LATEST NEWS
            }

            //var test = dbContext.News.FirstOrDefault();
            //var testview = Mapper.Map<NewsViewModel>(test);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page. ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page. ";

            return View();
        }

        public ActionResult E404()
        {
            ViewBag.Message = "Your contact page. ";

            return View();
        }

        public ActionResult Error(string mess)
        {
            ViewBag.Message = mess;

            return View();
        }
    }
}