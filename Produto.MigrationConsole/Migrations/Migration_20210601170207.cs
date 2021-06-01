using FluentMigrator;

namespace Produto.MigrationConsole.Migrations
{
    [Migration(20210601170207)]
    public class Migration_20210601170207 : Migration
    {
        public override void Down()
        {
            Delete.Table("Produto");
        }

        public override void Up()
        {
            Create.Table("Produto")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsCustom("VARCHAR(100)").NotNullable()
                .WithColumn("ValorVenda").AsDecimal(9, 2).NotNullable()
                .WithColumn("Imagem").AsCustom("VARCHAR(MAX)").Nullable();
        }
    }
}
