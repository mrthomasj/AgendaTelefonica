using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AgendaTelefonica
{
    public partial class Form1 : Form
    {
        Validations val = new Validations();
        AgendaTelefonica.Models.ContactsEntity contextContacts;
        List<AgendaTelefonica.Models.tb_contatos> contacts;
        int curId = 0;

        public Form1()
        {
            InitializeComponent();
            ExibirContatos();
        }


        public void ExibirContatos()
        {
            contextContacts = new Models.ContactsEntity();
            contacts = new List<Models.tb_contatos>();
            //ObjectQuery<Models.tb_contatos> contactsQry = contextContacts.tb_contatos.;
            contacts = contextContacts.tb_contatos.ToList();
            dgvMain.DataSource = contextContacts.tb_contatos.ToList();

            PreencheCampos();


        }


        public void PreencheCampos()
        {
            Models.tb_contatos curContact = contacts[curId];
            txtName.Text = curContact.nmContato;
            txtTel1.Text = curContact.nr_telPrinc;
            txtTel2.Text = curContact.nr_telSec;
            txtEmail.Text = curContact.email;
            txtNote.Text = curContact.notes;
        }

        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            curId = dgvMain.CurrentRow.Index;
            PreencheCampos();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            
            val.clearAll(this);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Models.tb_contatos newCnt = new Models.tb_contatos();
            newCnt.nmContato = txtName.Text;
            newCnt.nr_telPrinc = txtTel1.Text;
            newCnt.nr_telSec = txtTel2.Text;
            newCnt.email = txtEmail.Text;
            newCnt.notes = txtNote.Text;

            contacts.Add(newCnt);
            contextContacts.tb_contatos.Add(newCnt);
            contextContacts.SaveChanges();
            MessageBox.Show("Contato salvo com sucesso!");
            ExibirContatos();
            PreencheCampos();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Models.tb_contatos curCnt = contacts[curId];
            curCnt.nmContato = txtName.Text;
            curCnt.nr_telPrinc = txtTel1.Text;
            curCnt.nr_telSec = txtTel2.Text;
            curCnt.email = txtEmail.Text;
            curCnt.notes = txtNote.Text;

            contextContacts.SaveChanges();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!val.checkEmpty(this))
                return;
            Models.tb_contatos curCnt = contacts[curId];
            curCnt.nmContato = txtName.Text;
            curCnt.nr_telPrinc = txtTel1.Text;
            curCnt.nr_telSec = txtTel2.Text;
            curCnt.email = txtEmail.Text;
            curCnt.notes = txtNote.Text;
            contextContacts.tb_contatos.Remove(curCnt);
            contextContacts.SaveChanges();
            curId = 0;
            ExibirContatos();
            PreencheCampos();
        }
    }
}
