namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactDetailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactDetails", "PhoneNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.ContactDetails", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.ContactDetails", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.ContactDetails", "Address", c => c.String(maxLength: 50));
            AlterColumn("dbo.ContactDetails", "Website", c => c.String(maxLength: 250));
            DropColumn("dbo.ContactDetails", "phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactDetails", "phone", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactDetails", "Website", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.ContactDetails", "Address", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.ContactDetails", "Email", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.ContactDetails", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.ContactDetails", "PhoneNumber");
        }
    }
}
