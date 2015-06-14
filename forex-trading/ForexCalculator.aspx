<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForexCalculator.aspx.cs" Inherits="forex_trading.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function displayForm() {
            removeBoxes();
            var numberOfTextBoxes = document.getElementById('<%=txtSplits.ClientID%>').value;

            for (i = 0; i < numberOfTextBoxes; i++) {
                var newInput = document.createElement("input");
                newInput.setAttribute("type", "Text");
                newInput.setAttribute("name", "txtSplitPercentage" + i);
                document.getElementById("divToAddSplitPercent").appendChild(newInput);
            }           
        }

        function removeBoxes() {
            var d = document.getElementById("divToAddSplitPercent");
            while (d.firstChild) d.removeChild(d.firstChild);
        }

        function displayBoxes() {
            var numberOfTextBoxes = document.getElementById('<%=txtSplits.ClientID%>').value;

            for (i = 0; i < numberOfTextBoxes; i++) {
                var newInput = document.createElement("input");
                newInput.setAttribute("type", "Text");
                newInput.setAttribute("name", "txtSplitPercentage" + i);
                document.getElementById("divToAddSplitPercent").appendChild(newInput);
            }
        }
    </script>
</head>

<body onload="">
    <form id="form1" runat="server">
    <div>Test page</div>
        <table>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblLot" runat="server" Text="Lot size:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLotSize" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPrice" runat="server" Text="Price to buy/ sell:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLoss" runat="server" Text="Loss acceptable:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoss" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProfit" runat="server" Text="Profit required:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProfit" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSplits" runat="server" Text="Number of Splits:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSplits" runat="server" Text="2" onBlur="displayForm()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTradeType" runat="server" Text="Trade Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTradeType" runat="server">
                            <asp:ListItem Text="Buy" Value="Buy" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Sell" Value="Sell"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSplitPercent" runat="server" Text="Split percentage:"></asp:Label>
                    </td>
                    <td id="divToAddSplitPercent">
                        <asp:PlaceHolder ID="plcSplitPercentage" runat="server" />  
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <asp:Table ID="tblResult" runat="server" GridLines="Both" >
            <asp:TableHeaderRow ID="Table1HeaderRow"
                BackColor="LightBlue"
                runat="server">
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Price Value" />
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Pip Value SP" />
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Stop Loss" />
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Stop Loss Value" />
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Pip Value TP" />
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Take Profit" />
                <asp:TableHeaderCell
                    Scope="Column"
                    Text="Take Profit Value" />
            </asp:TableHeaderRow>  
        </asp:Table>
    </form>
</body>
</html>
