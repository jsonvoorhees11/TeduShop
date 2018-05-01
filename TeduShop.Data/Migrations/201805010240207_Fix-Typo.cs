namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Pages", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "MetaDescription", c => c.String(maxLength: 256));
            DropColumn("dbo.Products", "MetaDecription");
            DropColumn("dbo.ProductCategories", "MetaDecription");
            DropColumn("dbo.Pages", "MetaDecription");
            DropColumn("dbo.PostCategories", "MetaDecription");
            DropColumn("dbo.Posts", "MetaDecription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "MetaDecription", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "MetaDecription", c => c.String(maxLength: 256));
            AddColumn("dbo.Pages", "MetaDecription", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "MetaDecription", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "MetaDecription", c => c.String(maxLength: 256));
            DropColumn("dbo.Posts", "MetaDescription");
            DropColumn("dbo.PostCategories", "MetaDescription");
            DropColumn("dbo.Pages", "MetaDescription");
            DropColumn("dbo.ProductCategories", "MetaDescription");
            DropColumn("dbo.Products", "MetaDescription");
        }
    }
}
