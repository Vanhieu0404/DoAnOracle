using System;
using System.Windows.Forms;
namespace Login
{
    public partial class TrangChinh : Form
    {
        public TrangChinh()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if(currentFormChild!=null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TrangChinh_Load(object sender, EventArgs e)
        {
                
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_TramThuPhi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TramThuPhi());
        }

        private void btn_TinhThanh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TinhThanh());
        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhachHang());
        }

        private void btn_Thetag_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TheTagg());
        }

        private void btn_VeThu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new VeThu());
        }

        private void btn_Xe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Xe());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Policy());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.TableSpace());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.Profile());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.User());
        }
    }
}
