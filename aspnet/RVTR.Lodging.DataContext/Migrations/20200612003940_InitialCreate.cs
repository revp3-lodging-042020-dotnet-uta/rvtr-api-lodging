using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RVTR.Lodging.DataContext.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    StateProvince = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressModel_LocationModel_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lodgings_LocationModel_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    LodgingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Lodgings_LodgingId",
                        column: x => x.LodgingId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    LodgingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Lodgings_LodgingId",
                        column: x => x.LodgingId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentalUnitModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Occupancy = table.Column<int>(nullable: false),
                    RentalUnitType = table.Column<string>(nullable: true),
                    RentalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalUnitModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalUnitModel_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BathroomModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fixture = table.Column<double>(nullable: false),
                    RentalUnitModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BathroomModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BathroomModel_RentalUnitModel_RentalUnitModelId",
                        column: x => x.RentalUnitModelId,
                        principalTable: "RentalUnitModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BedroomModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BedType = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    RentalUnitModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedroomModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BedroomModel_RentalUnitModel_RentalUnitModelId",
                        column: x => x.RentalUnitModelId,
                        principalTable: "RentalUnitModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(nullable: true),
                    RentalUnitModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageModel_RentalUnitModel_RentalUnitModelId",
                        column: x => x.RentalUnitModelId,
                        principalTable: "RentalUnitModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressModel_LocationId",
                table: "AddressModel",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BathroomModel_RentalUnitModelId",
                table: "BathroomModel",
                column: "RentalUnitModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BedroomModel_RentalUnitModelId",
                table: "BedroomModel",
                column: "RentalUnitModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModel_RentalUnitModelId",
                table: "ImageModel",
                column: "RentalUnitModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_LocationId",
                table: "Lodgings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_LodgingId",
                table: "Rentals",
                column: "LodgingId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalUnitModel_RentalId",
                table: "RentalUnitModel",
                column: "RentalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_LodgingId",
                table: "Reviews",
                column: "LodgingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressModel");

            migrationBuilder.DropTable(
                name: "BathroomModel");

            migrationBuilder.DropTable(
                name: "BedroomModel");

            migrationBuilder.DropTable(
                name: "ImageModel");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RentalUnitModel");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Lodgings");

            migrationBuilder.DropTable(
                name: "LocationModel");
        }
    }
}
