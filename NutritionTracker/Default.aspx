<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NutritionTracker.Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nutrition Tracker</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class ="page-header">        
                <h1>Nutrition Tracker</h1>
                <p class="lead">Use this tool to track your daily nutritional intake!</p>
                <p class="lead">&nbsp;</p>
            </div>
            <div class="row">
                <div class="col-md-6">                    
                    <h4>New Food Item:</h4>                    
                    <div class="form-group">
                        <asp:TextBox ID="newNameTextBox" runat="server" class="form-control" placeholder ="Enter food name"></asp:TextBox>
                    </div>                    
                    <div class="form-group">
                        <asp:TextBox ID="newCaloriesTextBox" runat="server" class="form-control" placeholder ="Enter number of calories"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="newProteinsTextBox" runat="server" class="form-control" placeholder ="Enter grams of protein"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="newCarbsTextBox" runat="server" class="form-control" placeholder ="Enter grams of carbohydrate"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="newFatsTextBox" runat="server" class="form-control" placeholder ="Enter grams of fat"></asp:TextBox>
                    </div>
                    <br />
                    <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Add Item" CssClass="btn btn-lg btn-primary" />
                    <br />
                    <br />
                    <asp:Label ID="errorLabel" runat="server" ForeColor="#FF6600"></asp:Label>
                    <br />
                    <br />
                </div>                
                <div class="col-md-3">
                    <div>    
                        <h4>Today&#39;s Totals:</h4>                        
                        Calories:<asp:Label ID="totalCaloriesLabel" runat="server"></asp:Label>                        
                        <br />    
                        Proteins:<asp:Label ID="totalProteinsLabel" runat="server"></asp:Label>
                        <br />
                        Carbs: <asp:Label ID="totalCarbsLabel" runat="server"></asp:Label>
                        <br />
                        Fats:<asp:Label ID="totalFatsLabel" runat="server"></asp:Label>
                        <br />
                        <br />
                        Your caloric breakdown:
                        <br />
                        <asp:Label ID="ratioLabel" runat="server"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </div>        
                </div>
            </div>    
         </div> 
      <div>
            <asp:GridView ID="foodGridView" runat="server">
            </asp:GridView>
            <br />
            <br />
            <asp:Chart ID="trendChart" runat="server">
                <series>
                   <asp:Series ChartType="Line" Name="Series1">
                   </asp:Series>
                </series>
                <chartareas>
                   <asp:ChartArea Name="ChartArea1">
                   </asp:ChartArea>
                </chartareas>
            </asp:Chart>
            <br />
            <br />
        </div>        
    </form>        
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script> 
</body>
</html>
