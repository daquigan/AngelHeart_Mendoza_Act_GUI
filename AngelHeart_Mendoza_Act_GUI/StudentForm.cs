using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngelHeart_Mendoza_Act_GUI
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
            // Mock Student Data
            StudentNameLabel.Text = "Angel Heart Mendoza";
            StudentAgeLabel.Text = "21";
            StudentAddressLabel.Text = "Poponto Pangasinan";
            StudentContactLabel.Text = "09194449596";
            StudentEmailLabel.Text = "heart@gmail.com";
            StudentCourseYearLabel.Text = "BSIT Third";
            ParentNameLabel.Text = "Luvie Mendoza";
            ParentContactLabel.Text = "09127653921";
            HobbiesLabel.Text = "Singing, Watching Kdramas";
            NicknameLabel.Text = "Mes Hart";
        }

        private void AddImageBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            StudentPictureBox.Image = Image.FromFile(openFileDialog.FileName);
        }

        private void ChangeImageBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Change Image functionality is not implemented.", "Feature Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditUpdateBtn_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm(this);
            this.Hide();
            editForm.Show();
        }

    }
}
