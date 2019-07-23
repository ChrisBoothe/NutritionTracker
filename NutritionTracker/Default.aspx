<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NutritionTracker.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Nutrition Tracker</h2>
        <h4>New Food Item:</h4>
        Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="newNameTextBox" runat="server"></asp:TextBox>
        <br />
        Calories:&nbsp;
        <asp:TextBox ID="newCaloriesTextBox" runat="server" style="margin-left: 7px" Width="57px"></asp:TextBox>
        <br />
        Proteins:&nbsp;&nbsp; <asp:TextBox ID="newProteinsTextBox" runat="server" style="margin-left: 4px" Width="56px"></asp:TextBox>
        <br />
        Carbs:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="newCarbsTextBox" runat="server" style="margin-left: 0px" Width="57px"></asp:TextBox>
        <br />
        Fats:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="newFatsTextBox" runat="server" style="margin-left: 3px" Width="57px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Add Item" />
        <br />
        <h4>Today&#39;s Totals:</h4>
        Calories:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="totalCaloriesTextBox" runat="server" Width="57px"></asp:TextBox>
        <br />
        Proteins:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="totalProteinsTextBox" runat="server" style="margin-left: 0px" Width="56px"></asp:TextBox>
        <br />
        Carbs:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="totalCarbsTextBox" runat="server" style="margin-left: 1px" Width="57px"></asp:TextBox>
        <br />
        Fats:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="totalFatsTextBox" runat="server" Width="57px"></asp:TextBox>
        <br />
        Ratio:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="ratioLabel" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
