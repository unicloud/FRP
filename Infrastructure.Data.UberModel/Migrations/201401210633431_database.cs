namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FRP.DocumentType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("FRP.AircraftLicense", "FileContent", c => c.Binary());
            AddColumn("FRP.AircraftLicense", "FileName", c => c.String());
            AddColumn("FRP.EnginePlanHistory", "ImportDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("FRP.EnginePlanHistory", "Note", c => c.String());
            DropColumn("FRP.AircraftLicense", "LicenseFile");
            DropColumn("FRP.EnginePlan", "IsFinished");
        }
        
        public override void Down()
        {
            AddColumn("FRP.EnginePlan", "IsFinished", c => c.Boolean(nullable: false));
            AddColumn("FRP.AircraftLicense", "LicenseFile", c => c.Binary());
            DropColumn("FRP.EnginePlanHistory", "Note");
            DropColumn("FRP.EnginePlanHistory", "ImportDate");
            DropColumn("FRP.AircraftLicense", "FileName");
            DropColumn("FRP.AircraftLicense", "FileContent");
            DropTable("FRP.DocumentType");
        }
    }
}
