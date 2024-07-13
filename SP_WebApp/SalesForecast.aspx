<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalesForecastingApp.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Forecasting Application</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .scrollable-gridview {
            height: 400px; 
            overflow-y: auto;
            overflow-x: auto;
            border: 1px solid #dddddd; 
            border-radius: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Sales Forecasting Application</h2>
            <div class="mb-3">
                <label for="txtYear" class="form-label">Year</label>
                <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtIncrement" class="form-label">Percentage Increment</label>
                <asp:TextBox ID="txtIncrement" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">

                <asp:Button ID="btnQuerySales" runat="server" Text="Query Sales" CssClass="btn btn-primary" OnClick="btnQuerySales_Click" />
                <asp:Button ID="btnApplyIncrement" runat="server" Text="Apply Increment" CssClass="btn btn-success" OnClick="btnApplyIncrement_Click" />
                <asp:Button ID="btnExportCsv" runat="server" Text="Export to CSV" CssClass="btn btn-secondary" OnClick="btnExportCsv_Click" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblFillEntries" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="scrollable-gridview">
                <asp:GridView ID="gvSalesData" runat="server" CssClass="table table-bordered" AutoGenerateColumns="true" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblIncreasedSales" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
