namespace BigShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        RoleID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupID, t.RoleID })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.GroupID })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.GroupID);
            
            AddColumn("dbo.ApplicationRoles", "Description", c => c.String(maxLength: 250));
            AddColumn("dbo.ApplicationRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserGroups", "UserID", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupID", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleID", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupID", "dbo.ApplicationGroups");
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupID" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserID" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleID" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupID" });
            DropColumn("dbo.ApplicationRoles", "Discriminator");
            DropColumn("dbo.ApplicationRoles", "Description");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
