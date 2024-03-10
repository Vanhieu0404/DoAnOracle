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


namespace Login
{
    public partial class TramThuPhi : Form
    {
        public TramThuPhi()
        {
            InitializeComponent();
        }
        OracleConnection con = new OracleConnection();
        string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.238.1)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));Password=123;User ID=HR";
        private DataTable ExecuteQuery(string query)
        {
            DataTable result = new DataTable();
            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        private void TramThuPhi_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = connString;
                con.Open();

                // Thực hiện truy vấn để lấy dữ liệu từ bảng NHANVIEN
                string sql = "SELECT * FROM tramthuphibot";
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

        private void button2_Click(object sender, EventArgs e)
        {
            string maThe = textBox1.Text;
            string query = "SELECT X.*, T.SoDuTK, K.HoKH, K.TenKH FROM XE X, THETAG T, KHACHHANG K WHERE X.MaXe = T.MaXe AND K.MaKH = X.MaKH and T.MaVachThe = '" + maThe + "'";
            DataTable result = ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy xe nào đi qua trạm thu phí với mã thẻ này.");
            }
            else
            {
                DataRow row = result.Rows[0];
                string message = "Tên Chủ Xe " + row["HOKH"].ToString() + row["TENKH"].ToString() + " Biển số " + row["BienSoXe"].ToString() + " (" + row["LoaiXe"].ToString() + ", " + row["Mau"].ToString() + ") đã đi qua trạm thu phí.";
                int soDu = Convert.ToInt32(row["SoDuTK"]);
                if (soDu < 100000)
                {
                    message += " Số dư trong thẻ của bạn không đủ để thanh toán. Vui lòng nạp thêm tiền.";
                }
                else
                {
                    try
                    {
                        string maTram = textBox2.Text; // Lấy mã trạm từ textbox 2
                        string maVeThu = "VT" + (new Random().Next(100, 999)).ToString(); // Mã vé thu ngẫu nhiên
                        string maVachThe = maThe;
                        DateTime ngayGioThu = DateTime.Now;
                        // Thực thi truy vấn INSERT vào bảng VETHU
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO VETHU(MaVeThu, MaTram, MaVachThe, NgayGioThu) VALUES(:maVeThu, :maTram, :maVachThe, :ngayGioThu)";
                        cmd.Parameters.Add(":maVeThu", maVeThu);
                        cmd.Parameters.Add(":maTram", maTram);
                        cmd.Parameters.Add(":maVachThe", maVachThe);
                        cmd.Parameters.Add(":ngayGioThu", ngayGioThu);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Lưu thông tin vé thu thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Lưu thông tin vé thu thất bại!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu xuống cơ sở dữ liệu: " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                MessageBox.Show(message);
            }
        }
    }
}
