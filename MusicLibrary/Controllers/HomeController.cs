using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MusicLibrary.Data;
using MusicLibrary.Models;
using MusicLibrary.Services.Interfaces;

namespace MusicLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAlbumService _albumService;

        public HomeController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET: Default
        public ActionResult Index()
        {
            var albums = _albumService.GetAllAlbums().Select(f => new ViewAllAlbumViewModel(f));

            return View(albums);
        }

        [HttpPost]
        public ActionResult Save(SaveAlbumViewModel saveAlbum)
        {
            var album = saveAlbum.ToAlbum();

            _albumService.AddNewAlbum(album);

            return Json(new {status="OK"});
        }
    }
}