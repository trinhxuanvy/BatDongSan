using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QlyBDS.DAO;
using System.Data.SqlClient;

namespace QlyBDS
{
    public partial class fModifyProduct : Form
    {
        DataProvider data = new DataProvider();
        Tools tool = new Tools();
        string query = "";

        public fModifyProduct()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void fModifyProduct_Load(object sender, EventArgs e)
        {
            List<string> cn = new List<string>();
            cn = GetStringChiNhanh();
            int count = 0;
            while (count < cn.Count())
            {
                cmbChiNhanh.Items.Add(cn[count++]);
            }
            cn = GetStringLoaiNha();
            count = 0;
            while(count < cn.Count())
            {
                cmbLoaiNha.Items.Add(cn[count++]);
            }    
            data.conn.Close();
            count = 1;
            while (count < 20)
            {
                cmbSoPhong.Items.Add((count++).ToString());
            }
            cmbChiNhanh.SelectedIndex = 0;
            cmbSoPhong.SelectedIndex = 0;
        }

        private List<string> GetStringChiNhanh()
        {
            List<string> input = new List<string>();
            query = "exec GetChiNhanh";
            data.conn = new SqlConnection(data.connStr);
            data.conn.Open();
            data.com = new SqlCommand(query, data.conn);
            data.read = data.com.ExecuteReader();
            while (data.read.Read())
            {
                input.Add(data.read[0].ToString());
            }
            data.conn.Close();
            return input;
        }

        private List<string> GetStringLoaiNha()
        {
            List<string> input = new List<string>();
            query = "select distinct(TENKIEUNHA) from LOAINHA";
            data.conn = new SqlConnection(data.connStr);
            data.conn.Open();
            data.com = new SqlCommand(query, data.conn);
            data.read = data.com.ExecuteReader();
            while (data.read.Read())
            {
                input.Add(data.read[0].ToString());
            }
            data.conn.Close();
            return input;
        }

        public List<string> GetList(string que)
        {
            List<string> input = new List<string>();
            DataTable dt = new DataTable();
            dt = data.ExcuteQuery(que);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                input.Add(dt.Columns[i].Caption);
            }
            return input;
        }
    }
}
