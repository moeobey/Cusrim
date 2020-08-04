namespace Cusrim.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Name", c => c.String());
            AddColumn("dbo.Reports", "FacultyId", c => c.Long());
            CreateIndex("dbo.Reports", "FacultyId");
            AddForeignKey("dbo.Reports", "FacultyId", "dbo.Faculties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Reports", new[] { "FacultyId" });
            DropColumn("dbo.Reports", "FacultyId");
            DropColumn("dbo.Reports", "Name");
        }
    }
}
