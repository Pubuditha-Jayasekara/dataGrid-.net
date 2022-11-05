using System.Data;

namespace Smart_POS_System
{
    public partial class Form1 : Form
    {
        public static int qtySelected = 0;
        public static double totalPrice = 0.0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Inventory grid
            DataTable dt = new DataTable() { TableName = "inventory" };

            dt.Columns.Add(new DataColumn("Item_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit_Price(Rs.)", typeof(double)));
            dt.Columns.Add(new DataColumn("Available Qty", typeof(string)));

            var rows = new string[][]
            {
                new string[] {"Araliya White Rice", "2400.00", "10kg" },
                new string[] {"Araliya Red Rice", "2300.89", "10kg" },
                new string[] {"Nestomalt", "240.50", "1Kg" },
            };

            //dt.Rows.Add(rows[0]);
            //dt.Rows.Add(rows[1]);


            for (int i=0; i<rows.Length; i++)
            {
                dt.Rows.Add(rows[i]);
            }

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //Invoice grid
            DataTable dt2 = new DataTable() { TableName = "invoice" };

            dt2.Columns.Add(new DataColumn("Item_Name", typeof(string)));
            dt2.Columns.Add(new DataColumn("Unit_Price(Rs.)", typeof(double)));
            dt2.Columns.Add(new DataColumn("Qty", typeof(int)));
            dt2.Columns.Add(new DataColumn("Total", typeof(double)));

            var rowsdt2 = new string[][]
           {
           };

            foreach (var row in rowsdt2)
            {
                dt2.Rows.Add(rowsdt2);
            }

            dataGridView2.DataSource = dt2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Item Name like '{0}%' OR Product like '{0}%' OR Name like '{0}%' OR Product like '{0}%'", textBox1.Text);
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Item_Name like '{0}%'", textBox1.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            numericUpDown1.Value = 0;
            qtySelected = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            DataTable dt = dataGridView2.DataSource as DataTable;

            qtySelected = Convert.ToInt32(numericUpDown1.Value);
            double total = qtySelected * Convert.ToDouble(row.Cells[1].Value.ToString());
            dt.Rows.Add(
            new string[] { row.Cells[0].Value.ToString(),
                row.Cells[1].Value.ToString(),
                $"{qtySelected}",
               $"{total}"}
            );
            dataGridView2.DataSource = dt;

            totalPrice = 0.0;
            for (int i = 0; i < dataGridView2.Rows.Count; ++i){
                totalPrice = totalPrice + Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
            }

            label4.Text = $"{totalPrice}";
            label5.Text = $"{dataGridView2.Rows.Count - 1}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView2.DataSource as DataTable;
            dt.Rows.Clear();
            dataGridView2.DataSource = dt;
            textBox1.Text = "";
            numericUpDown1.Value = 0;
            qtySelected = 0;
            label4.Text = "0";
            label5.Text = "0";
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = dataGridView2.DataSource as DataTable;
            dt.Rows.RemoveAt(rowIndex);
            dataGridView2.DataSource = dt;

            totalPrice = 0.0;
            for (int i = 0; i < dataGridView2.Rows.Count; ++i)
            {
                totalPrice = totalPrice + Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
            }

            label4.Text = $"{totalPrice}";
            label5.Text = $"{dataGridView2.Rows.Count - 1}";
        }
    }
}