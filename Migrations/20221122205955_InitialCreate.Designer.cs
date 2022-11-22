﻿// <auto-generated />
using System;
using Maal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Maal.Migrations
{
    [DbContext(typeof(MaalContext))]
    [Migration("20221122205955_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Maal.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GameSessionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("GameTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Round")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameSessionId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Maal.Models.GamePlayer", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Dubli")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Fouled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Maal")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Won")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GamePlayer");
                });

            modelBuilder.Entity("Maal.Models.GameSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("GameSessionTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("Maal.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Maal.Models.Game", b =>
                {
                    b.HasOne("Maal.Models.GameSession", null)
                        .WithMany("Games")
                        .HasForeignKey("GameSessionId");
                });

            modelBuilder.Entity("Maal.Models.GamePlayer", b =>
                {
                    b.HasOne("Maal.Models.Game", "Game")
                        .WithMany("GamePlayers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Maal.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Maal.Models.Game", b =>
                {
                    b.Navigation("GamePlayers");
                });

            modelBuilder.Entity("Maal.Models.GameSession", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
