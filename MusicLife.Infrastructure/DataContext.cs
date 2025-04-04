﻿using Microsoft.EntityFrameworkCore;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumSong>()
                .HasKey(a => new { a.AlbumId, a.SongId });

            modelBuilder.Entity<UserFavourite>()
                .HasKey(u => new { u.UserId, u.SongId });
        }
        public DbSet<Album> albums { get; set; }
        public DbSet<Artist> artists { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<PlayList> playlists { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Song> songs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Token> tokens { get; set; }
        public DbSet<AlbumSong> albumSong { get; set; }
        public DbSet<UserFavourite> userFavourites { get; set; }
    }
}
