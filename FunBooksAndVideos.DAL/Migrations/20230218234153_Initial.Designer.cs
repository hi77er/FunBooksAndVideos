﻿// <auto-generated />
using System;
using FunBooksAndVideos.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunBooksAndVideos.DAL.Migrations
{
    [DbContext(typeof(FunDbContext))]
    [Migration("20230218234153_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SubscriptionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.ItemAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AttributeType")
                        .HasColumnType("int");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemAttributes");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ItemId");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.PurchaseOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.PurchaseOrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("PurchaseOrderItem");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.ShippingSlip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ShippingCarrier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("ShippingSlips");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.ItemAttribute", b =>
                {
                    b.HasOne("FunBooksAndVideos.DAL.Entities.Item", "Item")
                        .WithMany("Attributes")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.Membership", b =>
                {
                    b.HasOne("FunBooksAndVideos.DAL.Entities.Customer", null)
                        .WithMany("Memberships")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunBooksAndVideos.DAL.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.PurchaseOrder", b =>
                {
                    b.HasOne("FunBooksAndVideos.DAL.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.PurchaseOrderItem", b =>
                {
                    b.HasOne("FunBooksAndVideos.DAL.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunBooksAndVideos.DAL.Entities.PurchaseOrder", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.ShippingSlip", b =>
                {
                    b.HasOne("FunBooksAndVideos.DAL.Entities.PurchaseOrder", "Order")
                        .WithOne("ShippingSlip")
                        .HasForeignKey("FunBooksAndVideos.DAL.Entities.ShippingSlip", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.Customer", b =>
                {
                    b.Navigation("Memberships");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.Item", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("FunBooksAndVideos.DAL.Entities.PurchaseOrder", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("ShippingSlip");
                });
#pragma warning restore 612, 618
        }
    }
}
