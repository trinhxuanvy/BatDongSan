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

namespace QlyBDS
{
    public partial class fModifyContract : Form
    {
        DataProvider data = new DataProvider();
        Tools tool = new Tools();
        string query = "";
        public fModifyContract()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string idhd = "", idkh = "", idnha = "", nhl = dpNgayHieuLuc.Value.ToString(), nl = dpNgayLap.Value.ToString();
            if (txtMaKhachHang.Text != "")
            {
                idkh = txtMaKhachHang.Text;
            }
            if (txtMaNha.Text != "")
            {
                idnha = txtMaNha.Text;
            }
            if (txtMaHopDong.Text == "")
            {
                query = "exec InsertHDM '" + idkh + "', '" + idnha + "', '" + nhl + "', '" + nl + "'";
            }
            else
            {
                idhd = txtMaHopDong.Text;
                query = "exec UpdateHDM '" + idhd + "', '" + idkh + "', '" + idnha + "', '" + nhl + "', '" + nl + "'";
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.tool.ClearGroup(this.Controls);
        }

        private void txtMaHopDong_KeyUp(object sender, KeyEventArgs e)
        {
            string idhd = "";
            if (txtMaHopDong.Text != "")
            {
                idhd = txtMaHopDong.Text;
            }
            else
            {
                tool.ClearGroup(this.Controls);
                return;
            }
            if (tool.CheckInput(idhd) == false)
            {
                MessageBox.Show("Mã là chuỗi số", "Cảnh báo");
                return;
            }
            try
            {
                query = "select * from HopDongMua where IDHOPDONG = " + idhd;
                DataTable temp = new DataTable();
                temp = data.ExcuteQuery(query);
                List<string> input = new List<string>();
                for (int i = 1; i < temp.Columns.Count; i++)
                {            
                    input.Add(temp.Rows[0][i].ToString());
                }
                txtMaKhachHang.Text = input[0];
                txtMaNha.Text = input[1];
                dpNgayHieuLuc.Text = input[2];
                dpNgayLap.Text = input[3];
            }
            catch
            {
                this.tool.ClearGroup(this.Controls);
            }
        }
    }
}
