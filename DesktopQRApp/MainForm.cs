using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

namespace DesktopQRApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGenerateQRCode_Click(object sender, EventArgs e)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250
            };

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            if (String.IsNullOrWhiteSpace(txtQRCode.Text) || String.IsNullOrEmpty(txtQRCode.Text))
            {
                picQRCode.Image = null;
                MessageBox.Show("Text not found", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else
            {
                var qr = new ZXing.BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;

                var result = new Bitmap(qr.Write(txtQRCode.Text.Trim()));
                picQRCode.Image = result;
                txtQRCode.Clear();
            }
        }

        private void btnDecodeQRCode_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = new Bitmap(picQRCode.Image);
                BarcodeReader reader = new BarcodeReader 
                { 
                    AutoRotate = true, 
                    TryInverted = true 
                };

                Result result = reader.Decode(bitmap);
                string decoded = result.ToString().Trim();
                txtQRCode.Text = decoded;
            }
            catch(Exception)
            {
                MessageBox.Show("Image not found", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250
            };

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var qr = new ZXing.BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                picQRCode.ImageLocation = open.FileName;
            }
        }

        private void btnUseCamera_Click(object sender, EventArgs e)
        {
            CameraQRForm cameraForm = new CameraQRForm();
            cameraForm.ShowDialog();
        }
    }
}
