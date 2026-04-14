using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class moremodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "insurance_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    specialty = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    credentials = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    practice_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    scheduling_integrated = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providers", x => x.id);
                    table.ForeignKey(
                        name: "FK_providers_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsurancePlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patients_insurance_plans_InsurancePlanId",
                        column: x => x.InsurancePlanId,
                        principalTable: "insurance_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "insurance_providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurancePlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InNetwork = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_insurance_providers_insurance_plans_InsurancePlanId",
                        column: x => x.InsurancePlanId,
                        principalTable: "insurance_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insurance_providers_providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlotDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VisitReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DenialReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appointments_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_PatientId",
                table: "appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_ProviderId",
                table: "appointments",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_providers_InsurancePlanId",
                table: "insurance_providers",
                column: "InsurancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_providers_ProviderId_InsurancePlanId",
                table: "insurance_providers",
                columns: new[] { "ProviderId", "InsurancePlanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patients_InsurancePlanId",
                table: "patients",
                column: "InsurancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_UserId",
                table: "patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_providers_user_id",
                table: "providers",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "insurance_providers");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.DropTable(
                name: "insurance_plans");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
