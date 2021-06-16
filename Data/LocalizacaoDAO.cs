using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SiMPAC.Models;

namespace SiMPAC.Data
{
    public class LocalizacaoDAO : IDataAccessObject<Localizacao>
    {
        private IConnection conn;
        public LocalizacaoDAO(IConnection c)
        {
            conn = c;
        }

        public Localizacao search(int id)
        {
            Localizacao p = new Localizacao();
            using (SqlConnection sconn = conn.Fetch())
            {
                string query = "SELECT * FROM dbo.Localizacao where id=@id";
                var dt = new DataTable();
                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        p.Id = int.Parse(dr["id"].ToString());
                        p.Cidade = dr["cidade"].ToString();
                        p.Rua = dr["rua"].ToString();
                        p.CodPostal = dr["codpostal"].ToString();
                        p.Porta = int.Parse(dr["porta"].ToString());
                        p.Distrito = dr["distrito"].ToString();

                    }
                    reader.Close();
                }
                return p;
            }
        }
        public Localizacao insert(Localizacao obj)
        {
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "INSERT INTO dbo.Localizacao(id,cidade,rua,codpostal,porta,distrito) values (@Id,@Cidade,@Rua,@CodPostal,@Porta,@Distrito)";

                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                    command.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = obj.Cidade ?? (object)DBNull.Value;
                    command.Parameters.Add("@Rua", SqlDbType.VarChar).Value = obj.Rua;
                    command.Parameters.Add("@CodPostal", SqlDbType.VarChar).Value = obj.CodPostal;
                    command.Parameters.Add("@Porta", SqlDbType.Int).Value = obj.Porta;
                    command.Parameters.Add("@Distrito", SqlDbType.VarChar).Value = obj.Distrito;

                    command.ExecuteNonQuery();

                }
            }
            return obj;
        }
        public List<Localizacao> getAll()
        {
            List<Localizacao> localizacoes = new List<Localizacao>();
            using (SqlConnection sconn = conn.Fetch())
            {
                string queryString = "SELECT * FROM dbo.Localizacao";

                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand(queryString, sconn);
                    DataSet tab = new DataSet();
                    adapter.Fill(tab);

                    foreach (DataTable table in tab.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Localizacao a = new Localizacao()
                            {
                                Id = int.Parse(row["id"].ToString()),
                                Cidade = row["cidade"].ToString(),
                                Rua = row["Rua"].ToString(),
                                CodPostal = row["codpostal"].ToString(),
                                Porta = int.Parse(row["portal"].ToString()),
                                Distrito = row["distrito"].ToString()
                            };
                            localizacoes.Add(a);
                        }
                    }
                }
                return localizacoes;
            }
        }

        public bool remove(int key)
        {
            bool removed = false;
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "DELETE FROM dbo.Localizacao where id=@Id";

                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.AddWithValue("@Id", key);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        removed = true;
                    }
                }
            }
            return removed;
        }

        public bool update(int key, Localizacao obj)
        {
            bool updated = false;
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "UPDATE dbo.Localizacao SET cidade=@Cidade, rua=@Rua, codpostal=@CodPostal, porta=@Porta, distrito=@Distrito";


                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.AddWithValue("@Cidade", obj.Cidade);
                    command.Parameters.AddWithValue("@Rua", obj.Rua);
                    command.Parameters.AddWithValue("@CodPostal", obj.CodPostal);
                    command.Parameters.AddWithValue("@Porta", obj.Porta);
                    command.Parameters.AddWithValue("@Distrito", obj.Distrito);


                    if (command.ExecuteNonQuery() > 0)
                    {
                        updated = true;
                    }
                }
            }
            return updated;
        }
    }
}
