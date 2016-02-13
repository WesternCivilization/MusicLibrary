using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        [OneTimeSetUp]
        public void AlbumTestFixtureSetup()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            _albumRepository = new AlbumRepository(_connection);
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
            Assert.AreEqual("Frank Zappa", res.Artist.AristName);
            Assert.AreEqual(6,res.Tracks.Count());

        }

    }
}
