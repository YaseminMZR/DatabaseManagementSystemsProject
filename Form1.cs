using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SHOE_S
{
    public partial class Form1 : Form
    {

        PostgreSQL pSQL = new PostgreSQL();
        List<Panel> panelList = new List<Panel>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelList.Add(musteriGoruntule_panel);
            panelList.Add(musteriDuzenle_panel);
            panelList.Add(urunGoruntule_panel);
            panelList.Add(urunDuzenle_panel);
            panelList.Add(modelGoruntule_panel);
            panelList.Add(modelDuzenle_panel);
            panelList.Add(siparisGoruntule_panel);
            panelList.Add(siparisDuzenle_panel);
            musteriGoruntuleToolStripMenuItem_Click(sender, e);
        }

        public void panelCall(Panel panel)                                                     
        {
            foreach (Panel p in panelList)
            {
                if (p.Name == panel.Name)
                    p.Visible = true;
                else
                    p.Visible = false;
            }
        }

        public void mg_filter() 
        {
            foreach (DataGridViewRow dataGridViewRow in musteriGoruntule_dgv.Rows)
            {
                if (dataGridViewRow.Cells["Müşteri No"].Value.ToString().ToLower().Contains(mg_musteriNo_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Ad"].Value.ToString().ToLower().Contains(mg_musteriAd_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Soyad"].Value.ToString().ToLower().Contains(mg_musteriSoyad_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["TCKN"].Value.ToString().ToLower().Contains(mg_musteriTc_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["İlçe"].Value.ToString().ToLower().Contains(mg_ilce_cb.SelectedItem.ToString().ToLower()) == true)
                {
                    dataGridViewRow.Visible = true;
                }
                else
                {
                    dataGridViewRow.DataGridView.CurrentCell = null;
                    dataGridViewRow.Visible = false;
                }
            }
        }

        public void ug_filter()
        {
            foreach (DataGridViewRow dataGridViewRow in urunGoruntule_dgv.Rows)
            {
                if (dataGridViewRow.Cells["Ürün Kodu"].Value.ToString().ToLower().Contains(ug_urunKodu_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Ürün Adı"].Value.ToString().ToLower().Contains(ug_urunAdi_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Birim Fiyatı"].Value.ToString().ToLower().Contains(ug_birimFiyati_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Stok Miktarı"].Value.ToString().ToLower().Contains(ug_stokMiktari_tb.Text.ToLower()) == true &&
                    (!ug_checkBox.Checked || (dataGridViewRow.Cells["Üretim Tarihi"].Value.ToString().ToLower().Contains(ug_uretimTarihi_dtp.Value.ToString("d.MM.yyyy").ToLower()) == true)) &&
                    dataGridViewRow.Cells["Kategori"].Value.ToString().ToLower().Contains(ug_kategoriKodu_cb.SelectedItem.ToString().ToLower()) == true &&
                    dataGridViewRow.Cells["Model"].Value.ToString().ToLower().Contains(ug_modelKodu_cb.SelectedItem.ToString().ToLower()) == true &&
                    dataGridViewRow.Cells["Renk"].Value.ToString().ToLower().Contains(ug_urunRengi_tb.Text.ToLower()) == true )
                {
                    dataGridViewRow.Visible = true;
                }
                else
                {
                    dataGridViewRow.DataGridView.CurrentCell = null;
                    dataGridViewRow.Visible = false;
                }
            }
        }

        public void modelg_filter()
        {
            foreach (DataGridViewRow dataGridViewRow in modelGoruntule_dgv.Rows)
            {
                if (dataGridViewRow.Cells["Model Kodu"].Value.ToString().ToLower().Contains(mg_modelKodu_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Model Adı"].Value.ToString().ToLower().Contains(mg_modelAdi_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Stok Miktarı"].Value.ToString().ToLower().Contains(mg_stokMiktari_tb.Text.ToLower()) == true )
                {
                    dataGridViewRow.Visible = true;
                }
                else
                {
                    dataGridViewRow.DataGridView.CurrentCell = null;
                    dataGridViewRow.Visible = false;
                }
            }
        }

        public void sg_filter()
        {
            foreach (DataGridViewRow dataGridViewRow in siparisGoruntule_dgv.Rows)
            {
                if (dataGridViewRow.Cells["Sipariş Kodu"].Value.ToString().ToLower().Contains(sg_siparisKodu_tb.Text.ToLower()) == true &&
                    (!sg_checkBox.Checked || (dataGridViewRow.Cells["Sipariş Tarihi"].Value.ToString().ToLower().Contains(sg_siparisTarihi_dtp.Value.ToString("d.MM.yyyy").ToLower()) == true))&&
                    dataGridViewRow.Cells["Toplam Tutar"].Value.ToString().ToLower().Contains(sg_toplamTutar_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Kargo Kodu"].Value.ToString().ToLower().Contains(sg_kargoKodu_tb.Text.ToLower()) == true &&
                    dataGridViewRow.Cells["Müşteri No"].Value.ToString().ToLower().Contains(sg_musteriNo_cb.SelectedItem.ToString().ToLower()) == true &&
                    dataGridViewRow.Cells["Personel No"].Value.ToString().ToLower().Contains(sg_personelNo_cb.SelectedItem.ToString().ToLower()) == true &&
                    dataGridViewRow.Cells["Kargo Firması"].Value.ToString().ToLower().Contains(sg_kargoFirmasi_cb.SelectedItem.ToString().ToLower()) == true )
                {
                    dataGridViewRow.Visible = true;
                }
                else
                {
                    dataGridViewRow.DataGridView.CurrentCell = null;
                    dataGridViewRow.Visible = false;
                }
            }
        }

        private void musteriGoruntuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mg_musteriNo_tb.Text = "";
            mg_musteriTc_tb.Text = "";
            mg_musteriAd_tb.Text = "";
            mg_musteriSoyad_tb.Text = "";
            mg_ilce_cb.Items.Clear();
            mg_ilce_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"ilceAdi\" from \"ilce\"").Rows)
                mg_ilce_cb.Items.Add(d.ItemArray[0].ToString());
            mg_ilce_cb.Text = "";
            musteriGoruntule_dgv.DataSource = pSQL.sqlTablo("select " +
                "\"musteriNo\" as \"Müşteri No\", " +
                "\"TCKimlikNo\" as \"TCKN\", " +
                "\"musteriAdi\" as \"Ad\", " +
                "\"musteriSoyadi\" as \"Soyad\", " +
                "\"ilceAdi\" as \"İlçe\" " +
                "from \"Musteri\",\"ilce\" " +
                "where \"ilceKodu\"=\"ilce\"");
            panelCall(musteriGoruntule_panel);
        }

        private void musteriDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            md_musteriNo_tb.Text = "";
            md_musteriTc_tb.Text = "";
            md_musteriAd_tb.Text = "";
            md_musteriSoyad_tb.Text = "";
            md_ilce_cb.Items.Clear();
            md_ilce_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"ilceAdi\" from \"ilce\"").Rows)
                md_ilce_cb.Items.Add(d.ItemArray[0].ToString());
            md_ilce_cb.Text = "";
            musteriDuzenle_dgv.DataSource = pSQL.sqlTablo("select " +
                "\"musteriNo\" as \"Müşteri No\", " +
                "\"TCKimlikNo\" as \"TCKN\", " +
                "\"musteriAdi\" as \"Ad\", " +
                "\"musteriSoyadi\" as \"Soyad\", " +
                "\"ilceAdi\" as \"İlçe\" " +
                "from \"Musteri\",\"ilce\" " +
                "where \"ilceKodu\"=\"ilce\"");
            panelCall(musteriDuzenle_panel);
        }

        private void urunlerGorüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ug_urunKodu_tb.Text = "";
            ug_urunAdi_tb.Text = "";
            ug_birimFiyati_tb.Text = "";
            ug_stokMiktari_tb.Text = "";
            ug_urunRengi_tb.Text = "";
            ug_uretimTarihi_dtp.Value = DateTime.Today;
            ug_kategoriKodu_cb.Items.Clear();
            ug_kategoriKodu_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"KategoriAdi\" from \"Kategori\"").Rows)
                ug_kategoriKodu_cb.Items.Add(d.ItemArray[0].ToString());
            ug_kategoriKodu_cb.Text = "";
            ug_modelKodu_cb.Items.Clear();
            ug_modelKodu_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"modelAdi\" from \"Model\"").Rows)
                ug_modelKodu_cb.Items.Add(d.ItemArray[0].ToString());
            ug_modelKodu_cb.Text = "";
            urunGoruntule_dgv.DataSource = pSQL.sqlTablo("select" +
                "\"urunKodu\" as \"Ürün Kodu\", " +
                "\"urunAdi\" as \"Ürün Adı\", " +
                "\"birimFiyati\" as \"Birim Fiyatı\", " +
                "\"Urun\".\"stokMiktari\" as \"Stok Miktarı\", " +
                "\"uretimTarihi\" as \"Üretim Tarihi\", " +
                "\"KategoriAdi\" as \"Kategori\", " +
                "\"modelAdi\" as \"Model\", " +
                "\"urunRengi\" as \"Renk\" " +
                "from \"Urun\",\"Kategori\",\"Model\" " +
                "where \"Kategori\".\"kategoriKodu\"=\"Urun\".\"kategoriKodu\" and \"Model\".\"modelKodu\"=\"Urun\".\"modelKodu\"");
            panelCall(urunGoruntule_panel);
        }

        private void urunlerDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ud_urunKodu_tb.Text = "";
            ud_urunAdi_tb.Text = "";
            ud_birimFiyati_tb.Text = "";
            ud_stokMiktari_tb.Text = "";
            ud_urunRengi_tb.Text = "";
            ud_uretimTarihi_dtp.Value = DateTime.Today;
            ud_kategoriKodu_cb.Items.Clear();
            ud_kategoriKodu_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"KategoriAdi\" from \"Kategori\"").Rows)
                ud_kategoriKodu_cb.Items.Add(d.ItemArray[0].ToString());
            ud_kategoriKodu_cb.Text = "";
            ud_modelKodu_cb.Items.Clear();
            ud_modelKodu_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"modelAdi\" from \"Model\"").Rows)
                ud_modelKodu_cb.Items.Add(d.ItemArray[0].ToString());
            ud_modelKodu_cb.Text = "";
            urunDuzenle_dgv.DataSource = pSQL.sqlTablo("select" +
                "\"urunKodu\" as \"Ürün Kodu\", " +
                "\"urunAdi\" as \"Ürün Adı\", " +
                "\"birimFiyati\" as \"Birim Fiyatı\", " +
                "\"Urun\".\"stokMiktari\" as \"Stok Miktarı\", " +
                "\"uretimTarihi\" as \"Üretim Tarihi\", " +
                "\"KategoriAdi\" as \"Kategori\", " +
                "\"modelAdi\" as \"Model\", " +
                "\"urunRengi\" as \"Renk\" " +
                "from \"Urun\",\"Kategori\",\"Model\" " +
                "where \"Kategori\".\"kategoriKodu\"=\"Urun\".\"kategoriKodu\" and \"Model\".\"modelKodu\"=\"Urun\".\"modelKodu\"");
            panelCall(urunDuzenle_panel);
        }

        private void modellerGorüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mg_modelKodu_tb.Text = "";
            mg_modelAdi_tb.Text = "";
            mg_stokMiktari_tb.Text = "";
            modelGoruntule_dgv.DataSource = pSQL.sqlTablo("select " +
                "\"modelKodu\" as \"Model Kodu\", " +
                "\"modelAdi\" as \"Model Adı\", " +
                "\"stokMiktari\" as \"Stok Miktarı\" " +
                "from \"Model\"");
            panelCall(modelGoruntule_panel);
        }

        private void modellerDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            md_modelKodu_tb.Text = "";
            md_modelAdi_tb.Text = "";
            md_stokMiktari_tb.Text = "";
            modelDuzenle_dgv.DataSource = pSQL.sqlTablo("select " +
                "\"modelKodu\" as \"Model Kodu\", " +
                "\"modelAdi\" as \"Model Adı\", " +
                "\"stokMiktari\" as \"Stok Miktarı\" " +
                "from \"Model\"");
            panelCall(modelDuzenle_panel);
        }

        private void siparislerGorüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sg_siparisKodu_tb.Text = "";
            sg_kargoKodu_tb.Text = "";
            sg_toplamTutar_tb.Text = "";
            sg_kargoKodu_tb.Text = "";
            sg_siparisTarihi_dtp.Value = DateTime.Today;
            sg_musteriNo_cb.Items.Clear();
            sg_musteriNo_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"musteriNo\" from \"Musteri\"").Rows)
                sg_musteriNo_cb.Items.Add(d.ItemArray[0].ToString());
            sg_musteriNo_cb.Text = "";
            sg_personelNo_cb.Items.Clear();
            sg_personelNo_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"personelNo\" from \"Personel\"").Rows)
                sg_personelNo_cb.Items.Add(d.ItemArray[0].ToString());
            sg_personelNo_cb.Text = "";
            sg_kargoFirmasi_cb.Items.Clear();
            sg_kargoFirmasi_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"firmaKodu\" from \"KargoFirmasi\"").Rows)
                sg_kargoFirmasi_cb.Items.Add(d.ItemArray[0].ToString());
            sg_kargoFirmasi_cb.Text = "";
            siparisGoruntule_dgv.DataSource = pSQL.sqlTablo("select " +
                "\"siparisKodu\" as \"Sipariş Kodu\", " +
                "\"siparisTarihi\" as \"Sipariş Tarihi\", " +
                "\"toplamTutar\" as \"Toplam Tutar\", " +
                "\"kargoKodu\" as \"Kargo Kodu\", " +
                "\"musteriNo\" as \"Müşteri No\", " +
                "\"personelNo\" as \"Personel No\", " +
                "\"kargoFirmasi\" as \"Kargo Firması\" " +
                "from \"Siparis\"");
            panelCall(siparisGoruntule_panel);
        }

        private void siparislerDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sd_siparisKodu_tb.Text = "";
            sd_kargoKodu_tb.Text = "";
            sd_toplamTutar_tb.Text = "";
            sd_kargoKodu_tb.Text = "";
            sd_siparisTarihi_dtp.Value = DateTime.Today;
            sd_musteriNo_cb.Items.Clear();
            sd_musteriNo_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"musteriNo\" from \"Musteri\"").Rows)
                sd_musteriNo_cb.Items.Add(d.ItemArray[0].ToString());
            sd_musteriNo_cb.Text = "";
            sd_personelNo_cb.Items.Clear();
            sd_personelNo_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"personelNo\" from \"Personel\"").Rows)
                sd_personelNo_cb.Items.Add(d.ItemArray[0].ToString());
            sd_personelNo_cb.Text = "";
            sd_kargoFirmasi_cb.Items.Clear();
            sd_kargoFirmasi_cb.Items.Add("");
            foreach (System.Data.DataRow d in pSQL.sqlTablo("select \"firmaKodu\" from \"KargoFirmasi\"").Rows)
                sd_kargoFirmasi_cb.Items.Add(d.ItemArray[0].ToString());
            sd_kargoFirmasi_cb.Text = "";
            siparisDuzenle_dgv.DataSource = pSQL.sqlTablo("select " +
                "\"siparisKodu\" as \"Sipariş Kodu\", " +
                "\"siparisTarihi\" as \"Sipariş Tarihi\", " +
                "\"toplamTutar\" as \"Toplam Tutar\", " +
                "\"kargoKodu\" as \"Kargo Kodu\", " +
                "\"musteriNo\" as \"Müşteri No\", " +
                "\"personelNo\" as \"Personel No\", " +
                "\"kargoFirmasi\" as \"Kargo Firması\" " +
                "from \"Siparis\"");
            panelCall(siparisDuzenle_panel);
        }

        private void mg_musteriNo_tb_TextChanged(object sender, EventArgs e)
        {
            mg_filter();
        }

        private void mg_musteriAd_tb_TextChanged(object sender, EventArgs e)
        {
            mg_filter();
        }

        private void mg_musteriSoyad_tb_TextChanged(object sender, EventArgs e)
        {
            mg_filter();
        }

        private void mg_musteriTc_tb_TextChanged(object sender, EventArgs e)
        {
            mg_filter();
        }

        private void mg_ilce_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            mg_filter();
        }

        private void musteriDuzenle_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = musteriDuzenle_dgv.SelectedRows[0];
            md_musteriNo_tb.Text = selectedRow.Cells["Müşteri No"].Value.ToString();
            md_musteriTc_tb.Text = selectedRow.Cells["TCKN"].Value.ToString();
            md_musteriAd_tb.Text = selectedRow.Cells["Ad"].Value.ToString();
            md_musteriSoyad_tb.Text = selectedRow.Cells["Soyad"].Value.ToString();
            md_ilce_cb.SelectedIndex = md_ilce_cb.Items.IndexOf(selectedRow.Cells["İlçe"].Value.ToString());
        }

        private void md_ekle_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Musteri\" where \"musteriNo\"='" + md_musteriNo_tb.Text + "'")).Equals(0))
            {
                pSQL.sqlIslem("insert into \"Musteri\" " +
                    "values('"+md_musteriNo_tb.Text+"'," +
                    "'"+md_musteriTc_tb.Text+"'," +
                    "'"+md_musteriAd_tb.Text+"'," +
                    "'"+md_musteriSoyad_tb.Text+"'," +
                    "'"+pSQL.sqlString("select \"ilceKodu\" from \"ilce\" where \"ilceAdi\"='"+md_ilce_cb.SelectedItem.ToString()+"'") +"')");
                MessageBox.Show(md_musteriNo_tb.Text + " Numaralı Yeni Kayıt Başarıyla Eklendi");
                musteriDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Varolan Kayıt Eklenemez!");
        }

        private void md_sil_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Musteri\" where \"musteriNo\"='" + md_musteriNo_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("delete from \"Musteri\" where \"musteriNo\"='"+md_musteriNo_tb.Text+"'");
                MessageBox.Show(md_musteriNo_tb.Text + " Numaralı Kayıt Başarıyla Silindi");
                musteriDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Silinecek Kayıt Bulunamadı!");
        }

        private void md_kaydet_bt_Click(object sender, EventArgs e)
        {

            if (int.Parse(pSQL.sqlString("select count(*) from \"Musteri\" where \"musteriNo\"='" + md_musteriNo_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("update \"Musteri\" " +
                    "set \"TCKimlikNo\"='"+md_musteriTc_tb.Text+"', " +
                    "\"musteriAdi\"='" + md_musteriAd_tb.Text + "', " +
                    "\"musteriSoyadi\"='" + md_musteriSoyad_tb.Text + "', " +
                    "\"ilce\"='" + pSQL.sqlString("select \"ilceKodu\" from \"ilce\" where \"ilceAdi\"='" + md_ilce_cb.SelectedItem.ToString() + "'")+"' "+
                    "where \"musteriNo\"='"+md_musteriNo_tb.Text+"'");
                MessageBox.Show(md_musteriNo_tb.Text + " Numaralı Kayıt Başarıyla Kaydedildi");
                musteriDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Kaydedilecek Kayıt Bulunamadı!");
        }

        private void ug_urunKodu_tb_TextChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_urunAdi_tb_TextChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_birimFiyati_tb_TextChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_stokMiktari_tb_TextChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_urunRengi_tb_TextChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_kategoriKodu_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_modelKodu_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void ug_uretimTarihi_dtp_ValueChanged(object sender, EventArgs e)
        {
            ug_filter();
        }


        private void ug_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ug_filter();
        }

        private void urunDuzenle_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = urunDuzenle_dgv.SelectedRows[0];
            ud_urunKodu_tb.Text = selectedRow.Cells["Ürün Kodu"].Value.ToString();
            ud_urunAdi_tb.Text = selectedRow.Cells["Ürün Adı"].Value.ToString();
            ud_birimFiyati_tb.Text = selectedRow.Cells["Birim Fiyatı"].Value.ToString();
            ud_stokMiktari_tb.Text = selectedRow.Cells["Stok Miktarı"].Value.ToString();
            ud_urunRengi_tb.Text = selectedRow.Cells["Renk"].Value.ToString();
            ud_kategoriKodu_cb.SelectedIndex = ud_kategoriKodu_cb.Items.IndexOf(selectedRow.Cells["Kategori"].Value.ToString());
            ud_modelKodu_cb.SelectedIndex = ud_modelKodu_cb.Items.IndexOf(selectedRow.Cells["Model"].Value.ToString());
            ud_uretimTarihi_dtp.Value = DateTime.Parse(selectedRow.Cells["Üretim Tarihi"].Value.ToString());
        }

        private void ud_ekle_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Urun\" where \"urunKodu\"='" + ud_urunKodu_tb.Text + "'")).Equals(0))
            {
                pSQL.sqlIslem("insert into \"Urun\" " +
                    "values('" + ud_urunKodu_tb.Text + "'," +
                    "'" + ud_urunAdi_tb.Text + "'," +
                    "'" + ud_birimFiyati_tb.Text.Replace(',','.') + "'," +
                    "'" + ud_stokMiktari_tb.Text + "'," +
                    "'" + ud_uretimTarihi_dtp.Value.ToString("yyyy-MM-dd") + "'," +
                    "'" + pSQL.sqlString("select \"kategoriKodu\" from \"Kategori\" where \"KategoriAdi\"='" + ud_kategoriKodu_cb.SelectedItem.ToString() + "'") + "'," +
                    "'" + pSQL.sqlString("select \"modelKodu\" from \"Model\" where \"modelAdi\"='" + ud_modelKodu_cb.SelectedItem.ToString() + "'") + "'," +
                    "'" + ud_urunRengi_tb.Text + "')");
                MessageBox.Show(ud_urunKodu_tb.Text + " Numaralı Yeni Kayıt Başarıyla Eklendi");
                urunlerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Varolan Kayıt Eklenemez!");
        }

        private void ud_kaydet_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Urun\" where \"urunKodu\"='" + ud_urunKodu_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("update \"Urun\" " +
                    "set \"urunAdi\"='" + ud_urunAdi_tb.Text + "', " +
                    "\"birimFiyati\"='" + ud_birimFiyati_tb.Text.Replace(',', '.') + "', " +
                    "\"stokMiktari\"='" + ud_stokMiktari_tb.Text + "', " +
                    "\"uretimTarihi\"='" + ud_uretimTarihi_dtp.Value.ToString("yyyy-MM-dd") + "', " +
                    "\"kategoriKodu\"='" + pSQL.sqlString("select \"kategoriKodu\" from \"Kategori\" where \"KategoriAdi\"='" + ud_kategoriKodu_cb.SelectedItem.ToString() + "'") + "', " +
                    "\"modelKodu\"='" + pSQL.sqlString("select \"modelKodu\" from \"Model\" where \"modelAdi\"='" + ud_modelKodu_cb.SelectedItem.ToString() + "'") + "', " +
                    "\"urunRengi\"='" + ud_urunRengi_tb.Text + "' " +
                    "where \"urunKodu\"='" + ud_urunKodu_tb.Text + "'");
                MessageBox.Show(ud_urunKodu_tb.Text + " Numaralı Kayıt Başarıyla Kaydedildi");
                urunlerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Kaydedilecek Kayıt Bulunamadı!");
        }

        private void ud_sil_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Urun\" where \"urunKodu\"='" + ud_urunKodu_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("delete from \"Urun\" where \"urunKodu\"='" + ud_urunKodu_tb.Text + "'");
                MessageBox.Show(ud_urunKodu_tb.Text + " Numaralı Kayıt Başarıyla Silindi");
                urunlerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Silinecek Kayıt Bulunamadı!");
        }


        private void model_ekle_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Model\" where \"modelKodu\"='" + md_modelKodu_tb.Text + "'")).Equals(0))
            {
                pSQL.sqlIslem("insert into \"Model\" " +
                    "values('" + md_modelKodu_tb.Text + "'," +
                    "'" + md_modelAdi_tb.Text + "'," +
                    "'" + md_stokMiktari_tb.Text + "')");
                MessageBox.Show(md_modelKodu_tb.Text + " Numaralı Yeni Kayıt Başarıyla Eklendi");
                modellerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Varolan Kayıt Eklenemez!");
        }

        private void model_kaydet_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Model\" where \"modelKodu\"='" + md_modelKodu_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("update \"Model\" " +
                    "set \"modelAdi\"='" + md_modelAdi_tb.Text + "', " +
                    "\"stokMiktari\"='" + md_stokMiktari_tb.Text + "' " +
                    "where \"modelKodu\"='" + md_modelKodu_tb.Text + "'");
                MessageBox.Show(md_modelKodu_tb.Text + " Numaralı Kayıt Başarıyla Kaydedildi");
                modellerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Kaydedilecek Kayıt Bulunamadı!");
        }

        private void model_sil_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Model\" where \"modelKodu\"='" + md_modelKodu_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("delete from \"Model\" where \"modelKodu\"='" + md_modelKodu_tb.Text + "'");
                MessageBox.Show(md_modelKodu_tb.Text + " Numaralı Kayıt Başarıyla Silindi");
                modellerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Silinecek Kayıt Bulunamadı!");
        }
        private void mg_modelKodu_tb_TextChanged(object sender, EventArgs e)
        {
            modelg_filter();
        }

        private void mg_modelAdi_tb_TextChanged(object sender, EventArgs e)
        {
            modelg_filter();
        }

        private void mg_stokMiktari_tb_TextChanged(object sender, EventArgs e)
        {
            modelg_filter();
        }

        private void modelDuzenle_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = modelDuzenle_dgv.SelectedRows[0];
            md_modelKodu_tb.Text = selectedRow.Cells["Model Kodu"].Value.ToString();
            md_modelAdi_tb.Text = selectedRow.Cells["Model Adı"].Value.ToString();
            md_stokMiktari_tb.Text = selectedRow.Cells["Stok Miktarı"].Value.ToString();
        }

        private void sd_ekle_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Siparis\" where \"siparisKodu\"='" + sd_siparisKodu_tb.Text + "'")).Equals(0))
            {
                pSQL.sqlIslem("insert into \"Siparis\" " +
                    "values('" + sd_siparisKodu_tb.Text + "'," +
                    "'" + sd_siparisTarihi_dtp.Value.ToString("yyyy-MM-dd") + "'," +
                    "'" + sd_toplamTutar_tb.Text.Replace(',','.') + "', " +
                    "'" + sd_kargoKodu_tb.Text + "', " +
                    "'" + sd_musteriNo_cb.SelectedItem.ToString() + "', " +
                    "'" + sd_personelNo_cb.SelectedItem.ToString() + "', " +
                    "'" + sd_kargoFirmasi_cb.SelectedItem.ToString() + "')");
                MessageBox.Show(sd_siparisKodu_tb.Text + " Numaralı Yeni Kayıt Başarıyla Eklendi");
                siparislerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Varolan Kayıt Eklenemez!");
        }

        private void sd_kaydet_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Siparis\" where \"siparisKodu\"='" + sd_siparisKodu_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("update \"Siparis\" " +
                    "set \"siparisTarihi\"='" + sd_siparisTarihi_dtp.Value.ToString("yyyy-MM-dd") + "', " +
                    "\"toplamTutar\"='" + sd_toplamTutar_tb.Text.Replace(',','.') + "', " +
                    "\"kargoKodu\"='" + sd_kargoKodu_tb.Text + "', " +
                    "\"musteriNo\"='" + sd_musteriNo_cb.SelectedItem.ToString() + "', " +
                    "\"personelNo\"='" + sd_personelNo_cb.SelectedItem.ToString() + "', " +
                    "\"kargoFirmasi\"='" + sd_kargoFirmasi_cb.SelectedItem.ToString() + "' " +
                    "where \"siparisKodu\"='" + sd_siparisKodu_tb.Text + "'");
                MessageBox.Show(sd_siparisKodu_tb.Text + " Numaralı Kayıt Başarıyla Kaydedildi");
                siparislerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Kaydedilecek Kayıt Bulunamadı!");
        }

        private void sd_sil_bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(pSQL.sqlString("select count(*) from \"Siparis\" where \"siparisKodu\"='" + sd_siparisKodu_tb.Text + "'")).Equals(1))
            {
                pSQL.sqlIslem("delete from \"Siparis\" where \"siparisKodu\"='" + sd_siparisKodu_tb.Text + "'");
                MessageBox.Show(sd_siparisKodu_tb.Text + " Numaralı Kayıt Başarıyla Silindi");
                siparislerDuzenleToolStripMenuItem_Click(sender, e);
            }
            else
                MessageBox.Show("Silinecek Kayıt Bulunamadı!");
        }

        private void sg_siparisKodu_tb_TextChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_toplamTutar_tb_TextChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_kargoKodu_tb_TextChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_musteriNo_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_personelNo_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_kargoFirmasi_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_siparisTarihi_dtp_ValueChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void sg_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            sg_filter();
        }

        private void siparisDuzenle_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = siparisDuzenle_dgv.SelectedRows[0];
            sd_siparisKodu_tb.Text = selectedRow.Cells["Sipariş Kodu"].Value.ToString();
            sd_toplamTutar_tb.Text = selectedRow.Cells["Toplam Tutar"].Value.ToString();
            sd_kargoKodu_tb.Text = selectedRow.Cells["Kargo Kodu"].Value.ToString();
            sd_musteriNo_cb.SelectedIndex = sd_musteriNo_cb.Items.IndexOf(selectedRow.Cells["Müşteri No"].Value.ToString());
            sd_personelNo_cb.SelectedIndex = sd_personelNo_cb.Items.IndexOf(selectedRow.Cells["Personel No"].Value.ToString());
            sd_kargoFirmasi_cb.SelectedIndex = sd_kargoFirmasi_cb.Items.IndexOf(selectedRow.Cells["Kargo Firması"].Value.ToString());
            sd_siparisTarihi_dtp.Value = DateTime.Parse(selectedRow.Cells["Sipariş Tarihi"].Value.ToString());
        }
    }
}
