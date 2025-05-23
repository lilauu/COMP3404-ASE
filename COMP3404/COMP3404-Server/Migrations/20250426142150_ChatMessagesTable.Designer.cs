﻿// <auto-generated />
using System;
using COMP3404_Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace COMP3404_Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250426142150_ChatMessagesTable")]
    partial class ChatMessagesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ChatMessageIds")
                .StartsAt(0L);

            modelBuilder.Entity("COMP3404_Shared.Models.Accounts.UserAccount", b =>
                {
                    b.Property<int>("UserAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAccountId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GithubAccountId")
                        .HasColumnType("int");

                    b.Property<string>("GithubToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserAccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("COMP3404_Shared.Models.Chats.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatId"));

                    b.Property<string>("ChatName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "chatName");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("ChatId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("COMP3404_Shared.Models.Chats.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR ChatMessageIds");

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHumanSender")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("COMP3404_Shared.Models.Chats.Chat", b =>
                {
                    b.HasOne("COMP3404_Shared.Models.Accounts.UserAccount", "OwnerInfo")
                        .WithMany("Chats")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerInfo");
                });

            modelBuilder.Entity("COMP3404_Shared.Models.Chats.ChatMessage", b =>
                {
                    b.HasOne("COMP3404_Shared.Models.Chats.Chat", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("COMP3404_Shared.Models.Accounts.UserAccount", b =>
                {
                    b.Navigation("Chats");
                });

            modelBuilder.Entity("COMP3404_Shared.Models.Chats.Chat", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
