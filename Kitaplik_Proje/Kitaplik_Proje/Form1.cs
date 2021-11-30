using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Lenovo\Desktop\Kitaplık.mdb");
        //Provider=Microsoft.Jet.OLEDB.4.0;Data Source="C:\Users\Lenovo\Documents\Acces VeriTabanı İşlemleri\Kitaplık.mdb"
        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        string durum = "";
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komutkaydet = new OleDbCommand("insert into Kitaplar (KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5) ", baglanti);
            komutkaydet.Parameters.AddWithValue("@p1", txtkitapad.Text);
            komutkaydet.Parameters.AddWithValue("@p2", txtyazar.Text);
            komutkaydet.Parameters.AddWithValue("@p3", cmbtur.Text);
            komutkaydet.Parameters.AddWithValue("@p4", txtsayfa.Text);
            komutkaydet.Parameters.AddWithValue("@p5", durum);
            komutkaydet.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kitap Sisteme Kaydedildi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtkitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtkitapad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbtur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
            

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komutsil = new OleDbCommand("delete from Kitaplar where Kitapid=@p1", baglanti);
            komutsil.Parameters.AddWithValue("@p1", txtkitapid.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Listeden Silindi", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komutguncelle = new OleDbCommand("update Kitaplar set KitapAd=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Kitapid=@p6", baglanti);
            komutguncelle.Parameters.AddWithValue("@p1", txtkitapad.Text);
            komutguncelle.Parameters.AddWithValue("@p2", txtyazar.Text);
            komutguncelle.Parameters.AddWithValue("@p3", cmbtur.Text);
            komutguncelle.Parameters.AddWithValue("@p4", txtsayfa.Text);
            if (radioButton1.Checked==true)
            {
                komutguncelle.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked==true)
            {
                komutguncelle.Parameters.AddWithValue("@p5",durum);
            }
            komutguncelle.Parameters.AddWithValue("@p6", txtkitapid.Text);
            komutguncelle.ExecuteNonQuery();
            MessageBox.Show("Kitap Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            baglanti.Close();

        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            
            OleDbCommand komut = new OleDbCommand("select * from Kitaplar where KitapAd=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", txtKitapBul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            

        }

        private void btnara_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("select * from Kitaplar where KitapAd like '%"+txtKitapBul.Text+ "%'", baglanti);
            
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
