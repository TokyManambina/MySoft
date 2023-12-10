﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftSignAPI.Context;

#nullable disable

namespace SoftSignAPI.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20231207175912_changepfield")]
    partial class changepfield
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SoftSignAPI.Model.Attachement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("DocumentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentCode");

                    b.ToTable("Attachements");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Document", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocPasword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Object")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DocumentDynamicField", b =>
                {
                    b.Property<string>("DocumentCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("DocumentDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentCode", "DocumentDetailId");

                    b.HasIndex("DocumentDetailId");

                    b.ToTable("DocumentDynamicFields");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DocumentLink", b =>
                {
                    b.Property<string>("CodeLink")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CodeLink");

                    b.HasIndex("CodeDocument");

                    b.ToTable("DocumentLinks");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DynamicField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<bool>("isRequired")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("DocumentDetails");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DynamicFieldItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DetailId");

                    b.ToTable("DocumentDetailItems");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FieldType")
                        .HasColumnType("int");

                    b.Property<string>("FirstPage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<string>("LastPage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("PDF_Height")
                        .HasColumnType("float");

                    b.Property<double?>("PDF_Width")
                        .HasColumnType("float");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserDocumentId")
                        .HasColumnType("int");

                    b.Property<string>("Variable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Width")
                        .HasColumnType("float");

                    b.Property<double?>("X")
                        .HasColumnType("float");

                    b.Property<double?>("Y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserDocumentId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Flow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Historique", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<string>("Colonne")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Table")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Historiques");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Hour")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Society", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Storage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Societies");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasClientSpace")
                        .HasColumnType("bit");

                    b.Property<bool>("HasDynamicFieldManager")
                        .HasColumnType("bit");

                    b.Property<bool>("HasFlow")
                        .HasColumnType("bit");

                    b.Property<bool>("HasFlowManager")
                        .HasColumnType("bit");

                    b.Property<bool>("HasLibrary")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPhysicalLibrary")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxUser")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("SoftSignAPI.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid?>("SocietyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransfertMail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("SocietyId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SoftSignAPI.Model.UserDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MyTurn")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DocumentCode");

                    b.HasIndex("UserId");

                    b.ToTable("UserDocuments");
                });

            modelBuilder.Entity("SoftSignAPI.Model.UserFlow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<Guid>("FlowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlowId");

                    b.ToTable("UserFlows");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Attachement", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Document", "Document")
                        .WithMany("Attachements")
                        .HasForeignKey("DocumentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DocumentDynamicField", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Document", "Document")
                        .WithMany("DocumentDetailValues")
                        .HasForeignKey("DocumentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftSignAPI.Model.DynamicField", "DocumentDetail")
                        .WithMany("Values")
                        .HasForeignKey("DocumentDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("DocumentDetail");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DocumentLink", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Document", "Document")
                        .WithMany("DocumentLinks")
                        .HasForeignKey("CodeDocument")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DynamicField", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Subscription", "Subscription")
                        .WithMany("DynamicFields")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DynamicFieldItem", b =>
                {
                    b.HasOne("SoftSignAPI.Model.DynamicField", "Detail")
                        .WithMany("Items")
                        .HasForeignKey("DetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Detail");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Field", b =>
                {
                    b.HasOne("SoftSignAPI.Model.UserDocument", "UserDocument")
                        .WithMany("Fields")
                        .HasForeignKey("UserDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDocument");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Flow", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Subscription", null)
                        .WithMany("Flows")
                        .HasForeignKey("SubscriptionId");
                });

            modelBuilder.Entity("SoftSignAPI.Model.User", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Society", "Society")
                        .WithMany("Users")
                        .HasForeignKey("SocietyId");

                    b.HasOne("SoftSignAPI.Model.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId");

                    b.Navigation("Society");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("SoftSignAPI.Model.UserDocument", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Document", "Document")
                        .WithMany("UserDocuments")
                        .HasForeignKey("DocumentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftSignAPI.Model.User", "User")
                        .WithMany("UserDocuments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoftSignAPI.Model.UserFlow", b =>
                {
                    b.HasOne("SoftSignAPI.Model.Flow", "Flow")
                        .WithMany("Users")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Document", b =>
                {
                    b.Navigation("Attachements");

                    b.Navigation("DocumentDetailValues");

                    b.Navigation("DocumentLinks");

                    b.Navigation("UserDocuments");
                });

            modelBuilder.Entity("SoftSignAPI.Model.DynamicField", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Values");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Flow", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Society", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SoftSignAPI.Model.Subscription", b =>
                {
                    b.Navigation("DynamicFields");

                    b.Navigation("Flows");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SoftSignAPI.Model.User", b =>
                {
                    b.Navigation("UserDocuments");
                });

            modelBuilder.Entity("SoftSignAPI.Model.UserDocument", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}
