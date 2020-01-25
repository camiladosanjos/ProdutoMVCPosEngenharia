using System.ComponentModel.DataAnnotations;

namespace ProdutoMVC.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Fabricante { get; set; }

        [Display(Name = "Código de Barras")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string CodigoDeBarras { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Estoque { get; set; }
    }
}
