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
using QlyBDS.DAO;

namespace QlyBDS
{
    public partial class fManager : Form
    {
        DataProvider data = new DataProvider();
        Tools tool = new Tools();
        string query = "";

        public fManager()
        {
            InitializeComponent();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private List<string> GetList(string que)
        {
            List<string> input = new List<string>();
            DataTable dt = new DataTable();
            dt = data.ExcuteQuery(que);
            for(int i = 0; i < dt.Columns.Count; i++)
            {
                input.Add(dt.Columns[i].Caption);
            }    
            return input;
        }

        private void fManager_Load(object sender, EventArgs e)
        {
           
        }

        private void button28_Click(object sender, EventArgs e)
        {
            fModifyStaff fmf = new fModifyStaff();
            this.Hide();
            fmf.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void fManager_Load_1(object sender, EventArgs e)
        {
            query = "select * from ChuNha";
            dgvKH_CN.DataSource = data.ExcuteQuery(query);
            query = "select * from NhaBan";
            dgvSP_NB.DataSource = data.ExcuteQuery(query);
            query = "select * from NhaThue";
            dgvSP_NT.DataSource = data.ExcuteQuery(query);
            query = "select * from NhaBan where IDNhanVien = null";
            dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
            query = "select * from NhaThue where IDNhanVien = null";
            dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
            query = "select * from HopDongMua";
            dgvQLGD_HDM.DataSource = data.ExcuteQuery(query);
            query = "select * from HopDongThue";
            dgvQLGD_HDT.DataSource = data.ExcuteQuery(query);
            query = "select * from NhanVien";
            dgvNV.DataSource = data.ExcuteQuery(query);
            cmbNV_TimKiem.DataSource = GetList("select * from NhanVien");
            cmbQLGD_TimKiem.DataSource = GetList("select * from HopDongMua");
            cmbQLGD_HDT.DataSource = GetList("select * from HopDongThue");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            fModifyStaff fmf = new fModifyStaff();
            this.Hide();
            fmf.ShowDialog();
            this.Show();
        }

        private void btnNV_TimKiem_Click(object sender, EventArgs e)
        {
            string type = cmbNV_TimKiem.Text, input = txtNV_TimKiem.Text;
            if(input != "")
            {
                if (type == "NGAYSINH")
                {
                    query = "select * from NhanVien where " + type + " = '" + input + "'";
                }
                else
                {
                    query = "select * from NhanVien where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%'";
                }
                try
                {
                    dgvNV.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                } 
            }
            else
            {
                query = "select * from NhanVien";
                dgvNV.DataSource = data.ExcuteQuery(query);
            }
        }

        private void btnQLGD_TimKiem_Click(object sender, EventArgs e)
        {
            string type = cmbQLGD_TimKiem.Text, input = txtQLGD_TimKiem.Text;
            if(input != "")
            {
                if (type == "NGAYLAP" || type == "NGAYHIEULUC")
                {
                    query = "select * from HopDongMua where " + type + " = '" + input + "'";
                }
                else
                {
                    query = "select * from HopDongMua where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%'";
                }
                try
                {
                    dgvQLGD_HDM.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                query = "select * from HopDongMua";
                dgvQLGD_HDM.DataSource = data.ExcuteQuery(query);
            }
        }

        private void txtNV_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                EventArgs newE = new EventArgs();
                btnNV_TimKiem_Click(sender, newE);
            }
        }

        private void txtQLGD_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EventArgs newE = new EventArgs();
                btnQLGD_TimKiem_Click(sender, newE);
            }
        }

        private void btnQLGD_HDT_TimKiem_Click(object sender, EventArgs e)
        {
            string type = cmbQLGD_HDT.Text, input = txtQLGD_HDT.Text;
            if(input != "")
            {
                if (type == "NGAYBATDAU" || type == "NGAYKETTHUC" || type == "NGAYLAP")
                {
                    query = "select * from HopDongThue where " + type + " = '" + input + "'";
                }
                else
                {
                    query = "select * from HopDongThue where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%'";
                }
                try
                {
                    dgvQLGD_HDT.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                query = "select * from HopDongThue";
                dgvQLGD_HDT.DataSource = data.ExcuteQuery(query);
            }
        }

        private void txtQLGD_HDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EventArgs newE = new EventArgs();
                btnQLGD_HDT_TimKiem_Click(sender, newE);
            }
        }

        private void btnQLGD_HDB_Click(object sender, EventArgs e)
        {
            fModifyContract fmc = new fModifyContract();
            this.Hide();
            fmc.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void btnQLGD_HDT_Click(object sender, EventArgs e)
        {
            fModifyContract fmc = new fModifyContract();
            this.Hide();
            fmc.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            fModifyRentContract fmc = new fModifyRentContract();
            this.Hide();
            fmc.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void btnQLGD_HDT_Sua_Click(object sender, EventArgs e)
        {
            fModifyRentContract fmc = new fModifyRentContract();
            this.Hide();
            fmc.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }
    }
}
