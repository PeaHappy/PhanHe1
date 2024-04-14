﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using PhanHe1.DAO;
using System.Runtime.InteropServices;
using System.Collections;

namespace PhanHe1
{
    public partial class Grant : Form
    {
        string labelName = "";

        public Grant()
        {
            InitializeComponent();
        }

        private void Grant_Load(object sender, EventArgs e)
        {

        }

        private void radioButtonRU_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonRU.Checked)
            {
                panelNotRU.Hide(); 
                panel2.Show();
            }
        }

        private void radioButtonRole_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRole.Checked)
            {
                labelName = "Tên Role: ";
                changeLabelName(labelName);
                panelNotRU.Show();
                panel2.Hide();


            }

        }

        private void radioButtonUser_CheckedChanged(object sender, EventArgs e)
        {
            if( radioButtonUser.Checked)
            {
                labelName = "Tên User: ";
                changeLabelName(labelName);
                panelNotRU.Show();
                panel2.Hide();


            }

        }
        private void changeLabelName(string t)
        {
            labelNameG.Text = t;
        }


        private void labelNameG_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxOpion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panelOption_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelOption_Click(object sender, EventArgs e)
        {

        }

        private void buttonGrant_Click(object sender, EventArgs e)
        {
            if (radioButtonRole.Checked || radioButtonUser.Checked)
            {

                string name = textNameG.Text.Trim();
                string table = textBoxTable.Text.Trim(); ;
                string option = checkBoxOpion.Checked ? "with grant option" : "";
                string right = "";
                string spe_right = "";
                string col = "";

                if (checkBoxI.Checked)
                {
                    right += "INSERT";
                }
                if (checkBoxD.Checked)
                {
                    right += (right != "" ? ", " : "") + "DELETE";
                }
                if (checkBoxS.Checked)
                {
                    right += (right != "" ? ", " : "") + "SELECT";
                }
                if (checkBoxU.Checked)
                {
                    spe_right += "SELECT, UPDATE";
                    col = textBoxForU.Text;

                }

                // Kiểm tra xem chuỗi quyền có rỗng không
                if (right != "" || spe_right != "")
                {
                    string spe_grantQuery = "";
                    string grantQuery = "";
                    if (right != "")
                    {
                        grantQuery = "GRANT " + right + " ON sys." + table + " TO " + name + " " + option ;
                       MessageBox.Show(grantQuery);
                        try
                        {
                            OracleDataProvider.Instance.ExecuteNonQuery(grantQuery);

                            MessageBox.Show("Quyền đã được cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    if (spe_right != "" && col != "")
                    {
                        spe_grantQuery = "GRANT " + spe_right + " (" + col + ")" + " ON sys." + table + " TO " + name + " " + option;
                        MessageBox.Show(spe_grantQuery);

                        try
                        {
                            OracleDataProvider.Instance.ExecuteNonQuery(spe_grantQuery);
                            MessageBox.Show("Quyền đã được cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (spe_right != "")
                    {
                        MessageBox.Show("Vui lòng nhập cột muốn cấp quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }




            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBoxU_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxU.Checked)
            {
                panel1.Show();  
            }
            else
            { 
                panel1.Hide();
            }  
        }

        private void textBoxForU_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelNotRU_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxRU_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonRU_Click(object sender, EventArgs e)
        {
            if (radioButtonRU.Checked)
            {

                string name = textNameG.Text.Trim();
                string role = textBoxRU.Text.Trim();
                if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Vui lòng điền đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string grantQuery = $"GRANT {role} TO {name}";
                    try
                    {
                        OracleDataProvider.Instance.ExecuteNonQuery(grantQuery);
                        MessageBox.Show("Quyền đã được cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
