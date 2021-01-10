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
            string mn = "", mnv = "", ln = "", mcn = "", cn = "", sp = "", nd = dpNgayDang.Value.ToString(),
                dk = "", quan = "", duong = "", tp = "", kv = "", tt = "", nhh = dpNgayHetHan.Value.ToString(), gb = "", gt = "";
            if (txtMaNV.Text != "")
            {
                mnv = txtMaNV.Text;
            }
            if(cmbLoaiNha.Text != "")
            {
                ln = ConvertStringLoaiNha(cmbLoaiNha.Text);
            }
            if(txtMaCN.Text != "")
            {
                mcn = txtMaCN.Text;
            }
            if(cmbChiNhanh.Text != "")
            {
                cn = GetChiNhanh(cmbChiNhanh.Text);
            }
            if(cmbSoPhong.Text != "")
            {
                sp = cmbSoPhong.Text;
            }
            if(txtDieuKien.Text != "")
            {
                dk = txtDieuKien.Text;
            }
            if(txtQuan.Text != "")
            {
                quan = txtQuan.Text;
            }
            if(txtDuong.Text != "")
            {
                duong = txtDuong.Text;
            }
            if(txtKV.Text != "")
            {
                kv = txtKV.Text;
            }
            if(txtTP.Text != "")
            {
                tp = txtTP.Text;
            }
            if(cmbTT.Text != "")
            {
                tt = GetTinhTrang(cmbTT.Text, 1);
            }
            if(txtGiaBan.Text != "")
            {
                gb = txtGiaBan.Text;
            }
            if(txtGiaThue.Text != "")
            {
                gt = txtGiaThue.Text;
            }
            if (txtMaNha.Text == "")
            {
                query = "exec InsertNhaBan '" + mnv + "', '" + ln + "', '" + mcn + "', '" + cn + "', N'" + duong + "', N'" + 
                                    quan + "', N'" + kv + "', N'" + tp + "', '" + nd + "', '" + nhh + "', '" + sp + "', '" + tt + "', '" + gt + "', '" +
                                    gb + "', N'" + dk + "'";
            }
            else
            {
                mn = txtMaNha.Text;
                query = "exec UpdateNhaBan '" + mn + "', '" + mnv + "', '" + ln + "', '" + mcn + "', '" + cn + "', N'" + duong + "', N'" +
                                    quan + "', N'" + kv + "', N'" + tp + "', '" + nd + "', '" + nhh + "', '" + sp + "', '" + tt + "', '" + gt + "', '" +
                                    gb + "', N'" + dk + "'";
            }
            try
            {
                if (MessageBox.Show("Bạn có muốn lưu?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    data.ExcuteProc(query);
                    MessageBox.Show("Đã lưu", "Thông báo");
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi vui lòng kiểm tra lại!");
            }
        }

        private void fModifyProduct_Load(object sender, EventArgs e)
        {
            List<string> cn = new List<string>();
            cmbChiNhanh.DataSource = GetStringChiNhanh();
            cmbLoaiNha.DataSource = GetStringLoaiNha();  
            int count = 1;
            while (count < 20)
            {
                cmbSoPhong.Items.Add((count++).ToString());
            }
            List<string> tt = new List<string>();
            tt.Add("Còn trống");
            tt.Add("Đang thuê");
            tt.Add("Đã bán");
            cmbTT.DataSource = tt;
            cmbChiNhanh.SelectedIndex = -1;
            cmbSoPhong.SelectedIndex = -1;
            cmbTT.SelectedIndex = -1;
            cmbLoaiNha.SelectedIndex = -1;
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

        public string GetChiNhanh(string que)
        {
            string result = "";
            for (int i = 0; i < que.Length; i++)
            {
                if (que[i] >= '0' && que[i] <= '9')
                {
                    result += que[i];
                }
                else if (que[i] == ',')
                {
                    break;
                }

            }
            return result;
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

        private void txtMaNha_KeyUp(object sender, KeyEventArgs e)
        {
            string idnha = "";
            if (txtMaNha.Text != "")
            {
                idnha = txtMaNha.Text;
            }
            else
            {
                this.tool.ClearGroup(this.Controls);
                return;
            }
            if (tool.CheckInput(idnha) == false)
            {
                MessageBox.Show("Mã là chuỗi số", "Cảnh báo");
                this.tool.ClearGroup(this.Controls);
                return;
            }
            try
            {
                query = "select * from NhaBan where IDNhaBan = " + idnha;
                DataTable temp = new DataTable();
                temp = data.ExcuteQuery(query);
                List<string> input = new List<string>();
                for (int i = 1; i < temp.Columns.Count; i++)
                {
                    input.Add(temp.Rows[0][i].ToString());
                }
                txtMaNV.Text = input[0];
                cmbLoaiNha.Text = ConvertStringLoaiNha(input[1], 0);
                txtMaCN.Text = input[2];
                List<string> cn = new List<string>();
                cn = GetStringChiNhanh();
                for(int i = 0; i < cn.Count; i++)
                {
                    if(GetChiNhanh(cn[i]) == input[3])
                    {
                        input[3] = cn[i];
                        break;
                    }
                }
                cmbChiNhanh.Text = input[3];
                cmbSoPhong.Text = input[10];
                dpNgayDang.Text = input[8];
                txtDieuKien.Text = input[15];
                txtQuan.Text = input[5];
                txtDuong.Text = input[4];
                txtTP.Text = input[7];
                txtKV.Text = input[6];
                cmbTT.Text = GetTinhTrang(input[11]);
                dpNgayHetHan.Text = input[9];
                txtGiaBan.Text = input[14];
                txtGiaThue.Text = input[13];
            }
            catch
            {
                this.tool.ClearGroup(this.Controls);
            }
        }

        public string ConvertStringLoaiNha(string s, object type = null)
        {
            string result = "";
            if(type == null)
            {
                query = "select IDLOAINHA from LOAINHA where TENKIEUNHA = N'" + s + "'";
            }
            else
            {
                query = "select TENKIEUNHA from LOAINHA where IDLOAINHA = '" + s + "'";
            }
            data.conn = new SqlConnection(data.connStr);
            data.conn.Open();
            data.com = new SqlCommand(query, data.conn);
            data.read = data.com.ExecuteReader();
            while (data.read.Read())
            {
                result = data.read[0].ToString();
            }
            data.conn.Close();
            return result;
        }

        public string GetTinhTrang(string s, object type = null)
        {
            string result = "";
            if(type == null)
            {
                if (s == "0")
                {
                    result = "Còn trống";
                }
                else if (s == "1")
                {
                    result = "Đang thuê";
                }
                else if (s == "2")
                {
                    result = "Đã bán";
                }
            }
            else
            {
                if (s == "Còn trống")
                {
                    result = "0";
                }
                else if (s == "Đang thuê")
                {
                    result = "1";
                }
                else if (s == "Đã bán")
                {
                    result = "2";
                }
            }
            return result;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.tool.ClearGroup(this.Controls);
        }
    }
}
