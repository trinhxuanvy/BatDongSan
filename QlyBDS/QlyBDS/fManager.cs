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
            fModifyOwners fmo = new fModifyOwners();
            this.Hide();
            fmo.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        public List<string> GetList(string que)
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
            dgvKH_CN.DataSource = data.ExcuteQuery("select * from ChuNha");
            dgvSP_NB.DataSource = data.ExcuteQuery("select * from NhaBan");
            dgvSP_NT.DataSource = data.ExcuteQuery("select * from NhaThue");
            dgvSP_NCD_NB.DataSource = data.ExcuteQuery("select * from NhaBan where IDNhanVien is null");
            dgvSP_NCD_NT.DataSource = data.ExcuteQuery("select * from NhaThue where IDNhanVien is null");
            dgvSP_NB.DataSource = data.ExcuteQuery("select * from NhaBan where IDNhanVien is not null");
            dgvSP_NT.DataSource = data.ExcuteQuery("select * from NhaThue where IDNhanVien is not null");
            dgvQLGD_HDM.DataSource = data.ExcuteQuery("select * from HopDongMua");
            dgvQLGD_HDT.DataSource = data.ExcuteQuery("select * from HopDongThue");
            dgvNV.DataSource = data.ExcuteQuery("select * from NhanVien");
            dgvKH_MT.DataSource = data.ExcuteQuery("select * from KhachHang");
            cmbNV_TimKiem.DataSource = GetList("select * from NhanVien");
            cmbQLGD_TimKiem.DataSource = GetList("select * from HopDongMua");
            cmbQLGD_HDT.DataSource = GetList("select * from HopDongThue");
            cmbSP_NB_Search.DataSource = GetList("select * from NhaBan");
            cmbSP_NT_TimKiem.DataSource = GetList("select * from NhaThue");
            cmbKH_CN_TimKiem.DataSource = GetList("select * from ChuNha");
            cmbKH_MT_TimKiem.DataSource = GetList("select * from KhachHang");
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

        private void btnSP_NB_Them_Click(object sender, EventArgs e)
        {
            fModifyProduct fmp = new fModifyProduct();
            this.Hide();
            fmp.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void txtSP_NT_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EventArgs newE = new EventArgs();
                btnSP_NT_TimKiem_Click(sender, newE);
            }
        }

        private void btnSP_NT_TimKiem_Click(object sender, EventArgs e)
        {
            string type = cmbSP_NT_TimKiem.Text, input = txtSP_NT_TimKiem.Text;
            if (input != "")
            {
                if (type == "NGAYDANG" || type == "NGAYHETHAN")
                {
                    query = "select * from NhaThue where " + type + " = '" + input + "' and IDNhanVien is not null";
                }
                else
                {
                    query = "select * from NhaThue where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%' and IDNhanVien is not null";
                }
                try
                {
                    dgvSP_NT.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                query = "select * from NhaThue where IDNhanVien is not null";
                dgvSP_NT.DataSource = data.ExcuteQuery(query);
            }
        }

        private void btnSP_NB_Search_Click(object sender, EventArgs e)
        {
            string type = cmbSP_NB_Search.Text, input = txtSP_NB_Search.Text;
            if (input != "")
            {
                if (type == "NGAYDANG" || type == "NGAYHETHAN")
                {
                    query = "select * from NhaBan where " + type + " = '" + input + "' and IDNhanVien is not null";
                }
                else
                {
                    query = "select * from NhaBan where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%' and IDNhanVien is not null";
                }
                try
                {
                    dgvSP_NB.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                query = "select * from NhaBan where IDNhanVien is not null";
                dgvSP_NB.DataSource = data.ExcuteQuery(query);
            }
        }

        private void txtSP_NB_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EventArgs newE = new EventArgs();
                btnSP_NB_Search_Click(sender, newE);
            }
        }

        private void cmbSP_NCD_TimKiem_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSP_NCD_TimKiem_1.Text == "Nhà Bán")
            {
                query = "select * from NhaBan";
            }
            else
            {
                query = "select * from NhaThue";
            }
            List<string> temp = new List<string>();
            temp = GetList(query);
            for(int i = 0; i < temp.Count; i++)
            {
                if(temp[i] == "IDNHANVIEN")
                {
                    temp.RemoveAt(i);
                }    
            }    
            cmbSP_NCD_TimKiem_2.DataSource = temp;
        }

        private void btnSP_NCD_TimKiem_Click(object sender, EventArgs e)
        {
            string type1 = cmbSP_NCD_TimKiem_1.Text, type2 = cmbSP_NCD_TimKiem_2.Text, input = txtSP_NCD_TimKiem.Text;
            if (input != "")
            {
                if(type1 == "Nhà Bán")
                {
                    if (type2 == "NGAYDANG" || type2 == "NGAYHETHAN")
                    {
                        query = "select * from NhaBan where " + type2 + " = '" + input + "' and IDNhanVien is null";
                    }
                    else
                    {
                        query = "select * from NhaBan where " + type2 + " like '%' + dbo.NonUnicode(N'" + input + "') + '%' and IDNhanVien is null";
                    }
                    try
                    {
                        dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
                    }
                    catch
                    {
                        return;
                    }
                }    
                else
                {
                    if (type2 == "NGAYDANG" || type2 == "NGAYHETHAN")
                    {
                        query = "select * from NhaThue where " + type2 + " = '" + input + "' and IDNhanVien is null";
                    }
                    else
                    {
                        query = "select * from NhaThue where " + type2 + " like '%' + dbo.NonUnicode(N'" + input + "') + '%' and IDNhanVien is null";
                    }
                    try
                    {
                        dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
                    }
                    catch
                    {
                        return;
                    }
                }    
            }
            else
            {
                if(type1 == "Nhà Bán")
                {
                    query = "select * from NhaBan where IDNhanVien is null";
                    dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
                }
                else
                {
                    query = "select * from NhaThue where IDNhanVien is null";
                    dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
                }             
            }
        }

        private void txtSP_NCD_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EventArgs newE = new EventArgs();
                btnSP_NCD_TimKiem_Click(sender, newE);
            }
        }

        private void txtSP_NCD_MaNha_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            string type1 = cmbSP_NCD_Duyet.Text, idnha = txtSP_NCD_MaNha.Text, idnv = txtSP_NCD_MaNV.Text;
            if (idnha != "" && idnv != "")
            {
                if (type1 == "Nhà Bán")
                {
                    query = "exec DuyetNhaBan '" + idnha + "', '" + idnv + "'";
                    try
                    {
                        dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
                    }
                    catch
                    {
                        MessageBox.Show("Không thành công", "Thông báo");
                        return;
                    }
                }
                else if(type1 == "Nhà Thuê")
                {
                    query = "exec DuyetNhaThue '" + idnha + "', '" + idnv + "'";
                    try
                    {
                        dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
                    }
                    catch
                    {
                        MessageBox.Show("Không thành công", "Thông báo");
                        return;
                    }
                } 
            }
            else
            {
                if (type1 == "Nhà Bán")
                {
                    query = "select * from NhaBan";
                    dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
                }
                else if(type1 == "Nhà Thuê")
                {
                    query = "select * from NhaThue";
                    dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
                }
            }
            fManager_Load_1(sender, e);
        }

        private void txtSP_NCD_MaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKH_CN_Sua_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void txtKH_CN_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                EventArgs newe = new EventArgs();
                btnKH_CN_TimKiem_Click(sender, newe);
            }
        }

        private void btnKH_CN_TimKiem_Click(object sender, EventArgs e)
        {
            string type = cmbKH_CN_TimKiem.Text, input = txtKH_CN_TimKiem.Text;
            if (input != "")
            {
                query = "select * from ChuNha where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%'";
                try
                {
                    dgvKH_CN.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                query = "select * from ChuNha";
                dgvKH_CN.DataSource = data.ExcuteQuery(query);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            fModifyCustomer fmc = new fModifyCustomer();
            this.Hide();
            fmc.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void btnKH_MT_Sua_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        private void txtKH_MT_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EventArgs newe = new EventArgs();
                btnKH_MT_TimKiem_Click(sender, newe);
            }
        }

        private void btnKH_MT_TimKiem_Click(object sender, EventArgs e)
        {
            string type = cmbKH_MT_TimKiem.Text, input = txtKH_MT_TimKiem.Text;
            if (input != "")
            {
                query = "select * from KhachHang where " + type + " like '%' + dbo.NonUnicode(N'" + input + "') + '%'";
                try
                {
                    dgvKH_MT.DataSource = data.ExcuteQuery(query);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                query = "select * from KhachHang";
                dgvKH_MT.DataSource = data.ExcuteQuery(query);
            }
        }

        private void dgvKH_CN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvKH_CN.CurrentRow.Index;
            string val = dgvKH_CN.Rows[row].Cells[0].Value.ToString();
            try
            {
                dgvKH_CN_LSGD_NB.DataSource = data.ExcuteQuery("exec SelectHDMB 1, " + val);
                dgvKH_CN_LSGD_NT.DataSource = data.ExcuteQuery("exec SelectHDMB 2, " + val);
            }
            catch
            {
                return;
            }          
        }

        private void dgvKH_MT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvKH_MT.CurrentRow.Index;
            string val = dgvKH_MT.Rows[row].Cells[0].Value.ToString();
            try
            {
                dgvKH_CN_LSGD_NB.DataSource = data.ExcuteQuery("exec SelectHDMBMT 1, " + val);
                dgvKH_CN_LSGD_NT.DataSource = data.ExcuteQuery("exec SelectHDMBMT 2, " + val);
            }
            catch
            {
                return;
            }
        }

        private void txtSP_NCD_MaNha_KeyUp(object sender, KeyEventArgs e)
        {
            string type1 = cmbSP_NCD_Duyet.Text, idnha = "";
            if (txtSP_NCD_MaNha.Text != "" && type1 != "")
            {
                idnha = txtSP_NCD_MaNha.Text;
                if (type1 == "Nhà Bán")
                {
                    query = "select * from NhaBan where IDNHABAN like '%' + '" + idnha + "' + '%' and IDNHANVIEN is null";
                    try
                    {
                        dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
                        if(dgvSP_NCD_NB.Rows.Count == 1)
                        {
                            txtSP_NCD_MaNha.Text = "";
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
                else if (type1 == "Nhà Thuê")
                {
                    query = "select * from NhaThue where IDNHATHUE like '%' + '" + idnha + "' + '%' and IDNHANVIEN is null";
                    try
                    {
                        dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
                        if (dgvSP_NCD_NT.Rows.Count == 1)
                        {
                            txtSP_NCD_MaNha.Text = "";
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
            }
            else
            {
                if (type1 == "Nhà Bán")
                {
                    query = "select * from NhaBan where IDNhanVien is null";
                    dgvSP_NCD_NB.DataSource = data.ExcuteQuery(query);
                }
                else if (type1 == "Nhà Thuê")
                {
                    query = "select * from NhaThue where IDNhanVien is null";
                    dgvSP_NCD_NT.DataSource = data.ExcuteQuery(query);
                }
            }
        }

        private void dgvSP_NCD_NB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvSP_NCD_NB.CurrentRow.Index;
            string val = dgvSP_NCD_NB.Rows[row].Cells[0].Value.ToString();
            try
            {
                dgvSP_LSGD.DataSource = data.ExcuteQuery("exec SelectHDNBT 1, " + val);
            }
            catch
            {
                return;
            }
        }

        private void dgvSP_NCD_NT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvSP_NCD_NT.CurrentRow.Index;
            string val = dgvSP_NCD_NT.Rows[row].Cells[0].Value.ToString();
            try
            {
                dgvSP_LSGD.DataSource = data.ExcuteQuery("exec SelectHDNBT 2, " + val);
            }
            catch
            {
                return;
            }
        }

        private void dgvSP_NB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvSP_NB.CurrentRow.Index;
            string val = dgvSP_NB.Rows[row].Cells[0].Value.ToString();
            try
            {
                dgvSP_LSGD.DataSource = data.ExcuteQuery("exec SelectHDNBT 1, " + val);
                dgvSP_LSSH.DataSource = data.ExcuteQuery("exec SelectKH 1, " + val);
                dgvSP_NVQL.DataSource = data.ExcuteQuery("exec SelectNV 1, " + val);
            }
            catch
            {
                return;
            }
        }

        private void dgvSP_NT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvSP_NT.CurrentRow.Index;
            string val = dgvSP_NT.Rows[row].Cells[0].Value.ToString();
            try
            {
                dgvSP_LSGD.DataSource = data.ExcuteQuery("exec SelectHDNBT 2, " + val);
                dgvSP_LSSH.DataSource = data.ExcuteQuery("exec SelectKH 2, " + val);
                dgvSP_NVQL.DataSource = data.ExcuteQuery("exec SelectNV 2, " + val);
                dgvSP_NX.DataSource = data.ExcuteQuery("select * from NhanXetNhaThue where IDNhaThue = " + val);
            }
            catch
            {
                return;
            }
        }

        private void txtSP_NCD_MaNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                EventArgs newe = new EventArgs();
                btnDuyet_Click(sender, newe);
            }    
        }

        private void btnSP_NB_Sua_Click(object sender, EventArgs e)
        {
            btnSP_NB_Them_Click(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            fModifyRentProduct fmp = new fModifyRentProduct();
            this.Hide();
            fmp.ShowDialog();
            fManager_Load_1(sender, e);
            this.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button16_Click(sender, e);
        }
    }
}
