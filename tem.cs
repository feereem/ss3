namespace WindowsFormsApp1
{
    public partial class Templete : Form
{
    public static F_Products f_pd;
    public static F_AddEdit f_add;
    public static F_Order f_order;
    public static F_EditOrder f_eorder;
    public static readonly HttpClient http = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:7282/api/")
    };
    public Templete()
    {
        InitializeComponent();
        if (http.DefaultRequestHeaders.Authorization == null)
        {
            var a = Convert.ToBase64String(Encoding.UTF8.GetBytes("staff:BCLyon2024"));
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", a);
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        tem(panel1);
    }
    public void tem(Control ctrl)
    {
        foreach (Control a in ctrl.Controls)
        {
            if(a is Button bt)
            {
                bt.BackColor = Color.PaleTurquoise;
            }
            if(a is ComboBox cb)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            if(a is DateTimePicker dtp)
            {
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = (" / / ");
            }
            if(a is DataGridView dgv)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ForeColor = Color.Black;
                dgv.RowHeadersVisible = false;
                dgv.MultiSelect = false;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            }
            if (a.HasChildren)
            {
                tem(a);
            }
        }
    }
    // Func Connect API
    public async Task<T> ConnectAPI<T>(string endpoint, HttpMethod method, object data = null)
    {
        try
        {
            HttpRequestMessage re = new HttpRequestMessage(method, endpoint);
            if (data != null)
            {
                var json = JsonSerializer.Serialize(data);
                re.Content = new StringContent(json,Encoding.UTF8,"application/json");
            }
            var res = await http.SendAsync(re);
            if (res.IsSuccessStatusCode)
            {
                var response = await res.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(response)) return default;
                if (typeof(T) == typeof(string)) return (T)(object)response;

                return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        catch (Exception ex)
        {
            MsgWaring("Error" + ex.Message);
        }
        return default;
    }
    public void MsgWaring(string str)
    {
        MessageBox.Show(str, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    public void MsgSuccess(string str)
    {
        MessageBox.Show(str, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
}
