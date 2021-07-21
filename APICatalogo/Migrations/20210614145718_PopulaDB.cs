using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCatalogo.Migrations
{
    public partial class PopulaDB : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, ImagemURL) Values ('Ferrari','https://loocalizei.com.br/wp-content/uploads/2017/11/la-fi-hy-ferrari-anniversary-20170819-e1511781169560.jpg')");
           
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemURL, Estoque, DataCadastro, CategoriaId) Values('Cayane','Toyota', 30.000, 'https://quatrorodas.abril.com.br/wp-content/uploads/2020/06/qr-727-carro-compara-porsche-x-m5-01-e1591112619291.jpg?quality=70&strip=info',507,now(),(Select CategoriaId from Categorias where Nome='Ferrari'))");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

       
    }
}
