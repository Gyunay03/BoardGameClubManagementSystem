using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesClubManagementSystem.Models;

public partial class BoardGamesContext : DbContext
{
    public BoardGamesContext()
    {
    }

    public BoardGamesContext(DbContextOptions<BoardGamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoardGame> BoardGames { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Posetiteli> Posetitelis { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardGames;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoardGame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BoardGam__3214EC072BDFDE19");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventNumber).HasName("PK__Events__78A1A19C6D438F8D");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__50E1AF316F6E72BE");

            entity.ToTable("Menu");

            entity.Property(e => e.Category).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC073C0C0260");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_ToPosetiteli");

            entity.HasOne(d => d.Event).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_ToEvents");

            entity.HasOne(d => d.Game).WithMany(p => p.Orders)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_ToBoardGames");

            entity.HasOne(d => d.Menu).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_ToMenu");
        });

        modelBuilder.Entity<Posetiteli>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posetite__3214EC070EAA8994");

            entity.ToTable("Posetiteli");

            entity.Property(e => e.Name).HasMaxLength(60);
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.Reservation).WithMany(p => p.Posetitelis)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posetiteli_ToReservations");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationNumber).HasName("PK__tmp_ms_x__FAA69AEA9ADB271C");

            entity.Property(e => e.Comment).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.Name).HasMaxLength(60);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Status).HasMaxLength(10);

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0718102E66");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Role).HasMaxLength(30);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
