namespace WindowsFormsApp1
{
    public partial class Template : Form
    {
        // w
        public static readonly HttpClient http = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7169/api/")
        };
        public Template()
        {
            InitializeComponent();
            if(http.DefaultRequestHeaders.Authorization == null)
            {
                var a = Convert.ToBase64String(Encoding.UTF8.GetBytes("staff:BCLyon2024"));
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", a);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void tem(Control ctrl)
        {
            foreach (Control a in ctrl.Controls)
            {
                if (a is Button bt)
                {
                    bt.BackColor = Color.PaleTurquoise;
                }
                if(a is ComboBox cb)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                if(a is DateTimePicker dt)
                {
                    dt.Format = DateTimePickerFormat.Custom;
                    dt.CustomFormat = (" / / ");
                }
                if(a is DataGridView dgv)
                {
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgv.ForeColor = Color.Black;
                    dgv.RowHeadersVisible = false;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.MultiSelect = false;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                }
                if (a.HasChildren)
                {
                    tem(a);
                }
            }
        }
    }
}
