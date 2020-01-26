using ProdutoMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                var cmdTxt = 
                    "SELECT * FROM Produto";

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
                var cmdTxt = 
                    "INSERT INTO Produto (Nome, Fabricante, CodigoDeBarras, Preco, Estoque) " +
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

        public void AtualizarProduto(Produto produto)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string cmdTxt = 
                    "UPDATE Produto " +
                    "SET Nome=@Nome, Fabricante=@Fabricante, CodigoDeBarras=@CodigoDeBarras, Preco=@Preco, Estoque=@Estoque " +
                    "Where Id=@Id";

                var cmd = new SqlCommand(cmdTxt, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", produto.Id);
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

        public Produto DetalharProduto(int id)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var cmdTxt = 
                    "SELECT Id, Nome, Fabricante, CodigoDeBarras, Preco, Estoque " +
                    "FROM Produto " +
                    "Where Id = @Id";

                var cmd = new SqlCommand(cmdTxt, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                Produto produto = null;

                try
                {
                    connection.Open();

                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) 
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                produto = new Produto();
                                produto.Id = (int)reader["Id"];
                                produto.Nome = reader["Nome"].ToString();
                                produto.Fabricante = reader["Fabricante"].ToString();
                                produto.CodigoDeBarras = reader["CodigoDeBarras"].ToString();
                                produto.Preco = (decimal)reader["Preco"];
                                produto.Estoque = (int)reader["Estoque"];
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
                return produto;
            }
        }

        public void ExcluirProduto(int id)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var cmdTxt = "DELETE Produto Where Id=@Id";
                var cmd = new SqlCommand(cmdTxt, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

    }
}
