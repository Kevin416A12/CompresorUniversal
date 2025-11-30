using CompresorUniversal.Core.Interfaces;
using CompresorUniversal.Core.Modelos;
using CompresorUniversal.Core.MyZipFormat;
using CompresorUniversal.Core.Compresor;

namespace CompresorUniversal.UI.Forms
{
    public partial class MainForm : Form
    {
        private List<string> selectedFiles = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFiles.Clear();
                listFiles.Items.Clear();

                foreach (var file in openFileDialog1.FileNames)
                {
                    selectedFiles.Add(file);
                    listFiles.Items.Add(file);
                }
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            if (selectedFiles.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un archivo.");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Archivo .myzip|*.myzip";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            AlgorithmType type = (AlgorithmType)comboAlgorithms.SelectedIndex;
            var compressor = CompressorFactory.Create(type);

            var result = MyZipWriter.CompressFiles(selectedFiles, compressor);

            File.WriteAllBytes(saveDialog.FileName, result.FileBytes);

            ShowStats(result.Stats);
            MessageBox.Show("Compresión completada.");
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivo .myzip|*.myzip";

            if (open.ShowDialog() != DialogResult.OK)
                return;

            var result = MyZipReader.Decompress(open.FileName);

            ShowStats(result.Stats);

            MessageBox.Show($"Archivos descomprimidos en:\n{result.OutputFolder}");
        }

        private void ShowStats(Statistics stats)
        {
            lblStats.Text =
                $"Estadísticas:\n" +
                $"- Tiempo: {stats.TimeMilliseconds} ms\n" +
                $"- Memoria usada: {stats.MemoryBytes} bytes\n" +
                $"- Tasa de compresión: {stats.CompressionRatio:F2}%";
        }
    }
}
