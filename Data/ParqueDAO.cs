using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiMPAC.Models;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace SiMPAC.Data
{
    public class ParqueDAO : IDataAccessObject<Parque>
    {
        private IConnection conn;
        public ParqueDAO(IConnection c)
        {
            conn = c;
        }

        public Parque search(int id)
        {
            Console.WriteLine("Id: " + id);
            Parque p = new Parque();
            using (SqlConnection sconn = conn.Fetch())
            {
                string query = "SELECT * FROM dbo.Parque where id=@id";
                var dt = new DataTable();
                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine("Existe id");
                        p.Id = int.Parse(dr["id"].ToString());
                        p.Nome = dr["nome"].ToString();
                        p.AvaliacaoMedia = double.Parse(dr["avaliacaoMedia"].ToString());
                        p.CapacidadeMax = int.Parse(dr["capacidadeMaxima"].ToString());
                        p.LotacaoAtual = int.Parse(dr["lotacaoAtual"].ToString());
                        p.TemPiscina = int.Parse(dr["temPiscina"].ToString()) == 1 ? true : false;
                        p.TemBungalow = int.Parse(dr["temBungalow"].ToString()) == 1 ? true : false;
                        p.TemChurrasqueira = int.Parse(dr["temChurrasqueira"].ToString()) == 1 ? true : false;
                        p.TemEspacoDesportivo = int.Parse(dr["temEspacoDesportivo"].ToString()) == 1 ? true : false;
                        p.IdLocalizacao = int.Parse(dr["Localizacao_id"].ToString());

                    }
                    reader.Close();
                }
                return p;
            }
        }
        public Parque insert(Parque obj)
        {
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "INSERT INTO dbo.Parque(id,nome,avaliacaoMedia,capacidadeMax,lotacaoAtual,temPiscina,temBungalow,temChurrasqueira,temEspacoDesportivo,Localizacao_id) values (@Id,@Nome,@AvaliacaoMedia,@CapacidadeMax,@LotacaoAtual,@TemPiscina,@TemBungalow,@TemChurrasqueira,@TemEspacoDesportivo,@IdLocalizacao)";

                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = obj.Nome ?? (object)DBNull.Value;
                    command.Parameters.Add("@AvaliacaoMedia", SqlDbType.Decimal).Value = obj.AvaliacaoMedia;
                    command.Parameters.Add("@CapacidadeMax", SqlDbType.Int).Value = obj.CapacidadeMax;
                    command.Parameters.Add("@LotacaoAtual", SqlDbType.Int).Value = obj.LotacaoAtual;
                    command.Parameters.Add("@TemPiscina", SqlDbType.TinyInt).Value = obj.TemPiscina;
                    command.Parameters.Add("@TemBungalow", SqlDbType.TinyInt).Value = obj.TemBungalow;
                    command.Parameters.Add("@TemChurrasqueira", SqlDbType.TinyInt).Value = obj.TemChurrasqueira;
                    command.Parameters.Add("@TemEspacoDesportivo", SqlDbType.TinyInt).Value = obj.TemEspacoDesportivo;
                    command.Parameters.Add("@IdLocalizacao", SqlDbType.Int).Value = obj.IdLocalizacao;

                    command.ExecuteNonQuery();

                }
            }
            return obj;
        }
        public List<Parque> getAll()
        {
            List<Parque> parques = new List<Parque>();
            using (SqlConnection sconn = conn.Fetch())
            {
                string queryString = "SELECT * FROM dbo.Parque";

                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand(queryString, sconn);
                    DataSet tab = new DataSet();
                    adapter.Fill(tab);

                    foreach (DataTable table in tab.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Parque a = new Parque()
                            {
                                Id = int.Parse(row["id"].ToString()),
                                Nome = row["nome"].ToString(),
                                AvaliacaoMedia = double.Parse(row["avaliacaoMedia"].ToString()),
                                CapacidadeMax = int.Parse(row["capacidadeMaxima"].ToString()),
                                LotacaoAtual = int.Parse(row["lotacaoAtual"].ToString()),
                                TemPiscina =  int.Parse(row["temPiscina"].ToString()) == 1 ? true : false,
                                TemBungalow = int.Parse(row["temBungalow"].ToString()) == 1 ? true : false,
                                TemChurrasqueira = int.Parse(row["temChurrasqueira"].ToString()) == 1 ? true : false,
                                TemEspacoDesportivo = int.Parse(row["temEspacoDesportivo"].ToString()) == 1 ? true : false,
                                IdLocalizacao = int.Parse(row["Localizacao_id"].ToString())
                            };
                            parques.Add(a);
                        }
                    }
                }
                return parques;
            }
        }

        public bool remove(int key)
        {
            bool removed = false;
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "DELETE FROM dbo.Parque where id=@Id";

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

        public bool update(int key, Parque obj)
        {
            bool updated = false;
            using (SqlConnection sconn = conn.Fetch())
            {
                String query = "UPDATE dbo.Parque SET nome=@Nome, avaliacaoMedia=@AvaliacaoMedia, capacidadeMax=@CapacidadeMax, lotacaoAtual=@LotacaoAtual, temPiscina=@TemPiscina, temBungalow=@TemBungalow, temChurrasqueira=@TemChurrasqueira, temEspacoDesportivo=@TemEspacoDesportivo, Localizacao_id=@LocalizacaoId WHERE id=@Id";


                using (SqlCommand command = new SqlCommand(query, sconn))
                {
                    command.Parameters.AddWithValue("@Nome", obj.Nome);
                    command.Parameters.AddWithValue("@AvaliacaoMedia", obj.AvaliacaoMedia);
                    command.Parameters.AddWithValue("@CapacidadeMax", obj.CapacidadeMax);
                    command.Parameters.AddWithValue("@LotacaoAtual", obj.LotacaoAtual);
                    command.Parameters.AddWithValue("@TemPiscina", obj.TemPiscina);
                    command.Parameters.AddWithValue("@TemBungalow", obj.TemBungalow);
                    command.Parameters.AddWithValue("@TemChurrasqueira", obj.TemChurrasqueira);
                    command.Parameters.AddWithValue("@TemEspacoDesportivo", obj.TemEspacoDesportivo);
                    command.Parameters.AddWithValue("@LocalizacaoId", obj.IdLocalizacao);
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
