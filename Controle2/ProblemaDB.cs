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
                string sql = "INSERT INTO Problema (descricao, tipo, nivelDificudade, dataCriacao)' " +
                         " VALUES ('" + problema.Descricao + "', ' " + problema.Tipo + " ', ' " + problema.NivelDificuldade + "' ,' " + problema.DataCriacao + "')";
                using (db = new DB())

                    //verificar ' "
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
                var sql = "SELECT problema.id, problema.descricao , tipo.descricao as tipo,problema.datacriacao,  nivel.descricao as nivelDificudade FROM Problema inner join tipo on tipo.id = problema.tipo inner join nivel on nivel.id = problema.nivelDificudade";
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
                    Tipo = new Tipo() { Descricao = retorno["tipo"].ToString() },
                    NivelDificuldade = new Nivel() { Descricao = retorno["nivelDificudade"].ToString() },
                    DataCriacao = Convert.ToDateTime(retorno["dataCriacao"]),
                };

                listFabricante.Add(item);
            }
            return listFabricante;
        }
    }
}
