namespace UniCloud.Infrastructure.Data.UberModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            AddColumn("FRP.ContractAircraft", "Status", c => c.Int(nullable: false));
            AddColumn("FRP.ContractEngine", "Status", c => c.Int(nullable: false));
            AddColumn("FRP.MaintainInvoice", "DocumentName", c => c.String());
            AddColumn("FRP.MaintainInvoice", "DocumentId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("FRP.MaintainInvoice", "DocumentId");
            DropColumn("FRP.MaintainInvoice", "DocumentName");
            DropColumn("FRP.ContractEngine", "Status");
            DropColumn("FRP.ContractAircraft", "Status");
        }
    }
}
