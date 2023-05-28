﻿// <auto-generated />
using System;
using Confab.Modules.Tickets.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Confab.Modules.Tickets.Core.DAL.Migrations
{
    [DbContext(typeof(TicketsDbContext))]
    [Migration("20230528043048_Tickets_Module_Init")]
    partial class Tickets_Module_Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("tickets")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Conference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("From")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ParticipantsLimit")
                        .HasColumnType("integer");

                    b.Property<DateTime>("To")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Conferences", "tickets");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<Guid>("ConferenceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("PurchasedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TicketSaleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UsedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ConferenceId");

                    b.HasIndex("TicketSaleId");

                    b.ToTable("Tickets", "tickets");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.TicketSale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ConferenceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("From")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("To")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.ToTable("TicketSales", "tickets");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Ticket", b =>
                {
                    b.HasOne("Confab.Modules.Tickets.Core.Entities.Conference", "Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Confab.Modules.Tickets.Core.Entities.TicketSale", "TicketSale")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conference");

                    b.Navigation("TicketSale");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.TicketSale", b =>
                {
                    b.HasOne("Confab.Modules.Tickets.Core.Entities.Conference", null)
                        .WithMany("TicketSales")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Conference", b =>
                {
                    b.Navigation("TicketSales");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.TicketSale", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
