using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "UserTypeTemp",
                table: "Users",
                type: "text",
                nullable: true
            );
            migrationBuilder.Sql("UPDATE \"Users\" SET \"UserTypeTemp\" = " +
            "CASE CAST(\"UserType\" AS INTEGER)" +
            "WHEN 0 THEN 'Cliente'" +
            "WHEN 1 THEN 'Administrador'" +
            "ELSE 'Unknown' END" );

            migrationBuilder.DropColumn(name: "UserType", table: "Users");
            migrationBuilder.RenameColumn(name: "UserTypeTemp", table: "Users", newName: "UserType");
        }
    }
}
