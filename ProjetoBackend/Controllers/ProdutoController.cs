using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoBackend.Models;

namespace ProjetoBackend.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();
            return View(produtos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult DeletarConfirmar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ProdutoModel produto)
        {
            if (ModelState.IsValid)
            {
                string filePath = ("../Arquivos/produtos.txt");
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"Nome: {produto.Nome} - Categoria: {produto.Categoria} - Preço: {produto.Preco}");
                }

                return RedirectToAction("Index");
            }

            return View(produto);
        }

        [HttpGet]
        public IActionResult ListarProdutos(ProdutoModel produto)
        {   
            // Caminho do arquivo de texto
            string filePath = ("../Arquivos/produtos.txt");
            // Verifique se o arquivo existe
            if (!System.IO.File.Exists(filePath))   
            {
                // Se o arquivo não existir, retorne uma lista vazia
                var produtosVazios = new List<ProdutoModel>();
                return View(produtosVazios);
            }

            // Ler as linhas do arquivo e criar uma lista de produtos
            var linhas = System.IO.File.ReadAllLines(filePath);
            var produtos = new List<ProdutoModel>();

            foreach (var linha in linhas)
            {
                // Dividir a linha em partes usando o caractere '-' como delimitador
                var partes = linha.Split('-');
                if (partes.Length == 3)
                {
                    var nomePartes = partes[0].Split(':');
                    var categoriaPartes = partes[1].Split(':');
                    var precoPartes = partes[2].Split(':');

                    if (nomePartes.Length == 2 && categoriaPartes.Length == 2 && precoPartes.Length == 2)
                    {
                        var produtoNew = new ProdutoModel
                        {
                            Nome = nomePartes[1].Trim(),
                            Categoria = categoriaPartes[1].Trim(),
                            Preco = decimal.Parse(precoPartes[1].Trim())
                        };
                        produtos.Add(produtoNew);
                    }
                }
            }

            return View(produtos);
        }
    }
}
