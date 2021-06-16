using System;
using System.Data.SqlClient;
using System.Data;
using SiMPAC.Models;
using SiMPAC.Data;
using System.Globalization;
using System.Collections.Generic;

namespace SiMPAC.Data
{
    public class UtilizadorDAO : IDataAccessObject<Utilizador>
    {
        private IConnection conn;
        public UtilizadorDAO(IConnection c)
        {
            conn = c;
        }

        public Utilizador search(int id)
        {
            Utilizador u = new Utilizador();
            using (SqlConnection sconn = conn.Fetch())
            {
                string query = "SELECT * FROM Utilizador where id=@id";
                var dt = new DataTable();
                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        u.Id = int.Parse(dr["id"].ToString());
                        u.Email = dr["email"].ToString();
                        u.Password = dr["password"].ToString();
                        u.DataNascimento = Convert.ToDateTime(dr["dataNasc"].ToString());
                        u.PrefLotacao = int.Parse(dr["lotacaoPref"].ToString());
                        u.PrefPiscina = int.Parse(dr["piscinaPref"].ToString()) == 1 ? true : false;
                        u.PrefBungalow = int.Parse(dr["bungalowPref"].ToString()) == 1 ? true : false;
                        u.PrefChurrasqueira = int.Parse(dr["churrasqueiraPref"].ToString()) == 1 ? true : false;
                        u.PrefEspDesportivo = int.Parse(dr["espacoDesportivoPref"].ToString()) == 1 ? true : false;
                        u.IdLocalizacao = int.Parse(dr["Localizacao_id"].ToString());

                    }
                    reader.Close();
                }
                return u;
            }
        }
        public Utilizador insert(Utilizador obj)
        {
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "INSERT INTO dbo.Utilizador(id,email,password,nome,dataNasc,lotacaoPref,piscinaPref,bungalowPref,churrasqueiraPref,espacoDesportivoPref,Localizacao_id) values (@Id,@Email,@Password,@Nome,@DataNasc,@LotacaoPref,@PiscinaPref,@BungalowPref,@ChurrasqueiraPref,@EspacoDesportivoPref,@Localizacao_id)";

                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                    command.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.Email ?? (object)DBNull.Value;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = obj.Password ?? (object)DBNull.Value;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = obj.Nome ?? (object)DBNull.Value;
                    command.Parameters.Add("@DataNasc", SqlDbType.DateTime).Value = obj.DataNascimento;
                    command.Parameters.Add("@LotacaoPref", SqlDbType.Int).Value = obj.PrefLotacao;
                    command.Parameters.Add("@PiscinaPref", SqlDbType.TinyInt).Value = obj.PrefPiscina;
                    command.Parameters.Add("@BungalowPref", SqlDbType.TinyInt).Value = obj.PrefBungalow;
                    command.Parameters.Add("@ChurrasqueiraPref", SqlDbType.TinyInt).Value = obj.PrefChurrasqueira;
                    command.Parameters.Add("@EspacoDesportivoPref", SqlDbType.TinyInt).Value = obj.PrefEspDesportivo;
                    command.Parameters.Add("@Localizacao_id", SqlDbType.Int).Value = obj.IdLocalizacao;

                    command.ExecuteNonQuery();

                }
            }
            return obj;
        }
            public List<Utilizador> getAll()
            {
                List<Utilizador> utilizadores = new List<Utilizador>();
                using (SqlConnection sconn = conn.Fetch())
                {
                    string queryString = "SELECT * FROM dbo.Utilizador";

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = new SqlCommand(queryString, sconn);
                        DataSet tab = new DataSet();
                        adapter.Fill(tab);

                        foreach (DataTable table in tab.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                Utilizador a = new Utilizador
                                {
                                    Id = int.Parse(row["id"].ToString()),
                                    Email = row["email"].ToString(),
                                    Password = row["password"].ToString(),
                                    Nome = row["nome"].ToString(),
                                    DataNascimento = DateTime.Parse(row["dataNasc"].ToString()),
                                    PrefLotacao = int.Parse(row["lotacaoPref"].ToString()),
                                    PrefPiscina = int.Parse(row["piscinaPref"].ToString()) == 1 ? true : false,
                                    PrefBungalow = int.Parse(row["bungalowPref"].ToString()) == 1 ? true : false,
                                    PrefChurrasqueira = int.Parse(row["churrasqueiraPref"].ToString()) == 1 ? true : false,
                                    PrefEspDesportivo = int.Parse(row["espacoDesportivoPref"].ToString()) == 1 ? true : false,
                                    IdLocalizacao = int.Parse(row["Localizacao_id"].ToString())
                                };
                                utilizadores.Add(a);
                            }
                        }
                    }
                    return utilizadores;
                }
            }

            public bool remove(int key)
            {
                bool removed = false;
                using (SqlConnection sconn = conn.Fetch())
                {
                    String query = "DELETE FROM dbo.Utilizador where id=@Id";

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

            public bool update(int key, Utilizador obj)
            {
                bool updated = false;
                using (SqlConnection sconn = conn.Fetch())
                {
                    String query = "UPDATE dbo.Utilizador SET email=@Email, password=@Password, nome=@Nome, dataNasc=@DataNasc, lotacaoPref=@LotacaoPref, piscinaPref=@PiscinaPref, bungalowPref=@BungalowPref, churrasqueiraPref=@ChurrasqueiraPref, espacoDesportivoPref=@EspacoDesportivoPref, Localizacao_id=@LocalizacaoId WHERE id=@Id";


                    using (SqlCommand command = new SqlCommand(query, sconn))
                    {
                        command.Parameters.AddWithValue("@Email", obj.Email);
                        command.Parameters.AddWithValue("@Password", obj.Password);
                        command.Parameters.AddWithValue("@Nome", obj.Nome);
                        command.Parameters.AddWithValue("@DataNasc", obj.DataNascimento);
                        command.Parameters.AddWithValue("@LotacaoPref", obj.PrefLotacao);
                        command.Parameters.AddWithValue("@PiscinaPref", obj.PrefPiscina);
                        command.Parameters.AddWithValue("@BungalowPref", obj.PrefBungalow);
                        command.Parameters.AddWithValue("@ChurrasqueiraPref", obj.PrefChurrasqueira);
                        command.Parameters.AddWithValue("@EspacoDesportivoPref", obj.PrefEspDesportivo);
                        command.Parameters.AddWithValue("@Localizacao_id", obj.IdLocalizacao);
                        command.Parameters.AddWithValue("@Id", key);


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
