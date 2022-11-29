using Npgsql;
using System.Data;

namespace Responsi2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        String connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=Responsi_hasan";
        private NpgsqlConnection conn;
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;
        public int departemen_code;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            try
            {
                conn.Open();
                dgvData.DataSource = null;
                sql = "select * from kar_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.Text);
            try
            {
                conn.Open();
                sql = @"select * from kar_insert(:_nama,:_nama_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", txtNama.Text);
                cmd.Parameters.AddWithValue("_nama_dep", comboBox1.Text);

                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Berhasil ditambahkan", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    LoadData();
                    txtNama.Text = comboBox1.Text = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                r = dgvData.Rows[e.RowIndex];
                txtNama.Text = r.Cells["_nama"].Value.ToString();
                comboBox1.Text = r.Cells["_nama_dep"].Value.ToString();
            }
        }

      

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Warning, mohon pilih data", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //MessageBox.Show(comboBox1.Text);
            try
            {
                conn.Open();
                sql = @"select * from kar_update(:_id_karyawan,:_nama,:_nama_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                cmd.Parameters.AddWithValue("_nama", txtNama.Text);
                cmd.Parameters.AddWithValue("_nama_dep", comboBox1.Text);

                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Berhasil di Update", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    LoadData();
                    txtNama.Text = comboBox1.Text = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Warning, mohon pilih data", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //MessageBox.Show(comboBox1.Text);
            try
            {
                conn.Open();
                sql = @"select * from kar_delete(:_id_karyawan)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());

                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Berhasil di Delete", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    LoadData();
                    txtNama.Text = comboBox1.Text = null;
                    r = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}