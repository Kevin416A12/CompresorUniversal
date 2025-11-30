namespace CompresorUniversal.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnSelectFiles;
        private Button btnCompress;
        private Button btnDecompress;

        private ComboBox comboAlgorithms;
        private ListBox listFiles;

        private Label lblStats;
        private Label lblAlgorithm;

        private OpenFileDialog openFileDialog1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSelectFiles = new Button();
            this.btnCompress = new Button();
            this.btnDecompress = new Button();

            this.comboAlgorithms = new ComboBox();
            this.listFiles = new ListBox();

            this.lblStats = new Label();
            this.lblAlgorithm = new Label();

            this.openFileDialog1 = new OpenFileDialog();

            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Location = new Point(20, 20);
            this.btnSelectFiles.Size = new Size(200, 35);
            this.btnSelectFiles.Text = "Seleccionar archivos";
            this.btnSelectFiles.Click += new EventHandler(this.btnSelectFiles_Click);

            // 
            // listFiles
            // 
            this.listFiles.Location = new Point(20, 70);
            this.listFiles.Size = new Size(400, 200);

            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.Location = new Point(20, 290);
            this.lblAlgorithm.Size = new Size(120, 30);
            this.lblAlgorithm.Text = "Algoritmo:";

            // 
            // comboAlgorithms
            // 
            this.comboAlgorithms.Location = new Point(140, 285);
            this.comboAlgorithms.Size = new Size(180, 30);
            this.comboAlgorithms.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboAlgorithms.Items.AddRange(new object[]
            {
                "Huffman",
                "LZ77",
                "LZ78"
            });
            this.comboAlgorithms.SelectedIndex = 0;

            // 
            // btnCompress
            // 
            this.btnCompress.Location = new Point(20, 330);
            this.btnCompress.Size = new Size(150, 40);
            this.btnCompress.Text = "Comprimir";
            this.btnCompress.Click += new EventHandler(this.btnCompress_Click);

            // 
            // btnDecompress
            // 
            this.btnDecompress.Location = new Point(200, 330);
            this.btnDecompress.Size = new Size(150, 40);
            this.btnDecompress.Text = "Descomprimir";
            this.btnDecompress.Click += new EventHandler(this.btnDecompress_Click);

            // 
            // lblStats
            // 
            this.lblStats.Location = new Point(20, 390);
            this.lblStats.Size = new Size(500, 150);
            this.lblStats.Text = "Estadísticas:";

            // 
            // MainForm
            // 
            this.ClientSize = new Size(550, 580);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.lblAlgorithm);
            this.Controls.Add(this.comboAlgorithms);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.lblStats);

            this.Text = "Universal Compressor";
        }
    }
}
