using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Data;
using MusicLibrary.DataAccess;
using MusicLibrary.DataAccess.Interfaces;
using NUnit.Framework;

namespace MusicLibrary.Repository.Tests
{
    [TestFixture]
    public class AlbumTestFixture
    {
        private IDbConnection _connection;
        private IAlbumRepository _albumRepository;
        private ITrackRepository _trackRepository;
        
        [OneTimeSetUp]
        public void AlbumTestFixtureSetup()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            _trackRepository = new TrackRepository(_connection);
            _albumRepository = new AlbumRepository(_connection, _trackRepository);
        }

        [Test]
        public void GetAllAlbumsTest()
        {
            var res = _albumRepository.GetAllAlbums().ToList();

            Assert.NotNull(res);
            Assert.AreEqual(3,res.Count);

        }

        [Test]
        public void GetAllAlbumsByNameTest()
        {
            var res = _albumRepository.GetAlbumByName("Hot Rats");

            Assert.NotNull(res);
            Assert.AreEqual("Hot Rats", res.AlbumName);
            Assert.AreEqual("Frank Zappa", res.Artist.ArtistName);
            Assert.AreEqual(6,res.Tracks.Count());

        }

        [Test]
        public void GetAllAlbumsByGenreTest()
        {
            var res = _albumRepository.FindAllAlbumsByGenre(new Genre {GenreName = "Metal"});

            Assert.NotNull(res);
            var resList = res.ToList();
            Assert.AreEqual(1, resList.Count);
            Assert.AreEqual("Holy Diver", resList[0].AlbumName);

        }


        [Test]
        public void GetAllAlbumsByArtistTest()
        {
            var res = _albumRepository.FindAllAlbumsByArtist(new Artist { ArtistName = "Frank Zappa" });

            Assert.NotNull(res);
            var resList = res.ToList();
            Assert.AreEqual(1, resList.Count);
            Assert.AreEqual("Hot Rats", resList[0].AlbumName);

        }
    }
}
