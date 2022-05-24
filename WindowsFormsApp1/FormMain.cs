using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Data.Entity;

namespace WindowsFormsApp1
{
	public partial class FormMain : Form, IMessageFilter
    {
        const int extraWidth = 20;


        public class FormWrap
		{
			public Type m_frm = null;

			public FormWrap(Type frm)
			{
				m_frm = frm;
			}


		}

		public FormMain(DbSet<Option> options)
		{
			InitializeComponent();

            this.Move += FormMain_Move;
            this.SizeChanged += FormMain_SizeChanged;

            ListViewItem lvi;
			Int32 nCnt = 0;

            foreach (Option option in options)
            {
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(option.Image));
                imageList.Images.Add(x);
                lvi = listViewMain.Items.Add(option.Name, nCnt);
                lvi.Tag = new FormWrap(FormGetter.GetForm((int)option.Number));
                nCnt++;
            }

            Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
		}

		private void listViewMain_DoubleClick(object sender, EventArgs e)
		{
            try
            {
                if (listViewMain.SelectedItems.Count == 0)
                {
                    return;
                }

                //ErrorProvider.HideAll();

                Type selectedItemType = ((FormWrap) listViewMain.SelectedItems[0].Tag).m_frm;

                List<Form> appOpenForms = new List<Form>(this.OwnedForms);
                if (appOpenForms.Exists(x => x.GetType() == selectedItemType))
                {
                    Form alreadyForm = appOpenForms.Find(x => x.GetType() == selectedItemType);
                    if (alreadyForm.WindowState == FormWindowState.Minimized)
                        alreadyForm.WindowState = FormWindowState.Normal;
                    alreadyForm.Focus();
                    return;
                }

                foreach (Form form in appOpenForms)
                        form.Close();

                Form newForm = (Form)Activator.CreateInstance(selectedItemType);
                newForm.Show(this);
                newForm.Size = new Size(Screen.FromControl(this).WorkingArea.Width - this.Width + extraWidth, this.Height);
                newForm.SetDesktopLocation(this.Location.X + this.Size.Width - extraWidth, this.Location.Y);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message + " " + E.ToString());

					 this.Visible = true;
            }
		}


		private void FormMain_Load(object sender, EventArgs e)
		{
			labelLogo.Text += DateTime.Now.ToShortDateString();

		    this.Location = Screen.FromControl(this).WorkingArea.Location;
            this.Location = new Point(
                this.Location.X - SystemInformation.Border3DSize.Width - SystemInformation.BorderSize.Width,
                this.Location.Y);
            this.Size = new Size(this.Width, Screen.FromControl(this).WorkingArea.Height);
            this.MaximumSize = this.Size;
        }

        private void listViewMain_MouseHover(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        private void listViewMain_MouseLeave(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        // P/Invoke declarations
        private const int WM_MOUSEWHEEL = 0x20a;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                //// find the control at screen position m.LParam
                Point pos = new Point(m.LParam.ToInt32());
                IntPtr hWnd = WindowFromPoint(pos);
                if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                {
                    SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            } 
            return false;
        }

        private void listViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // previous FormMain location
        private Point m_PreviousLocation = new Point(int.MinValue, int.MinValue);

        private void FormMain_Move(object sender, EventArgs e)
        {

            if (this.OwnedForms.Length <= 0)
                return;

            Form formsToAdjust =  this.OwnedForms[0];

            // If the main form has been moved...
            if (m_PreviousLocation.X != int.MinValue) //... we move the child form as well
                formsToAdjust.Location = new Point(
                      formsToAdjust.Location.X + Location.X - m_PreviousLocation.X,
                      formsToAdjust.Location.Y + Location.Y - m_PreviousLocation.Y
                    );

            m_PreviousLocation = Location;
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            Form[] formsToAdjust = this.OwnedForms;

            foreach (Form form in formsToAdjust)
            {
                form.Size = new Size(form.Width, this.Height);
                form.SetDesktopLocation(this.Location.X + this.Size.Width - extraWidth, this.Location.Y);
            }
        }
    }
}
