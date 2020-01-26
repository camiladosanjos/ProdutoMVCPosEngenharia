using System;
using Microsoft.AspNetCore.Mvc;
using ProdutoMVC.Models;

namespace ProdutoMVC.Controllers
{
    public class ProdutoController : Controller
    {
        ProdutoMVC.ProdutoRepository.ProdutoRepository repository = new ProdutoMVC.ProdutoRepository.ProdutoRepository();

        // GET: Produto
        public ActionResult Index()
        {
            var produtos = repository.ListarProdutos();
            return View(produtos);
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            var produto = repository.DetalharProduto(id);

            if (produto == null)
            {
                StatusCode(404);
            }

            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.CriarProdutos(produto);
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            var produto = repository.DetalharProduto(id);

            if (produto == null)
            {
                StatusCode(404);
            }

            return View(produto);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                repository.AtualizarProduto(produto);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            var produto = repository.DetalharProduto(id);

            if (produto == null)
            {
                StatusCode(404);
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Produto produto)
        {
            repository.ExcluirProduto(produto.Id);
            return RedirectToAction("Index");
        }
    }
}