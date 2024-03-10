﻿using System;
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
    public partial class AddPolicy : Form
    {
        OracleConnection con = new OracleConnection();
        string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.238.1)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User id=sys;Password=1;DBA Privilege=SYSDBA;";
        public AddPolicy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.ConnectionString = connString;
            con.Open();
            try
            {
              
                string sql = "BEGIN  DBMS_FGA.ADD_POLICY(object_schema => '" + textBox1.Text + "',object_name => '" + textBox2.Text + "',policy_name => '" + textBox3.Text + "',statement_types => '" + textBox4.Text + "');END; ";
                OracleCommand cmd = new OracleCommand(sql, con);
                //OracleDataReader r = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();



                MessageBox.Show("Thêm policy thành công");
            }

            catch
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void AddPolicy_Load(object sender, EventArgs e)
        {

        }
    }
}
