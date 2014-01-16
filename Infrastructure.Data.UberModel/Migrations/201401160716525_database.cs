namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("FRP.AirProgrammingLine", "AcTypeId", "FRP.AircraftSeries");
            DropForeignKey("FRP.AirProgrammingLine", "AircraftCategoryId", "FRP.AircraftCategory");
            DropIndex("FRP.AirProgrammingLine", new[] { "AcTypeId" });
            DropIndex("FRP.AirProgrammingLine", new[] { "AircraftCategoryId" });
            AddColumn("FRP.AircraftLicense", "FileName", c => c.String());
            AddColumn("FRP.AirProgrammingLine", "AircraftSeriesId", c => c.Guid(nullable: false));
            AddColumn("FRP.PlanHistory", "ApprovalHistoryId", c => c.Guid());
            CreateIndex("FRP.AirProgrammingLine", "AircraftSeriesId");
            AddForeignKey("FRP.AirProgrammingLine", "AircraftSeriesId", "FRP.AircraftSeries", "ID");
            DropColumn("FRP.AirProgrammingLine", "AcTypeId");
            DropColumn("FRP.AirProgrammingLine", "AircraftCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("FRP.AirProgrammingLine", "AircraftCategoryId", c => c.Guid(nullable: false));
            AddColumn("FRP.AirProgrammingLine", "AcTypeId", c => c.Guid(nullable: false));
            DropForeignKey("FRP.AirProgrammingLine", "AircraftSeriesId", "FRP.AircraftSeries");
            DropIndex("FRP.AirProgrammingLine", new[] { "AircraftSeriesId" });
            DropColumn("FRP.PlanHistory", "ApprovalHistoryId");
            DropColumn("FRP.AirProgrammingLine", "AircraftSeriesId");
            DropColumn("FRP.AircraftLicense", "FileName");
            CreateIndex("FRP.AirProgrammingLine", "AircraftCategoryId");
            CreateIndex("FRP.AirProgrammingLine", "AcTypeId");
            AddForeignKey("FRP.AirProgrammingLine", "AircraftCategoryId", "FRP.AircraftCategory", "ID");
            AddForeignKey("FRP.AirProgrammingLine", "AcTypeId", "FRP.AircraftSeries", "ID");
        }
    }
}
