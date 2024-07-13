using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Web.UI;
using InteractiveDataDisplay.WPF;




namespace SalesForecastingApp
{
    public partial class Default : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SuperstoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvSalesData.Visible = false;
            }
        }

        protected void btnQuerySales_Click(object sender, EventArgs e)
        {
            int year;
            if (int.TryParse(txtYear.Text, out year))
            {
                DataTable salesData = GetSalesData(year);
                gvSalesData.DataSource = salesData;
                gvSalesData.DataBind();
                gvSalesData.Visible = true;
                ViewState["dataTable"] = salesData; //adding queried sales to viewstate
            }
        }

        protected void btnApplyIncrement_Click(object sender, EventArgs e)
        {
            int year;
            decimal increment;
            if (int.TryParse(txtYear.Text, out year) && decimal.TryParse(txtIncrement.Text, out increment))
            {
                DataTable salesData = ApplySalesIncrement(year, increment);
                gvSalesData.DataSource = salesData;
                gvSalesData.DataBind();
                gvSalesData.Visible = true;
                ViewState["dataTable"] = salesData; //adding incremented sales to viewstate
                
                lblFillEntries.Text = "";
            }
            else
            {
                lblFillEntries.Text = "Fill Percentage and year first";
            }
        }

        
        private DataTable GetSalesData(int year)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // selecting the state, its sales and returns
                string query = @"SELECT 
                    T_Orders.State, 
                    SUM(T_Products.Sales) AS TotalSales,
                    COUNT(T_OrderReturns.OrderId) AS Returns
                    FROM 
                        T_Orders
                    INNER JOIN 
                        T_Products ON T_Orders.OrderId = T_Products.OrderId
                    LEFT JOIN 
                        T_OrderReturns ON T_Orders.OrderId = T_OrderReturns.OrderId
                    WHERE 
                        YEAR(T_Orders.OrderDate) = @year
                    GROUP BY 
                        T_Orders.State";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        private DataTable ApplySalesIncrement(int year, decimal increment)
        {
            DataTable dataTable = GetSalesData(year);
            dataTable.Columns.Add("IncrementedSales", typeof(decimal));

            // adding an incremented sale row 
            foreach (DataRow row in dataTable.Rows)
            {
                decimal totalSales = Convert.ToDecimal(row["TotalSales"]);
                decimal incrementedSales = totalSales * (1 + (increment / 100));
                row["IncrementedSales"] = incrementedSales;
            }
            
            return dataTable;
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
            DataTable salesData = ViewState["dataTable"] as DataTable;
            if (salesData != null && salesData.Rows.Count > 0)
            {
                string filePath = Server.MapPath("~/App_Data/SalesForecast.csv");
                ExportToCSV(salesData, filePath);
                Response.ContentType = "application/csv";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=SaleForecast.csv");
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                // Handle the case where the GridView has no data
                lblIncreasedSales.Text = "Please query sales data first.";
            }
        }

        public void ExportToCSV(DataTable data, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = data.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in data.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            File.WriteAllText(filePath, sb.ToString());
        }

    }
    
}