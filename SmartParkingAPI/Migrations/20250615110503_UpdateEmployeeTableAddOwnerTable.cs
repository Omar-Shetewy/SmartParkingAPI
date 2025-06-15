using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeTableAddOwnerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT * FROM sys.foreign_keys 
                WHERE name = 'FK_Employees_Garages_GarageId'
            )
            BEGIN
                ALTER TABLE [Employees] DROP CONSTRAINT [FK_Employees_Garages_GarageId];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT name 
                FROM sys.indexes 
                WHERE name = 'IX_Employees_GarageId' AND object_id = OBJECT_ID('Employees')
            )
            BEGIN
                DROP INDEX [IX_Employees_GarageId] ON [Employees];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT * FROM sys.columns 
                WHERE Name = N'Address' 
                AND Object_ID = Object_ID(N'Employees')
            )
            BEGIN
                ALTER TABLE [Employees] DROP COLUMN [Address];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT * FROM sys.columns 
                WHERE Name = N'Email' 
                AND Object_ID = Object_ID(N'Employees')
            )
            BEGIN
                ALTER TABLE [Employees] DROP COLUMN [Email];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'FirstName' AND Object_ID = Object_ID(N'Employees'))
            BEGIN
                ALTER TABLE [Employees] DROP COLUMN [FirstName];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'Gender' AND Object_ID = Object_ID(N'Employees'))
            BEGIN
                ALTER TABLE [Employees] DROP COLUMN [Gender];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'LastName' AND Object_ID = Object_ID(N'Employees'))
            BEGIN
                ALTER TABLE [Employees] DROP COLUMN [LastName];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'PhoneNumber' AND Object_ID = Object_ID(N'Employees'))
            BEGIN
                ALTER TABLE [Employees] DROP COLUMN [PhoneNumber];
            END
            ");

            migrationBuilder.Sql(@"
            IF EXISTS (
                SELECT * FROM sys.columns 
                WHERE Name = N'GarageId' AND Object_ID = Object_ID(N'Employees')
            )
            BEGIN
                EXEC sp_rename N'Employees.GarageId', N'UserId', 'COLUMN';
            END
            ");

            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.columns 
                WHERE Name = N'OwnerId' AND Object_ID = Object_ID(N'Garages')
            )
            BEGIN
                ALTER TABLE [Garages] ADD [OwnerId] int NULL;
            END
            ");

            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'Owners'
            )
            BEGIN
                CREATE TABLE [Owners] (
                    [OwnerId] int NOT NULL IDENTITY(1,1),
                    [UserId] int NOT NULL,
                    CONSTRAINT [PK_Owners] PRIMARY KEY ([OwnerId]),
                    CONSTRAINT [FK_Owners_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]) ON DELETE CASCADE
                )
            END
            ");

            // Create Index IX_Garages_OwnerId if not exists
            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT name FROM sys.indexes
                WHERE name = 'IX_Garages_OwnerId' AND object_id = OBJECT_ID('Garages')
            )
            BEGIN
                CREATE INDEX [IX_Garages_OwnerId] ON [Garages]([OwnerId])
            END
            ");

            // Create Index IX_Employees_UserId if not exists
            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT name FROM sys.indexes
                WHERE name = 'IX_Employees_UserId' AND object_id = OBJECT_ID('Employees')
            )
            BEGIN
                CREATE UNIQUE INDEX [IX_Employees_UserId] ON [Employees]([UserId])
            END
            ");

            // Create Index IX_Owners_UserId if not exists
            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT name FROM sys.indexes
                WHERE name = 'IX_Owners_UserId' AND object_id = OBJECT_ID('Owners')
            )
            BEGIN
                CREATE INDEX [IX_Owners_UserId] ON [Owners]([UserId])
            END
            ");

            // Add FK FK_Employees_Users_UserId if not exists
            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.foreign_keys WHERE name = 'FK_Employees_Users_UserId'
            )
            BEGIN
                ALTER TABLE [Employees] ADD CONSTRAINT [FK_Employees_Users_UserId]
                FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]) ON DELETE CASCADE
            END
            ");

            // Add FK FK_Garages_Owners_OwnerId if not exists
            migrationBuilder.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.foreign_keys WHERE name = 'FK_Garages_Owners_OwnerId'
            )
            BEGIN
                ALTER TABLE [Garages] ADD CONSTRAINT [FK_Garages_Owners_OwnerId]
                FOREIGN KEY ([OwnerId]) REFERENCES [Owners]([OwnerId])
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Garages_Owners_OwnerId",
                table: "Garages");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Garages_OwnerId",
                table: "Garages");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Garages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employees",
                newName: "GarageId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GarageId",
                table: "Employees",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Garages_GarageId",
                table: "Employees",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "GarageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
