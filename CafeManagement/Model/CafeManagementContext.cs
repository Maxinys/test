using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Model;

public partial class CafeManagementContext : DbContext
{
    public static CafeManagementContext _context;
    public CafeManagementContext()
    {
    }

    public CafeManagementContext(DbContextOptions<CafeManagementContext> options)
        : base(options)
    {
    }
    public static CafeManagementContext GetContext()
    {
        if (_context == null) _context = new CafeManagementContext();
        return _context;
    }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdish> Orderdishes { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Shiftemployee> Shiftemployees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=2580;database=lab5", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PRIMARY");

            entity.ToTable("dishes");

            entity.Property(e => e.DishId).HasColumnName("DishID");
            entity.Property(e => e.DishName).HasMaxLength(50);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.UserId, "UserID");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("employees_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.ShiftId, "ShiftID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderStatus).HasMaxLength(20);
            entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

            entity.HasOne(d => d.Shift).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<Orderdish>(entity =>
        {
            entity.HasKey(e => e.OrderDishId).HasName("PRIMARY");

            entity.ToTable("orderdishes");

            entity.HasIndex(e => e.DishId, "DishID");

            entity.HasIndex(e => e.OrderId, "OrderID");

            entity.Property(e => e.OrderDishId).HasColumnName("OrderDishID");
            entity.Property(e => e.DishId).HasColumnName("DishID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Dish).WithMany(p => p.Orderdishes)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("orderdishes_ibfk_2");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdishes)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("orderdishes_ibfk_1");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PRIMARY");

            entity.ToTable("shifts");

            entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.ShiftType).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Shiftemployee>(entity =>
        {
            entity.HasKey(e => e.ShiftEmployeeId).HasName("PRIMARY");

            entity.ToTable("shiftemployees");

            entity.HasIndex(e => e.EmployeeId, "EmployeeID");

            entity.HasIndex(e => e.ShiftId, "ShiftID");

            entity.Property(e => e.ShiftEmployeeId).HasColumnName("ShiftEmployeeID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Shiftemployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("shiftemployees_ibfk_2");

            entity.HasOne(d => d.Shift).WithMany(p => p.Shiftemployees)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("shiftemployees_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
