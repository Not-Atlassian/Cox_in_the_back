using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class dbcreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370FA0824176", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    team_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    team_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    team_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    team_lead_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teams__F82DEDBCAAB0C3AF", x => x.team_id);
                    table.ForeignKey(
                        name: "FK__Teams__team_lead__60A75C0F",
                        column: x => x.team_lead_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    sprint_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    goal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "planned"),
                    team_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sprints__396C1802EFA36E05", x => x.sprint_id);
                    table.ForeignKey(
                        name: "FK__Sprints__team_id__693CA210",
                        column: x => x.team_id,
                        principalTable: "Teams",
                        principalColumn: "team_id");
                });

            migrationBuilder.CreateTable(
                name: "UsersTeams",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    team_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UsersTea__663CE9D4131E7ED3", x => new { x.user_id, x.team_id });
                    table.ForeignKey(
                        name: "FK__UsersTeam__team___6477ECF3",
                        column: x => x.team_id,
                        principalTable: "Teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UsersTeam__user___6383C8BA",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adjustments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    sprint_id = table.Column<int>(type: "int", nullable: false),
                    adjustment_points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adjustme__3214EC07B3BFDA02", x => x.id);
                    table.ForeignKey(
                        name: "FK__Adjustmen__sprin__6EC0713C",
                        column: x => x.sprint_id,
                        principalTable: "Sprints",
                        principalColumn: "sprint_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    story_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, defaultValue: "open"),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    sprint_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    story_points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stories__66339C56B86254B8", x => x.story_id);
                    table.ForeignKey(
                        name: "FK__Stories__created__6FE99F9F",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Stories__parent___70DDC3D8",
                        column: x => x.parent_id,
                        principalTable: "Stories",
                        principalColumn: "story_id");
                    table.ForeignKey(
                        name: "FK__Stories__sprint___6EF57B66",
                        column: x => x.sprint_id,
                        principalTable: "Sprints",
                        principalColumn: "sprint_id");
                });

            migrationBuilder.CreateTable(
                name: "TeamMemberAvailabilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    sprint_id = table.Column<int>(type: "int", nullable: false),
                    availability_points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TeamMemb__3214EC07A9A5EE06", x => x.id);
                    table.ForeignKey(
                        name: "FK__TeamMembe__sprin__6DCC4D03",
                        column: x => x.sprint_id,
                        principalTable: "Sprints",
                        principalColumn: "sprint_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersStories",
                columns: table => new
                {
                    story_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UsersSto__8DA87F2683494916", x => new { x.story_id, x.user_id });
                    table.ForeignKey(
                        name: "FK__UsersStor__story__73BA3083",
                        column: x => x.story_id,
                        principalTable: "Stories",
                        principalColumn: "story_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UsersStor__user___74AE54BC",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_sprint_id",
                table: "Adjustments",
                column: "sprint_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_team_id",
                table: "Sprints",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_created_by",
                table: "Stories",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_parent_id",
                table: "Stories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_sprint_id",
                table: "Stories",
                column: "sprint_id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberAvailabilities_sprint_id",
                table: "TeamMemberAvailabilities",
                column: "sprint_id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_team_lead_id",
                table: "Teams",
                column: "team_lead_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Teams__29E35E0C9F1894C7",
                table: "Teams",
                column: "team_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__F3DBC572843AEA6F",
                table: "Users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersStories_user_id",
                table: "UsersStories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTeams_team_id",
                table: "UsersTeams",
                column: "team_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjustments");

            migrationBuilder.DropTable(
                name: "TeamMemberAvailabilities");

            migrationBuilder.DropTable(
                name: "UsersStories");

            migrationBuilder.DropTable(
                name: "UsersTeams");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
