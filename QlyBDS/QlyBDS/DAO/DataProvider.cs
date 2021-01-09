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

namespace DoAn_HQTCSDL.DAO
{
    class DataProvider
    {
        // table chi nhánh
        public string[] item1 = { "ID_CHINHANH", "DUONG", "QUAN", "KHUVUC", "THANHPHO", "SDT", "FAX" };
        // table nhân viên
        public string[] item2 = { "IDNHANVIEN", "ID_CHINHANH", "TENNHANVIEN", "SDT", "GIOITINH", "NGAYSINH2", "LUONG", "DIACHI" };
        // connect sql
        string connStr = @"Data Source=DESKTOP-9MRAN9G\SQLEXPRESS;Initial Catalog=QLBN;Integrated Security=True";
        // hàm thực thi câu truy vấn
        public DataTable ExcuteQuery(string query)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand com = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable data = new DataTable();
                adapter.Fill(data);
                conn.Close();
                return data; 
            }    
        }

    }
}
