using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KreditPlus_API.Models;

namespace KreditPlus_API.Entity
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblMenu> TblMenus { get; set; } = null!;
        public virtual DbSet<TblMenuType> TblMenuTypes { get; set; } = null!;
        public virtual DbSet<TblOrder> TblOrders { get; set; } = null!;
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; } = null!;
        public virtual DbSet<TblOrderStatus> TblOrderStatuses { get; set; } = null!;
        public virtual DbSet<TblStatusMenu> TblStatusMenus { get; set; } = null!;
        public virtual DbSet<TblTransaction> TblTransactions { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserType> TblUserTypes { get; set; } = null!;
        public virtual DbSet<VMenu> VMenus { get; set; } = null!;
        public virtual DbSet<VOrder> VOrders { get; set; } = null!;
        public virtual DbSet<VTransaction> VTransactions { get; set; } = null!;
        public virtual DbSet<VUser> VUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_tbl_item");

                entity.ToTable("tbl_menu");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.MenuName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("menu_name");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.StatusMenuId).HasColumnName("status_menu_id");

                entity.Property(e => e.MenuTypeId).HasColumnName("menu_type_id");
            });

            modelBuilder.Entity<TblMenuType>(entity =>
            {
                entity.HasKey(e => e.MenuTypeId);

                entity.ToTable("tbl_menu_type");

                entity.Property(e => e.MenuTypeId).HasColumnName("menu_type_id");

                entity.Property(e => e.MenuTypeDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("menu_type_desc");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("tbl_order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CreatedByCashier).HasColumnName("created_by_cashier");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("order_number");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.OrderedByWaiters).HasColumnName("ordered_by_waiters");

                entity.Property(e => e.TableNumber).HasColumnName("table_number");
                entity.Property(e => e.ClosedBy).HasColumnName("closed_by");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId);

                entity.ToTable("tbl_order_detail");

                entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(10)
                    .HasColumnName("order_id")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Qty).HasColumnName("qty");
            });

            modelBuilder.Entity<TblOrderStatus>(entity =>
            {
                entity.HasKey(e => e.OrderStatusId);

                entity.ToTable("tbl_order_status");

                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");

                entity.Property(e => e.OrderStatusDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("order_status_desc");
            });

            modelBuilder.Entity<TblStatusMenu>(entity =>
            {
                entity.HasKey(e => e.StatusMenuId);

                entity.ToTable("tbl_status_menu");

                entity.Property(e => e.StatusMenuId).HasColumnName("status_menu_id");

                entity.Property(e => e.StatusMenuDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("status_menu_desc");
            });

            modelBuilder.Entity<TblTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tbl_transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("order_number");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tbl_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Token)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenExpired)
                    .HasColumnType("datetime")
                    .HasColumnName("token_expired");

                entity.Property(e => e.UserFullName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("user_full_name");

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserType).HasColumnName("user_type");
            });

            modelBuilder.Entity<TblUserType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);

                entity.ToTable("tbl_user_type");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_type_name");
            });

            modelBuilder.Entity<VMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_menu");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.MenuName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("menu_name");

                entity.Property(e => e.MenuTypeDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("menu_type_desc");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.StatusMenuDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("status_menu_desc");
            });

            modelBuilder.Entity<VOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_order");

                entity.Property(e => e.Cashier)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("cashier");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("order_number");

                entity.Property(e => e.StatusOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status_order");

                entity.Property(e => e.TableNumber).HasColumnName("table_number");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.Property(e => e.Waiters)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("waiters");
                entity.Property(e => e.ClosedByName)
                    .HasColumnType("string")
                    .HasColumnName("closed_by_name");
            });

            modelBuilder.Entity<VTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_transaction");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.UserWaitersOrder)
                    .HasColumnType("int")
                    .HasColumnName("user_waiters_order");

                entity.Property(e => e.UserWaitersNameOrder)
                    .HasColumnType("string")
                    .HasColumnName("user_waiters_name_order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("order_id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("order_number");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");
            });

            modelBuilder.Entity<VUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_user");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Token)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenExpired)
                    .HasColumnType("datetime")
                    .HasColumnName("token_expired");

                entity.Property(e => e.UserFullName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("user_full_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserType).HasColumnName("user_type");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_type_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
