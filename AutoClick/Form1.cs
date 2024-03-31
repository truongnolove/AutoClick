using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClick
{
    public partial class Form1 : Form
    {
        // Import các hàm từ thư viện User32.dll
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);
        public static int KEY_DOWN(int vKey)
        {
            return (GetKeyState(vKey) & 0x8000) != 0 ? 1 : 0;
        }

        // Các hằng số cho các loại sự kiện chuột
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;

        const int VK_RETURN = 0x0D; // Mã phím cho phím Enter

        int KeyStart = 112;
        int KeyStop = 113;
        // Import hàm từ thư viện kernel32.dll
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        
        bool Start = false;
        int Count = 0;
        public Form1()
        {
           
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(Convert.ToInt32(DateTime.Now.ToString("yyyy")), Convert.ToInt32(DateTime.Now.ToString("MM")), Convert.ToInt32(DateTime.Now.ToString("dd")), Convert.ToInt32(DateTime.Now.ToString("HH")), Convert.ToInt32(DateTime.Now.ToString("mm")), 01);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Start = false;
        }

        public void AutoClick()
        {
            while (true) {
                if (!Start)
                    break;
               
            }
            
        }
        // Hàm mô phỏng việc ấn phím Enter
        void PressEnter()
        {
            
            
            keybd_event(VK_RETURN, 0, 0, UIntPtr.Zero);
            
            keybd_event(VK_RETURN, 0, 2, UIntPtr.Zero);
        }
        void LeftClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0); // Click chuột phải
            Thread.Sleep(100); // Giữ chuột phải trong một khoảng thời gian ngắn
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0); // Nhả chuột phải
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (KEY_DOWN(KeyStart) != 0 ) {
                if (!Start)
                {
                    Start = true;
                    Count = 0;
                }
            }
            if (KEY_DOWN(KeyStop) != 0) {
                if (Start)
                    Start = false;
               
            }
            if (Start) {
               Count++;
               LeftClick(0, 0);
               if (checkBox1.Checked)
                   PressEnter();
               label1.Text = $"Số Lượng Đã Click : {Count}";
               Thread.Sleep(dateTimePicker1.Value.Second * 2);
           }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            KeyStart = 112 + comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            KeyStop = 112 + comboBox2.SelectedIndex;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng ứng dụng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                // Hủy sự kiện đóng nếu người dùng chọn "No"
                e.Cancel = true;
            }
            else
                Environment.Exit(1);
        }
    }
}
