﻿// <auto-generated />
using System;
using Festival.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Festival.DAL.Migrations
{
    [DbContext(typeof(FestivalDbContext))]
    partial class ICSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Festival.DAL.Entities.BandEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bands");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a59f68aa-cc0d-4e43-a384-ab461bba7d30"),
                            Description = "Mega super banda",
                            ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Metallica_at_The_O2_Arena_London_2008.jpg/1200px-Metallica_at_The_O2_Arena_London_2008.jpg",
                            Name = "Banda 1"
                        },
                        new
                        {
                            Id = new Guid("ff99de75-f759-4a56-b8b8-dcb9643c7620"),
                            Description = "Fuj Fuj banda",
                            ImgUrl = "https://scontent-prg1-1.xx.fbcdn.net/v/t1.6435-0/p526x296/45428212_322097681904122_8124368898846883840_n.jpg?_nc_cat=108&ccb=1-3&_nc_sid=8bfeb9&_nc_ohc=R2ImRaXPTQEAX_Avgv4&_nc_ht=scontent-prg1-1.xx&tp=6&oh=52dbae9db02f0817f70e76e26a65bc41&oe=60AF53FD",
                            Name = "Banda 2"
                        });
                });

            modelBuilder.Entity("Festival.DAL.Entities.EventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.HasIndex("StageId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dd63e50d-eb50-43df-97b2-39103be21325"),
                            BandId = new Guid("a59f68aa-cc0d-4e43-a384-ab461bba7d30"),
                            EndTime = new DateTime(2015, 10, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            StageId = new Guid("5a2c4194-5c8f-4caf-ba40-0bc93752e4f6"),
                            StartTime = new DateTime(2015, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("1b237e8c-9228-4b93-a863-5d80bf14bf7e"),
                            BandId = new Guid("ff99de75-f759-4a56-b8b8-dcb9643c7620"),
                            EndTime = new DateTime(2015, 8, 5, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            StageId = new Guid("5a2c4194-5c8f-4caf-ba40-0bc93752e4f6"),
                            StartTime = new DateTime(2015, 5, 5, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Festival.DAL.Entities.StageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5a2c4194-5c8f-4caf-ba40-0bc93752e4f6"),
                            Description = "Stage s výhledem",
                            ImgUrl = "https://c8.alamy.com/comp/BWHHRB/view-from-the-top-of-the-hill-at-the-top-of-the-park-stage-at-glastonbury-BWHHRB.jpg",
                            Name = "Stage na kopcu"
                        },
                        new
                        {
                            Id = new Guid("4d038276-18ef-461c-8783-d8521f6252db"),
                            Description = "Stage se záchodem",
                            ImgUrl = "https://shirokuma.blob.core.windows.net/osc/images-1/20140821solti-andras-a-toitoi-ceg.jpg",
                            Name = "Stage na hajzlu"
                        },
                        new
                        {
                            Id = new Guid("f079d174-dadb-4128-a5a8-d84e7978de81"),
                            Description = "Stage2",
                            ImgUrl = "https://shirokuma.blob.core.windows.net/osc/images-1/20140821solti-andras-a-toitoi-ceg.jpg",
                            Name = "Stage2"
                        });
                });

            modelBuilder.Entity("Festival.DAL.Entities.EventEntity", b =>
                {
                    b.HasOne("Festival.DAL.Entities.BandEntity", "Band")
                        .WithMany("Events")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Festival.DAL.Entities.StageEntity", "Stage")
                        .WithMany("Events")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Band");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Festival.DAL.Entities.BandEntity", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Festival.DAL.Entities.StageEntity", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
