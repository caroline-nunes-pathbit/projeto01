﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeText : Migration
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

            migrationBuilder.Sql(
                "UPDATE \"Users\" SET \"UserTypeTemp\" = " +
                "CASE " +
                "WHEN \"UserType\" = '0' THEN 'Cliente' " +
                "WHEN \"UserType\" = '1' THEN 'Administrador' " +
                "END"

            );

            migrationBuilder.DropColumn(name: "UserType", table: "Users");

            migrationBuilder.RenameColumn(name: "UserTypeTemp", table: "Users", newName: "UserType");
        }
    }
}
