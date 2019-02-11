namespace MovieStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNotNullForUserInMovie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Movies", new[] { "UserProfileId" });
            AlterColumn("dbo.Movies", "UserProfileId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Movies", "UserProfileId");
            AddForeignKey("dbo.Movies", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Movies", new[] { "UserProfileId" });
            AlterColumn("dbo.Movies", "UserProfileId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Movies", "UserProfileId");
            AddForeignKey("dbo.Movies", "UserProfileId", "dbo.UserProfiles", "Id");
        }
    }
}
