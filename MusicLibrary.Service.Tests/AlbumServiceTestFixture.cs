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
    public class AlbumServiceTestFixture
    {
        private IAlbumService _albumService;
        private Mock<IAlbumRepository> _albumRepository;
        private Mock<IArtistRepository> _artistRepository;
        private Mock<IGenreRepository> _genreRepository;


        [OneTimeSetUp]
        public void AlbumsTestFixtureSetup()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _artistRepository = new Mock<IArtistRepository>();
            _genreRepository = new Mock<IGenreRepository>();

           _albumService = new AlbumService(_albumRepository.Object, _artistRepository.Object,_genreRepository.Object );

        }

        [Test]
        public void GetNewAlbumsTwoNewAlbumsTest()
        {
            var album = new List<Album>()
            {
                new Album {ReleaseYear = 2016},
                new Album {ReleaseYear = 2016},
                new Album {ReleaseYear = 2015},
                new Album {ReleaseYear = 1990},
                new Album {ReleaseYear = 1991},
                new Album {ReleaseYear = 1991}
            };

            _albumRepository.Setup(f => f.GetAllAlbums()).Returns(album);

            var res = _albumService.GetNewAlbums().ToList();

            Assert.NotNull(res);
            Assert.IsNotEmpty(res);

            Assert.AreEqual(3,res.Count);

        }

        [Test]
        public void GetNewAlbumsNoNewAlbumsTest()
        {
            var album = new List<Album>()
            {
                new Album {ReleaseYear = 1990},
                new Album {ReleaseYear = 1991},
                new Album {ReleaseYear = 1991}
            };

            _albumRepository.Setup(f => f.GetAllAlbums()).Returns(album);

            var res = _albumService.GetNewAlbums().ToList();

            Assert.NotNull(res);
            Assert.IsEmpty(res);

        }

        [Test]
        public void AddNewAlbumValidArtistValidGenreTest()
        {
            var album = new Album
            {
                AlbumName = "Terrapin Station",
                Artist = new Artist
                {
                    ArtistName = "Grateful Dead"
                },
                Genre = new Genre
                {
                    GenreName = "Rock"
                },
                ReleaseYear = 1977,
                Tracks = new List<Track>()
                {
                    new Track {Name = "Estimated Prophet", Number = 1, Length = 5},
                    new Track {Name = "Dancin' in the Streets", Number = 2, Length = 3},
                    new Track {Name = "Passenger", Number = 3, Length = 2},
                    new Track {Name = "Samson and Delilah", Number = 4, Length = 3},
                    new Track {Name = "Sunrise", Number = 5, Length = 3},
                    new Track {Name = "Terrapin Station Park 1", Number = 6, Length = 16},
                }
            };

            _artistRepository.Setup(f => f.GetArtistByName(It.IsAny<string>())).Returns(new Artist
            {
                ArtistName = "Grateful Dead"
            });

            _genreRepository.Setup(f => f.GetGenreByName(It.IsAny<string>())).Returns(new Genre
            {
                GenreName = "Rock"
            });

            _albumRepository.Setup(f => f.CreateAlbum(album)).Returns(1);

            var res = _albumService.AddNewAlbum(album);

            Assert.NotNull(res);
            Assert.AreEqual(res.Id,1);
        }

        [Test]
        public void AddNewAlbumInvalidArtistValidGenreTest()
        {
            var album = new Album
            {
                AlbumName = "Terrapin Station",
                Artist = new Artist
                {
                    ArtistName = "Grateful Dead"
                },
                Genre = new Genre
                {
                    GenreName = "Rock"
                },
                ReleaseYear = 1977,
                Tracks = new List<Track>()
                {
                    new Track {Name = "Estimated Prophet", Number = 1, Length = 5},
                    new Track {Name = "Dancin' in the Streets", Number = 2, Length = 3},
                    new Track {Name = "Passenger", Number = 3, Length = 2},
                    new Track {Name = "Samson and Delilah", Number = 4, Length = 3},
                    new Track {Name = "Sunrise", Number = 5, Length = 3},
                    new Track {Name = "Terrapin Station Park 1", Number = 6, Length = 16},
                }
            };

            _artistRepository.Setup(f => f.GetArtistByName(It.IsAny<string>())).Returns(new Artist
            {
                ArtistName = "Grateful Dead"
            });


            _genreRepository.Setup(f => f.GetGenreByName(It.IsAny<string>())).Throws<Exception>();
            _genreRepository.Setup(f => f.CreateGenre(It.IsAny<Genre>())).Returns(98);

            _albumRepository.Setup(f => f.CreateAlbum(album)).Returns(1);

            var res = _albumService.AddNewAlbum(album);

            Assert.NotNull(res);
            Assert.AreEqual(1,res.Id);
            Assert.NotNull(res.Genre);
            Assert.AreEqual(98,res.Genre.Id);
        }


        [Test]
        public void AddNewAlbumValidArtistInValidGenreTest()
        {
            var album = new Album
            {
                AlbumName = "Terrapin Station",
                Artist = new Artist
                {
                    ArtistName = "Grateful Dead"
                },
                Genre = new Genre
                {
                    GenreName = "Rock"
                },
                ReleaseYear = 1977,
                Tracks = new List<Track>()
                {
                    new Track {Name = "Estimated Prophet", Number = 1, Length = 5},
                    new Track {Name = "Dancin' in the Streets", Number = 2, Length = 3},
                    new Track {Name = "Passenger", Number = 3, Length = 2},
                    new Track {Name = "Samson and Delilah", Number = 4, Length = 3},
                    new Track {Name = "Sunrise", Number = 5, Length = 3},
                    new Track {Name = "Terrapin Station Park 1", Number = 6, Length = 16},
                }
            };

            _artistRepository.Setup(f => f.GetArtistByName(It.IsAny<string>())).Throws<Exception>();
            _artistRepository.Setup(f => f.CreateArtist(It.IsAny<Artist>())).Returns(99);

            _genreRepository.Setup(f => f.GetGenreByName(It.IsAny<string>())).Returns(new Genre
            {
                GenreName = "Rock"
            });

            _albumRepository.Setup(f => f.CreateAlbum(album)).Returns(1);

            var res = _albumService.AddNewAlbum(album);

            Assert.NotNull(res);
            Assert.AreEqual(1, res.Id);
            Assert.NotNull(res.Artist);
            Assert.AreEqual(99, res.Artist.Id);
        }

        [Test]
        public void AddNewAlbumValidArtistValidGenreNoTracksTest()
        {
            var album = new Album
            {
                AlbumName = "Terrapin Station",
                Artist = new Artist
                {
                    ArtistName = "Grateful Dead"
                },
                Genre = new Genre
                {
                    GenreName = "Rock"
                },
                ReleaseYear = 1977,
                Tracks = new List<Track>()
                {

                }
            };

            _artistRepository.Setup(f => f.GetArtistByName(It.IsAny<string>())).Returns(new Artist
            {
                ArtistName = "Grateful Dead"
            });

            _genreRepository.Setup(f => f.GetGenreByName(It.IsAny<string>())).Returns(new Genre
            {
                GenreName = "Rock"
            });

            _albumRepository.Setup(f => f.CreateAlbum(album)).Returns(1);

            var res = _albumService.AddNewAlbum(album);

            Assert.Null(res);
        }

    }
}


/*   


        public void AddNewAlbum(Album album)
        {
            using (var scope = new TransactionScope())
            {

                if (!DoesArtistExist(album.Artist))
                {
                    album.Artist.Id =  _artistRepository.CreateArtist(album.Artist);
                }

                if (!DoesGenreExist(album.Genre))
                {
                    album.Genre.Id = _genreRepository.CreateGenre(album.Genre);
                }

                if (!album.TracksCountValid())
                    return;


                _albumRepository.CreateAlbum(album);

                scope.Complete();
            }
        }

        private bool DoesGenreExist(Genre genre)
        {

            try
            {
                _genreRepository.GetGenreByName(genre.GenreName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DoesArtistExist(Artist artist)
        {

            try
            {
               _artistRepository.GetArtistByName(artist.ArtistName);
                return true;
            }
            catch
            {
                return false;
            }
        }*/
