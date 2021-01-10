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

namespace QlyBDS.DAO
{
    class DataProvider
    {
        // table chi nhánh
        public string[] item1 = { "ID_CHINHANH", "DUONG", "QUAN", "KHUVUC", "THANHPHO", "SDT", "FAX" };
        // table nhân viên
        public string[] item2 = { "IDNHANVIEN", "ID_CHINHANH", "TENNHANVIEN", "SDT", "GIOITINH", "NGAYSINH2", "LUONG", "DIACHI" };
        // connect sql
        public string connStr = @"Data Source=DESKTOP-9MRAN9G\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";

        public SqlCommand com;
        public SqlDataReader read;
        public SqlDataAdapter adapter;
        public SqlConnection conn;

        // hàm thực thi câu truy vấn
        public DataTable ExcuteQuery(string query)
        {
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                com = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
                conn.Close();
                return data;
            }
        }

        public bool ExcuteProc(string query)
        {
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                return true;
            }
        }

        public ComboBox SetComboBox(string query)
        {
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand com = new SqlCommand(query, conn);
                SqlDataReader read = com.ExecuteReader();
                ComboBox cmb = new ComboBox();
                while(read.Read())
                {
                    cmb.Items.Add(read[0].ToString());
                }    
                conn.Close();
                return cmb;
            }
        }
    }
}
