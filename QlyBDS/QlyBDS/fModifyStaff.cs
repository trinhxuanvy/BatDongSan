using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QlyBDS.DAO;

namespace QlyBDS
{
    public partial class fModifyStaff : Form
    {
        DataProvider data = new DataProvider();
        Tools tool = new Tools();
        string query;

        public string GetChiNhanh(string que)
        {
            string result = "";
            for(int i = 0; i < que.Length; i++)
            {
                if(que[i] >= '0' && que[i] <= '9')
                {
                    result += que[i];
                }
                else if(que[i] == ',')
                {
                    break;
                }    
                
            }    
            return result;
        }

        public fModifyStaff()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idnv = "", idcn = "", ten = "", sdt = "", gt = "", ns = dpNgaySinh.Value.ToString(), luong = "", dc = "";
            if (this.cmbChiNhanh.Text != "")
            {
                idcn = GetChiNhanh(this.cmbChiNhanh.Text);
            }
            if (txtHoVaTen.Text != "")
            {
                ten = txtHoVaTen.Text;
            }
            if (txtSDT.Text != "")
            {
                sdt = txtSDT.Text;
            }
            if (this.cmbGioiTinh.Text != "")
            {
                gt = cmbGioiTinh.Text;
            }
            if (txtLuong.Text != "")
            {
                luong = txtLuong.Text;
            }
            if (txtDiaChi.Text != "")
            {
                dc = txtDiaChi.Text;
            }
            if (txtMaNhanVien.Text == "")
            {
                query = "exec InsertNhanVien '" + idcn + "', N'" + ten + "', '" + sdt + "', N'" + gt + "', '" + ns + "', '" + luong + "', N'" + dc + "'";
            }
            else {
                idnv = txtMaNhanVien.Text;
                query = "exec UpdateNhanVien '" + idnv + "', '" + idcn + "', N'" + ten + "', '" + sdt + "', N'" + gt + "', '" + ns + "', '" + luong + "', N'" + dc + "'";
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

        private void fModifyStaff_Load(object sender, EventArgs e)
        {
            List<string> cn = new List<string>();
            cn = GetStringChiNhanh();
            int count = 0;
            while (count < cn.Count())
            {
                cmbChiNhanh.Items.Add(cn[count++]);
            }
            data.conn.Close();

            List<string> gt = new List<string>();
            gt.Add("Nam");
            gt.Add("Nữ");
            gt.Add("Khác");
            count = 0;
            while(count < gt.Count())
            {
                cmbGioiTinh.Items.Add(gt[count++]);
            }
            cmbChiNhanh.SelectedIndex = -1;
            cmbGioiTinh.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.tool.ClearGroup(this.Controls);
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            string idnv = "";
            if(txtMaNhanVien.Text != "")
            {
                idnv = txtMaNhanVien.Text;
            }
            else
            {
                tool.ClearGroup(this.Controls);
                cmbGioiTinh.Text = "";
                return;
            }
            if(tool.CheckInput(idnv) == false)
            {
                MessageBox.Show("Mã là chuỗi số", "Cảnh báo");
                return;
            }
            try
            {
                query = "select * from NhanVien where IDNHANVIEN = " + idnv;
                DataTable temp = new DataTable();
                temp = data.ExcuteQuery(query);
                List<string> input = new List<string>();
                for (int i = 1; i < temp.Columns.Count; i++)
                {
                    input.Add(temp.Rows[0][i].ToString());
                }
                List<string> getcn = new List<string>();
                getcn = GetStringChiNhanh();
                for (int i = 0; i < getcn.Count(); i++)
                {
                    if (GetChiNhanh(getcn[i]) == input[0])
                    {
                        cmbChiNhanh.Text = getcn[i];
                    }
                }
                txtHoVaTen.Text = input[1];
                txtSDT.Text = input[2];
                cmbGioiTinh.Text = input[3];
                dpNgaySinh.Text = input[4];
                txtLuong.Text = input[5];
                txtDiaChi.Text = input[6];
            }
            catch
            {
                EventArgs newe = new EventArgs();
                button3_Click(sender, newe);
            }
        }

    }
}
