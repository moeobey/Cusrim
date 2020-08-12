namespace Cusrim.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        JobFunction = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        ContactNo = c.String(),
                        Website = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        StaffNo = c.String(),
                        Department = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        StudentCount = c.Long(nullable: false),
                        UserId = c.Long(),
                        profile_status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.Long(nullable: false),
                        password = c.String(),
                        UserRole = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        StudentId = c.Long(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        MatricNo = c.String(),
                        Department = c.String(),
                        Email = c.String(),
                        Level = c.String(),
                        Grade = c.String(),
                        UserId = c.Long(),
                        FacultyId = c.Long(),
                        profile_status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FacultyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "UserId", "dbo.Users");
            DropForeignKey("dbo.Students", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Faculties", "UserId", "dbo.Users");
            DropIndex("dbo.Students", new[] { "FacultyId" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.Reports", new[] { "StudentId" });
            DropIndex("dbo.Faculties", new[] { "UserId" });
            DropTable("dbo.Students");
            DropTable("dbo.Reports");
            DropTable("dbo.Users");
            DropTable("dbo.Faculties");
            DropTable("dbo.Companies");
        }
    }
}
