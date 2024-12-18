﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QueensPuzle.Web.Data;

#nullable disable

namespace NQueensPuzzle.Web.Migrations
{
    [DbContext(typeof(ResultContext))]
    [Migration("20241118092042_Migration2")]
    partial class Migration2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QueensPuzzle.Application.Models.SolutionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ElapsedTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FundamentalSolutionsCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalSolutionsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SolutionResults");
                });
#pragma warning restore 612, 618
        }
    }
}
