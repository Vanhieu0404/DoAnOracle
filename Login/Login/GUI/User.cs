using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Login.GUI
{
    public partial class User : Form
    {
        OracleConnection con = new OracleConnection();
        string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.238.1)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User id=sys;Password=1;DBA Privilege=SYSDBA;";
        public User()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            con.ConnectionString = connString;
            con.Open();
            try
            {
                string a = textBox1.Text;
                string b = textBox2.Text;
                string c = textBox3.Text;
                string create = $"CREATE  user {a} IDENTIFIED by {b} PROFILE {c}";

                using (OracleConnection connection = new OracleConnection(connString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(create, connection))
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Tạo  thành công");
                    }
                }

            }
            catch (OracleException ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        public void loadprofile()
        {

            try
            {
                con.ConnectionString = connString;
                con.Open();

                // Thực hiện truy vấn để lấy dữ liệu từ bảng NHANVIEN
                string sql = "SELECT * FROM dba_users";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = table;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            loadprofile();
        }
    }
}
