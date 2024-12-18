﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH.lab02_02
{
    public partial class frmQuanLy : Form
    {
        private List<Faculty> listFaculty = new List<Faculty>();
        public frmQuanLy()
        {
            InitializeComponent();
        }
        public void AddDataToGrid(Faculty faculty)
        {
            listFaculty.Add(faculty);
            UpdateGridView(listFaculty);

        }
        private void UpdateGridView(List<Faculty> listFaculty)
        {
            dgvFaculty.Rows.Clear();
            foreach (var faculty in listFaculty)
            {
                int newRowIndex = dgvFaculty.Rows.Add();
                dgvFaculty.Rows[newRowIndex].Cells[0].Value = faculty.MaNganh;
                dgvFaculty.Rows[newRowIndex].Cells[1].Value = faculty.TenNganh;
            }
        }
        private void ClearInputFields()
        {
            txtMaNganh.Text = "";
            txtTenNganh.Text = "";
        }

        private int GetSelectedRow()
        {
            if (dgvFaculty.CurrentRow != null)
            {
                return dgvFaculty.CurrentRow.Index;
            }
            return -1; 
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            pn2.Enabled = true;
            ClearInputFields();
        }

        public List<Faculty> DanhSachNganh(List<Faculty> listFaculty)
        {
            return listFaculty;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            pn2.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            pn2.Enabled = true;
            ClearInputFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvFaculty.CurrentRow != null)
            {
                int selectedRow = dgvFaculty.CurrentRow.Index;
                if (selectedRow >= 0)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này?", "Xác Nhận", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        listFaculty.RemoveAt(selectedRow);
                        dgvFaculty.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông Báo", MessageBoxButtons.OK);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            pn2.Enabled = false;
            try
            {
                if (txtMaNganh.Text == "" || txtTenNganh.Text == "")
                {
                    throw new Exception("Bắt Buộc Nhập Đầy Đủ Thông Tin!");
                }
                int selectedRow = GetSelectedRow();
                if (selectedRow >= 0)
                {
                    listFaculty[selectedRow].MaNganh = txtMaNganh.Text;
                    listFaculty[selectedRow].TenNganh = txtTenNganh.Text;

                    UpdateGridView(listFaculty);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
                else
                {
                    Faculty faculty = new Faculty
                    {
                        MaNganh = txtMaNganh.Text,
                        TenNganh = txtTenNganh.Text
                    };

                    AddDataToGrid(faculty);
                    MessageBox.Show("Thêm mới thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFaculty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int selectedRowIndex = e.RowIndex;

                string maNganh = dgvFaculty.Rows[selectedRowIndex].Cells[0].Value?.ToString();

                if (!string.IsNullOrEmpty(maNganh))
                {
                    MessageBox.Show($"Bạn đã chọn dòng có Mã Ngành: {maNganh}", "Thông Báo");
                }
            }
        }
    }
}
