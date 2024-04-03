using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5f2e42b4-9590-47e7-a306-59ff792db028"), "Medium" },
                    { new Guid("712094b9-0e2a-49d8-b5bd-264cf1eec80c"), "Easy" },
                    { new Guid("c3024467-6823-4aa8-94d3-2a3f23b18d1c"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("18c7da75-f609-4018-84d7-1e4a2a4b25d6"), "NTL", "Northland", null },
                    { new Guid("55b3e1cd-85ac-4e42-b486-dde8313ad0f9"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("5ff79a59-46e0-4d81-8c2a-a4d42e6e6b5e"), "STL", "Southland", null },
                    { new Guid("8691abf8-f8d3-4583-9545-d1e65ec15684"), "BOP", "Bay Of Plenty", null },
                    { new Guid("8fd9a57b-9688-4b89-8f65-3361f61a0454"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("c9aec110-4e49-45f7-a77e-85023f8d0f74"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5f2e42b4-9590-47e7-a306-59ff792db028"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("712094b9-0e2a-49d8-b5bd-264cf1eec80c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c3024467-6823-4aa8-94d3-2a3f23b18d1c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("18c7da75-f609-4018-84d7-1e4a2a4b25d6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("55b3e1cd-85ac-4e42-b486-dde8313ad0f9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5ff79a59-46e0-4d81-8c2a-a4d42e6e6b5e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8691abf8-f8d3-4583-9545-d1e65ec15684"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8fd9a57b-9688-4b89-8f65-3361f61a0454"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c9aec110-4e49-45f7-a77e-85023f8d0f74"));
        }
    }
}
