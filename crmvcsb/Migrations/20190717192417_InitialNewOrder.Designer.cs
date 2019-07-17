﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using crmvcsb.Infrastructure.EF.newOrder;

namespace mvccoresb.Migrations
{
    [DbContext(typeof(NewOrderContext))]
    [Migration("20190717192417_InitialNewOrder")]
    partial class InitialNewOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.AddressDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<int>("Code");

                    b.Property<string>("Country");

                    b.Property<string>("Province");

                    b.Property<string>("State");

                    b.Property<string>("StreetName");

                    b.HasKey("Id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.ClientDAL", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientName");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.DeliveryItemDAL", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeliveryName");

                    b.Property<int>("DeliveryNumber");

                    b.HasKey("Id");

                    b.ToTable("DeliveryItems");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.GoodsDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductName");

                    b.HasKey("Id");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.OrderDAL", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClientId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.PhysicalDimensionDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int?>("DimensionUnitId");

                    b.Property<string>("ParameterName");

                    b.HasKey("Id");

                    b.HasIndex("DimensionUnitId");

                    b.ToTable("PhysicalDimensions");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.PhysicalUnitDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PhysicalUnits");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.RouteDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.RouteVertexDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Distance");

                    b.Property<int?>("FromId");

                    b.Property<int>("InRouteMoveOrder");

                    b.Property<int>("PriorityWeigth");

                    b.Property<int?>("RouteDALId");

                    b.Property<int?>("ToId");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("RouteDALId");

                    b.HasIndex("ToId");

                    b.ToTable("RouteVertex");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.OrderDAL", b =>
                {
                    b.HasOne("crmvcsb.Domain.NewOrder.DAL.ClientDAL", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.PhysicalDimensionDAL", b =>
                {
                    b.HasOne("crmvcsb.Domain.NewOrder.DAL.PhysicalUnitDAL", "DimensionUnit")
                        .WithMany()
                        .HasForeignKey("DimensionUnitId");
                });

            modelBuilder.Entity("crmvcsb.Domain.NewOrder.DAL.RouteVertexDAL", b =>
                {
                    b.HasOne("crmvcsb.Domain.NewOrder.DAL.AddressDAL", "From")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("crmvcsb.Domain.NewOrder.DAL.RouteDAL")
                        .WithMany("RouteVertexes")
                        .HasForeignKey("RouteDALId");

                    b.HasOne("crmvcsb.Domain.NewOrder.DAL.AddressDAL", "To")
                        .WithMany()
                        .HasForeignKey("ToId");
                });
#pragma warning restore 612, 618
        }
    }
}
