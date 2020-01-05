﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEPFrameWork.Databases;

namespace SEPFrameWork.Forms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public BaseForm(IConnector dbConn, string dbName, string tabName,string windowsName)
        {
            InitializeComponent();
            databaseConnection = dbConn;
            tableName = tabName;
            databaseName = dbName;
            this.windowsName = windowsName;
            Load();
        }

        private string databaseName, tableName,windowsName;
        private IConnector databaseConnection = null;
        private List<string> fields = null;
        private List<string> fieldsAuto = null;
        private List<string> fieldsPrimary = null;

        #region Load
        private void Load()
        {
            this.Text = windowsName;
            lblHeader.Text = windowsName + " BẢNG";
            fields = databaseConnection.GetNameFieldsOfTable(tableName);
            fieldsAuto = databaseConnection.GetFieldsAutoIncrement(tableName);
            fieldsPrimary = databaseConnection.GetPrimaryKeyOfTable(tableName);
            int x = 50, y = 100;
            int idx = 0;
            ToolTip tt = new ToolTip();

            foreach (var field in fields)
            {
                // Add label
                Label lbl = new Label();
                lbl.Text = field;
                lbl.Size = new Size(180, 30);
                lbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.Location = new System.Drawing.Point(x , y + idx*50);
                lbl.Name = "lbl"+idx;
                this.Controls.Add(lbl);

                // Add textbox
                TextBox txt = new TextBox();
                txt.Text = "";
                txt.Size = new Size(300, 30);
                txt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                txt.Location = new System.Drawing.Point(x + 250, y + idx * 50);
                txt.Name = "txt" + idx;

                if (fieldsAuto.Contains(field, StringComparer.OrdinalIgnoreCase))
                {
                    txt.Enabled = false;
                    txt.ForeColor = Color.Red;
                    PictureBox pbAuto = new PictureBox();
                    pbAuto.Size = new Size(25, 25);
                    pbAuto.Location = new System.Drawing.Point(x + 210, y + idx * 50);
                    Bitmap pic1 = new Bitmap(Application.StartupPath + "\\Icon\\autoKey.png");
                    pbAuto.Image = pic1;
                    pbAuto.SizeMode = PictureBoxSizeMode.StretchImage;
                    tt.SetToolTip(pbAuto, "Thuộc tính tự tăng, không thể sửa giá trị");
                    this.Controls.Add(pbAuto);
                }
                if (fieldsPrimary.Contains(field,StringComparer.OrdinalIgnoreCase))
                {
                    lbl.ForeColor = Color.Red;
                    txt.Enabled = false;
                    txt.ForeColor = Color.Red;
                    PictureBox pbPrimary = new PictureBox();
                    pbPrimary.Size = new Size(25, 25);
                    pbPrimary.Location = new System.Drawing.Point(x + 180, y + idx * 50);
                    Bitmap pic2 = new Bitmap(Application.StartupPath + "\\Icon\\primaryKey.png");
                    pbPrimary.Image = pic2;
                    pbPrimary.SizeMode = PictureBoxSizeMode.StretchImage;
                    tt.SetToolTip(pbPrimary, "Thuộc tính primary, không thể sửa giá trị");
                    this.Controls.Add(pbPrimary);
                }
                this.Controls.Add(txt);

                idx++;
            }

            Button btn = new Button();
            btn.BackColor = System.Drawing.SystemColors.HotTrack;
            btn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btn.Location = new System.Drawing.Point(x + 190 , y + idx * 50);
            btn.Name = "btn";
            btn.Size = new System.Drawing.Size(130, 45);
            btn.Text = windowsName;
            btn.UseVisualStyleBackColor = false;
            btn.Click += new System.EventHandler(btn_Click);
            this.Controls.Add(btn);

        }
        #endregion

        #region Event
        private void btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Clicked!");
        }
        #endregion

    }
}
