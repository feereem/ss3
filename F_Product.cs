public partial class F_Products : Templete
{
    public F_Products()
    {
        InitializeComponent();
        f_pd = this;
    }

    private void F_Products_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    private void button1_Click(object sender, EventArgs e)
    {

        new F_AddEdit("Add", (int)dataGridView1.CurrentRow.Cells[7].Value).Show();
        Hide();
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        if (textBox1.Text != "search on product / category")
        {
            LoadData(textBox1.Text);
        }
    }
    // func Load Data Product
    public async Task LoadData(string str = "")
    {
        if (dataGridView1.Rows.Count > 0)
        {
            dataGridView1.Rows.Clear();
        }
        var q = await ConnectAPI<List<Product>>($"Products?str={str}", HttpMethod.Get, null);
        foreach (var item in q)
        {
            dataGridView1.Rows.Add(item.Active, item.ProductName, item.Category,item.Price, item.Cost, "edit delete", item, item.ProductId);
        }
    }

    private void textBox1_Layout(object sender, LayoutEventArgs e)
    {
        if(textBox1.Text == "")
        {
            textBox1.Text = "search on product / category";
            textBox1.ForeColor = Color.LightGray;
        }
    }

    private void button1_Enter(object sender, EventArgs e)
    {
        if(textBox1.Text != "")
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }
    }
}
