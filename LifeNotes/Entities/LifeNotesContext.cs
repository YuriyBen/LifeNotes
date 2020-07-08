using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LifeNotes.Entities
{
    public partial class LifeNotesContext : DbContext
    {
        public LifeNotesContext()
        {
        }

        public LifeNotesContext(DbContextOptions<LifeNotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=LifeNotesDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateInfo).HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Note__UserId__2A4B4B5E");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__UserInfo__A9D10534AC4A3EE7")
                    .IsUnique();

                entity.HasIndex(e => e.EmailConfirmationToken)
                    .HasName("UQ__UserInfo__F9CA6B6F09032B03")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__UserInfo__C9F284567F0FBC59")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmailConfirmationToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
