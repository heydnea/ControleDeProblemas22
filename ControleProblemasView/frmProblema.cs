using Controle2;
using Entidade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleProblemasView
{
    public partial class frmProblema : Form
    {
        public frmProblema()
        {
            InitializeComponent();
        }

        private void frmProblema_Load(object sender, EventArgs e)
        {
            CarregarGrid();
            cbTipo.DataSource = new NivelDB().ListarNivel();
            cbTipo.DisplayMember = "descricao";
            cbTipo.ValueMember = "id";

            CarregarGrid();
            cbNivel.DataSource = new TipoDB().ListarTipo();
            cbNivel.DisplayMember = "descrição";
            cbNivel.ValueMember = "id";



            //var nivel = new NivelDB().ListarNivel();
            //foreach (var item in nivel)
            //{
            //    cbNivel.Items.Add(item.Id);
            //}
            //var tipo = new TipoDB().ListarTipo();
            //foreach (var item in tipo)
            //{
            //    cbTipo.Items.Add(item.Id);
            //}

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string id = cbTipo.SelectedValue.ToString();
            MessageBox.Show("id" + id);

            Problema prob = new Problema();
            prob.Descricao = txtDescricao.Text;
            prob.DataCriacao = Convert.ToDateTime(txtDt.Text);
            prob.Tipo = new Tipo() { Id =Convert.ToInt32(cbTipo.SelectedValue) , Descricao = cbTipo.SelectedItem.ToString()}; 
          //  prob.Tipo =  new Tipo() { cbTipo.SelectedValue ,  };
            prob.NivelDificuldade = new Nivel() { Id = Convert.ToInt32(cbNivel.SelectedValue), Descricao = cbNivel.SelectedItem.ToString() };

            if (new ProblemaDB().insert(prob))
            {
                MessageBox.Show("Registro inserido!");
                txtDescricao.Text = "";
                txtDt.Text = "";
                cbTipo.SelectedItem = 0;
                cbNivel.SelectedItem = 0;
                CarregarGrid();
            }
            else
            {
                MessageBox.Show("Erro ao inserir registro!");
            }

        }
        private void CarregarGrid()
        {
            dgProblema.DataSource = new ProblemaDB().ListarProblema();
        }

        private void dgProblema_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
