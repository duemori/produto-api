namespace Produto.Domain.Entities
{
    public class ProdutoEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Imagem { get; set; }
    }
}
