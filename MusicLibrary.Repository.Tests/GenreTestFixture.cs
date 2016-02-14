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
    public class GenreTestFixture
    {
        private IDbConnection _connection;
        private IGenreRepository _genreRepository;

        [OneTimeSetUp]
        public void GenreTestFixtureSetup()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            _genreRepository = new GenreRepository(_connection);
        }

        [Test]
        public void GetAllGenresTest()
        {
            var res  =_genreRepository.GetAllGenres();
            Assert.NotNull(res);
            var collection = res as IList<Genre> ?? res.ToList();
            Assert.IsNotEmpty(collection);
            Assert.AreEqual(2,collection.Count());
        }

        [Test]
        public void GetAllGenreByNameTest()
        {
            var res = _genreRepository.GetGenreByName("Rock");
            Assert.NotNull(res);
            Assert.AreEqual(res.GenreName,"Rock");
        }

        [Test]
        public void GetAllGenreByInvalidNameTest()
        {
            Assert.Throws<System.InvalidOperationException>(()=>  _genreRepository.GetGenreByName("Jazz"));
        }
    }
}
