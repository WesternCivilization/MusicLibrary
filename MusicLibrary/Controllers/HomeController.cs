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
        private IAlbumService _albumService;
        private IPlaylistService _playlistService;

        public HomeController(IAlbumService albumService, IPlaylistService playlistService)
        {
            _playlistService = playlistService;
            _albumService = albumService;
        }

        // GET: Default
        public ActionResult Index()
        {
            var albums = _albumService.GetAllAlbums().Select(f => new ViewAllAlbumViewModel(f));

            return View(albums);
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}