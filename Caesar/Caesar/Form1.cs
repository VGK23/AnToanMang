using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private char shiftChar(char c, int key)
        {
            if (Char.IsLetter(c)) //kiểm tra có phải là chữ cái hay không, nếu không sẽ bỏ qua 
            {
                char Base = Char.IsUpper(c) ? 'A' : 'a';  // Xác định ký tự gốc là chữ in hoa hay chữ thường
                return (char)(((c - Base + key) % 26) + Base); // Thực hiện phép toán dịch chuyển trong bảng chữ cái
            }
            return c;
        }

        // Hàm xử lý chuỗi ký tự của plaintetx hoặc ciphertext
        // Tham số `encrypt` quyết định xem có mã hóa hay không (true: mã hóa, false: giải mã)

        // Const string& message: const sẽ đảm bảo messgae không thay đổi khi hàm được thực hiện
        // đồng thời & sẽ thham chiếu tới đối tượng 
        // mà không cần sao chép để tạo bản sao làm tốn dung lượng khi text nhập vào quá lớn
        private string ProcessMessage(string message, int key, bool encrypt)
        {
            string result = "";
            int effectiveKey = encrypt ? key : 26 - key; // Nếu là giải mã, key = 26 - key (theo quy tắc mã hóa Caesar)
                                                         // Dịch từng ký tự của chuỗi
            foreach (char c in  message)
            {
                result += shiftChar(c, effectiveKey);
            }
            return result;
        }

        private void btEncrypt_Click(object sender, EventArgs e)
        {
            int Key = int.Parse(tbKey.Text);
            string Plaintext = tbPlaintext.Text.ToString();
            tbCiphertext.Text = ProcessMessage(Plaintext, Key, true);
        }

        private void btDecrypt_Click(object sender, EventArgs e)
        {
            int Key = int.Parse(tbKey.Text);
            string Ciphertext = tbCiphertext.Text.ToString();
            tbPlaintext.Text = ProcessMessage(Ciphertext, Key, false);
        }

        private void btDecryptNoKey_Click(object sender, EventArgs e)
        {
            string Ciphertext = tbCiphertext.Text.ToString();
            for (int i = 1; i <= 25; ++i)
            {
                tbPlaintext.Text += "Shift " + i + ": Decrypted message: " + ProcessMessage(Ciphertext, i, false) + "\r\n";
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            tbPlaintext.Clear();
            tbCiphertext.Clear();
        }
    }
}
