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
    public partial class fModifyCustomer : Form
    {
        DataProvider data = new DataProvider();
        Tools tool = new Tools();
        string query = "";

        public fModifyCustomer()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string idkh = "", ht = "", sdt = "", dc = "", yc = "";
            if (txtTen.Text != "")
            {
                ht = txtTen.Text;
            }
            if (txtDiaChi.Text != "")
            {
                dc = txtDiaChi.Text;
            }
            if (txtSDT.Text != "")
            {
                sdt = txtSDT.Text;
            }
            if(rtxtYeuCau.Text != "")
            {
                yc = rtxtYeuCau.Text;
            }
            if (txtMaKhach.Text == "")
            {
                query = "exec InsertKhachHang N'" + ht + "', N'" + dc + "', '" + sdt + "', N'" + yc + "'";
            }
            else
            {
                idkh = txtMaKhach.Text;
                query = "exec UpdateKhachHang '" + idkh + "', N'" + ht + "', N'" + dc + "', '" + sdt + "', N'" + yc + "'";
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
            rtxtYeuCau.Text = "";
        }

        private void txtMaKhach_KeyUp(object sender, KeyEventArgs e)
        {
            string idkh = "";
            if (txtMaKhach.Text != "")
            {
                idkh = txtMaKhach.Text;
            }
            else
            {
                tool.ClearGroup(this.Controls);
                rtxtYeuCau.Text = "";
                return;
            }
            if (tool.CheckInput(idkh) == false)
            {
                MessageBox.Show("Mã là chuỗi số", "Cảnh báo");
                this.tool.ClearGroup(this.Controls);
                return;
            }
            try
            {
                query = "select * from KhachHang where IDKHACHHANG = " + idkh;
                DataTable temp = new DataTable();
                temp = data.ExcuteQuery(query);
                List<string> input = new List<string>();
                for (int i = 1; i < temp.Columns.Count; i++)
                {
                    input.Add(temp.Rows[0][i].ToString());
                }
                txtTen.Text = input[0];
                txtDiaChi.Text = input[1];
                txtSDT.Text = input[2];
                rtxtYeuCau.Text = input[3];
            }
            catch
            {
                this.tool.ClearGroup(this.Controls);
                rtxtYeuCau.Text = "";
            }
        }
    }
}
