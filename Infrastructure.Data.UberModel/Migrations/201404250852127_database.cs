namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            AddColumn("FRP.Document", "UpdateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("FRP.Request", "Note", c => c.String());
            DropColumn("FRP.Request", "RaDocNumber");
            DropColumn("FRP.Request", "SawsDocNumber");
            DropColumn("FRP.Request", "CaacNote");
            DropColumn("FRP.Request", "RaNote");
            DropColumn("FRP.Request", "SawsNote");
            DropColumn("FRP.Request", "RaDocumentName");
            DropColumn("FRP.Request", "SawsDocumentName");
            DropColumn("FRP.Request", "RaDocumentId");
            DropColumn("FRP.Request", "SawsDocumentId");
        }
        
        public override void Down()
        {
            AddColumn("FRP.Request", "SawsDocumentId", c => c.Guid());
            AddColumn("FRP.Request", "RaDocumentId", c => c.Guid());
            AddColumn("FRP.Request", "SawsDocumentName", c => c.String());
            AddColumn("FRP.Request", "RaDocumentName", c => c.String());
            AddColumn("FRP.Request", "SawsNote", c => c.String());
            AddColumn("FRP.Request", "RaNote", c => c.String());
            AddColumn("FRP.Request", "CaacNote", c => c.String());
            AddColumn("FRP.Request", "SawsDocNumber", c => c.String());
            AddColumn("FRP.Request", "RaDocNumber", c => c.String());
            DropColumn("FRP.Request", "Note");
            DropColumn("FRP.Document", "UpdateTime");
        }
    }
}
