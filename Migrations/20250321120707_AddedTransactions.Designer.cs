﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250321120707_AddedTransactions")]
    partial class AddedTransactions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OpeningDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AccountsCards", b =>
                {
                    b.Property<int>("AccountsId")
                        .HasColumnType("int");

                    b.Property<int>("CardsId")
                        .HasColumnType("int");

                    b.HasKey("AccountsId", "CardsId");

                    b.HasIndex("CardsId");

                    b.ToTable("AccountsCards");
                });

            modelBuilder.Entity("Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("FromAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("ToAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Account", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AccountsCards", b =>
                {
                    b.HasOne("Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Card", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Operation", b =>
                {
                    b.HasOne("Account", "Account")
                        .WithMany("Operations")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Transaction", b =>
                {
                    b.HasOne("Account", "FromAccount")
                        .WithMany("FromTransactions")
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Account", "ToAccount")
                        .WithMany("ToTransactions")
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("Account", b =>
                {
                    b.Navigation("FromTransactions");

                    b.Navigation("Operations");

                    b.Navigation("ToTransactions");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
