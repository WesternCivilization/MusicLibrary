using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MusicLibrary.Data;
using MusicLibrary.Models;

namespace MusicLibrary.Mapping
{
    public class AlbumProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Album, ViewAllAlbumViewModel>()
                .ForMember(f => f.AlbumLength, o => o.ResolveUsing(i => i.GetLength()))
                .ForMember(f => f.AlbumName, o => o.ResolveUsing(i => i.AlbumName))
                .ForMember(f => f.ArtistName, o => o.ResolveUsing(i => i.Artist?.ArtistName))
                .ForMember(f => f.ReleaseYear, o => o.ResolveUsing(i => i.ReleaseYear))
                .ForMember(f => f.Tracks, o => o.ResolveUsing(i => i.Tracks));

            Mapper.CreateMap<Track, TrackViewModel>()
                .ForMember(f => f.TrackLength, o => o.ResolveUsing(i => i.Length))
                .ForMember(f => f.TrackName, o => o.ResolveUsing(i => i.Name))
                .ForMember(f=>f.TrackNumber, o=>o.ResolveUsing(i=>i.Number));

        }
    }
}