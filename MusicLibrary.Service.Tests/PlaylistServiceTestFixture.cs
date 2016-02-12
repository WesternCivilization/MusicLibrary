using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MusicLibrary.Data;
using MusicLibrary.DataAccess.Interfaces;
using MusicLibrary.Services;
using MusicLibrary.Services.Interfaces;
using NUnit.Framework;

namespace MusicLibrary.Service.Tests
{
    [TestFixture]
    public class PlaylistServiceTestFixture
    {

        private IPlaylistService _playlistService;
        private Mock<IPlaylistRepository> _playlistRepository;


        [OneTimeSetUp]
        public void PlaylistServiceTestFixtureSetup()
        {
            _playlistRepository = new Mock<IPlaylistRepository>();

            _playlistService = new PlaylistService(_playlistRepository.Object);
        }

        [Test]
        public void TestGetAllPlaylists()
        {
            var playlists = new[]
            {
                new Playlist
                {
                    PlaylistName = "Workout Mix",
                    Tracks = new[]
                    {
                        new Track {Length = 5, Name = "Holy Diver", Number = 1},
                        new Track {Length = 3, Name = "Hells Bells", Number = 1},
                        new Track {Length = 6, Name = "Joes Garage", Number = 1}
                    }
                },
                new Playlist
                {
                    PlaylistName = "Chill Mix",
                    Tracks = new[]
                    {
                        new Track {Length = 12, Name = "Terrapin Station", Number = 5},
                        new Track {Length = 20, Name = "Run Like An Antelope", Number = 10},
                        new Track {Length = 5, Name = "Wish You Were Here", Number = 3}
                    }
                }
            };

            _playlistRepository.Setup(f => f.GetAllPlaylists()).Returns(playlists);

            var res = _playlistService.GetAllPlaylists();

            Assert.IsNotEmpty(res.Where(f=>f.PlaylistName == "Workout Mix"));
            Assert.IsNotEmpty(res.Where(f=>f.PlaylistName == "Chill Mix"));
        }
    }
}
