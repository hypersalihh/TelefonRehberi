using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KisiGetir();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tablo;

        void KisiGetir()
        {
            conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\hyper\\Documents\\database.mdf;Integrated Security=True;Connect Timeout=30");
            adapter = new SqlDataAdapter("SELECT *FROM Kisiler", conn);
            tablo = new DataTable();
            conn.Open();
            adapter.Fill(tablo);
            dgvKisiler.DataSource = tablo;
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KisiGetir();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO Kisiler (Ad, Soyad, Telefon) VALUES (@ad, @soyad, @tel)";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@tel", txtTelefon.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            KisiGetir();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "Update Kisiler Set ad=@Ad,soyad=@Soyad,Telefon=@tel Where Id=@id";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@tel", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@id", dgvKisiler.CurrentRow.Cells[0].Value.ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            KisiGetir();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Kisiler WHERE Id = @id";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@id", dgvKisiler.CurrentRow.Cells[0].Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            KisiGetir();
        }

        private void dgvKisiler_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dgvKisiler.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dgvKisiler.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dgvKisiler.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            DataView dv = tablo.DefaultView;
            dv.RowFilter = "Ad Like '" + txtAra.Text + "%'";
            dgvKisiler.DataSource = dv;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
   
        }
    }
}