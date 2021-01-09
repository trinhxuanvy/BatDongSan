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
    public partial class fModifyOwners : Form
    {
        DataProvider data = new DataProvider();
        Tools tool = new Tools();
        string query = "";

        public fModifyOwners()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string idcn = "", dc = "", sdt = "", ten = "", loai = "null";
            if (txtChuNha.Text != "")
            {
                ten = txtChuNha.Text;
            }
            if (txtSDT.Text != "")
            {
                sdt = txtSDT.Text;
            }
            if (txtDiaChi.Text != "")
            {
                dc = txtDiaChi.Text;
            }
            if (cmbLoaiCN.Text == "Cá Nhân")
            {
                loai = "'1'";
            }
            else if(cmbLoaiCN.Text == "Doanh Nghiệp")
            {
                loai = "'2'";
            }
            if (txtMaCN.Text == "")
            {
                query = "exec InsertChuNha N'" + ten + "', N'" + dc + "', '" + sdt + "', " + loai;
            }
            else
            {
                idcn = txtMaCN.Text;
                query = "exec UpdateChuNha '" + idcn + "', N'" + ten + "', N'" + dc + "', '" + sdt + "', " + loai;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.tool.ClearGroup(this.Controls);
            cmbLoaiCN.SelectedIndex = -1;
        }

        private void txtMaCN_KeyUp(object sender, KeyEventArgs e)
        {
            string idcn = "";
            if (txtMaCN.Text != "")
            {
                idcn = txtMaCN.Text;
            }
            else
            {
                tool.ClearGroup(this.Controls);
                return;
            }
            if (tool.CheckInput(idcn) == false)
            {
                MessageBox.Show("Mã là chuỗi số", "Cảnh báo");
                return;
            }
            try
            {
                query = "select * from ChuNha where IDCHUNHA = " + idcn;
                DataTable temp = new DataTable();
                temp = data.ExcuteQuery(query);
                List<string> input = new List<string>();
                for (int i = 1; i < temp.Columns.Count; i++)
                {
                    input.Add(temp.Rows[0][i].ToString());
                }
                txtChuNha.Text = input[0];
                txtDiaChi.Text = input[1];
                txtSDT.Text = input[2];
                if (input[3] == "1")
                {
                    cmbLoaiCN.Text = "Cá Nhân";
                }
                else if(input[3] == "2")
                {
                    cmbLoaiCN.Text = "Doanh Nghiệp";
                }    
            }
            catch
            {
                this.tool.ClearGroup(this.Controls);
                cmbLoaiCN.SelectedIndex = -1;
            }
        }
    }
}
