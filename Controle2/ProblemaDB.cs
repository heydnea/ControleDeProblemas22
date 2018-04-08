using System;
using Controle;
using Entidade;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Controle2
{
    public class ProblemaDB
    {
        
        private DB db;

        public bool insert(Problema problema)
        {
            try
            {
                string sql = "INSERT INTO Problema (descricao, tipo, nivelDificudade, dataCriacao)" +
                         " VALUES ('" + problema.Descricao + "', " + "" + problema.Tipo + ""+ ", " + "" + problema.NivelDificuldade + "" + ", " + "'" + problema.DataCriacao + "'" + " )";
                using (db = new DB())
                {
                    db.ExecutaComando(sql);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<Problema> ListarProblema()
        {
            using (db = new DB())
            {
                var sql = "SELECT id, descricao, tipo , nivelDificudade, dataCriacao FROM Problema";
                var retorno = db.ExecutaComandoRetorno(sql);
                return TransformaSQLReaderEmList(retorno);
            }
        }

        private List<Problema> TransformaSQLReaderEmList
            (SqlDataReader retorno)
        {
            var listFabricante = new List<Problema>();

            while (retorno.Read())
            {
                var item = new Problema()
                {
                    Id = Convert.ToInt32(retorno["id"]),
                    Descricao = retorno["descricao"].ToString(),
                    Tipo = Convert.ToInt32(retorno["tipo"]),
                    NivelDificuldade = Convert.ToInt32(retorno["nivelDificudade"]),
                    DataCriacao = Convert.ToDateTime(retorno["dataCriacao"]),
                };

                listFabricante.Add(item);
            }
            return listFabricante;
        }
    }
}
