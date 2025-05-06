using api_tema1.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace api_tema1.Database.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Adăugarea datelor de test pentru cărți
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Amintiri din copilărie", Author = "Ion Creangă", Year = 1892, ISBN = "9789731038742" },
                new Book { Id = 2, Title = "Moara cu noroc", Author = "Ioan Slavici", Year = 1881, ISBN = "9789731042367" },
                new Book { Id = 3, Title = "Ultima noapte de dragoste, întâia noapte de război", Author = "Camil Petrescu", Year = 1930, ISBN = "9789731051239" }
            );

            // Adăugarea datelor de test pentru recenzii
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, BookId = 1, ReaderName = "Maria Popescu", Rating = 5, Comment = "O carte minunată!", ReviewDate = new DateTime(2023, 5, 15) },
                new Review { Id = 2, BookId = 1, ReaderName = "Ion Ionescu", Rating = 4, Comment = "Distractivă și educativă.", ReviewDate = new DateTime(2023, 6, 10) },
                new Review { Id = 3, BookId = 2, ReaderName = "Elena Vasilescu", Rating = 5, Comment = "O capodoperă a literaturii române!", ReviewDate = new DateTime(2023, 4, 22) },
                new Review { Id = 4, BookId = 3, ReaderName = "Alexandru Munteanu", Rating = 4, Comment = "Foarte bine scrisă, dar destul de tristă.", ReviewDate = new DateTime(2023, 7, 3) }
            );
        }
    }
}