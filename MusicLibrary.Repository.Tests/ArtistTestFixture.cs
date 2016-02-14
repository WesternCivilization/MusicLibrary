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
    public class ArtistTestFixture
    {
        private IDbConnection _connection;
        private IArtistRepository _artistRepository;

        [OneTimeSetUp]
        public void AlbumTestFixtureSetup()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            _artistRepository = new ArtistRepository(_connection);
        }

        [Test]
        public void GetAllArtistsTest()
        {
            var res = _artistRepository.GetAllArtists().ToList();

            Assert.NotNull(res);
            Assert.AreEqual(3, res.Count);

        }

        [Test]
        public void GetArtistByNameTest()
        {
            var res = _artistRepository.GetArtistByName("Frank Zappa");

            Assert.NotNull(res);
            Assert.AreEqual("Frank Zappa", res.ArtistName);

        }

        [Test]
        public void GetAllArtistByInvalidNameTest()
        {
            Assert.Throws<System.InvalidOperationException>(() => _artistRepository.GetArtistByName("AC/DC"));
        }

    }
}
