using ProdutoMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutoMVC.ProdutoRepository
{
    public class ProdutoRepository
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProdutoMVC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<Produto> ListarProdutos()
        {
            var produtos = new List<Produto>();

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdTxt = "SELECT * FROM Produto";
                var select = new SqlCommand(cmdTxt, connection);

                try
                {
                    connection.Open();
                    using (var reader = select.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto();
                            produto.Id = (int)reader["Id"];
                            produto.Nome = reader["Nome"].ToString();
                            produto.Fabricante = reader["Fabricante"].ToString();
                            produto.CodigoDeBarras = reader["CodigoDeBarras"].ToString();
                            produto.Preco = (decimal) reader["Preco"];
                            produto.Estoque = (int)reader["Estoque"];

                            produtos.Add(produto);
                        }
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
                finally
                {
                    connection.Close();
                }

                return produtos;
            }
        }

        public void CriarProdutos(Produto produto)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var cmdTxt = "INSERT INTO " +
                    "Produto (Nome, Fabricante, CodigoDeBarras, Preco, Estoque) " +
                    "VALUES (@Nome, @Fabricante, @CodigoDeBarras, @Preco, @Estoque)";

                var cmd = new SqlCommand(cmdTxt, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Fabricante", produto.Fabricante);
                cmd.Parameters.AddWithValue("@CodigoDeBarras", produto.CodigoDeBarras);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

    }
}
