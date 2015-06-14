using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace forex_trading
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        double splitPercent, lossAcceptable, profitRequired, lotSize, priceToBuyOrSell = 0;
        bool isFirst = false;
        const string VIEW_STATE_NAME = "PercentageValues";
        const string CONTROL_NAME = "txtSplitPercentage";

        protected void Page_Load(object sender, EventArgs e)
        {
            //txtSplits.Attributes.Add("OnKeyPress", "displayForm()");

            if (Page.IsPostBack)
            {
                //displayInputControls();
                //ViewState("PercentageSplit") = splitPercent;
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            saveInputControls(Int32.Parse(txtSplits.Text));
            generateValues();
        }

        private void saveInputControls(int noOfSpilts)
        {
            if (ViewState[VIEW_STATE_NAME] == null)
            {
                for (int i = 0; i < noOfSpilts; i++)
                {
                    TextBox txtSplitPercent = new TextBox();
                    double percent = 0;
                    Double.TryParse(Request.Form[CONTROL_NAME + i.ToString()], out percent);
                    txtSplitPercent.Text = percent.ToString();
                    txtSplitPercent.ID = CONTROL_NAME + i;
                    plcSplitPercentage.Controls.Add(txtSplitPercent);
                    isFirst = true;
                    //displayInputControls();
                }
            }           
        }

        private void displayInputControls()
        {
            if (ViewState[VIEW_STATE_NAME] != null && !isFirst)
            {
                List<double> splitValues = (List<double>)ViewState[VIEW_STATE_NAME];
                int i = 0;
                foreach (double eachValue in splitValues)
                {
                    TextBox txtSplitPercent = new TextBox();
                    txtSplitPercent.ID = CONTROL_NAME + i.ToString();
                    txtSplitPercent.Text = eachValue.ToString();
                    plcSplitPercentage.Controls.Add(txtSplitPercent);
                    i++;
                }
            }
        }


        private void generateValues()
        {
            Double.TryParse(txtProfit.Text, out profitRequired);
            Double.TryParse(txtLoss.Text, out lossAcceptable);
            Double.TryParse(txtLotSize.Text, out lotSize);
            Double.TryParse(txtPrice.Text, out priceToBuyOrSell);

            getSplitTextBoxData(Convert.ToInt32(txtSplits.Text));

            displayInputControls();
        }

        private void getSplitTextBoxData(int noOfSpilts)
        {
            List<Double> listOfPercentages = new List<Double>();
            for (int i = 0; i < noOfSpilts; i++)
            {
                string nameOfSplitBox = CONTROL_NAME + i.ToString();

                if (ViewState[VIEW_STATE_NAME] != null)
                {
                    TextBox txtPercentage = (TextBox)plcSplitPercentage.FindControl(nameOfSplitBox);
                }

                Double.TryParse(Request.Form[nameOfSplitBox], out splitPercent);
                //Response.Write("Test " + splitPercent.ToString());
                listOfPercentages.Add(splitPercent);
                //adding rows to table based of calcs
                addTableRows();
            }
            ViewState.Add(VIEW_STATE_NAME, listOfPercentages);
        }

        private void addTableRows()
        {
            TableRow tableResultsRow = new TableRow();
            tblResult.Rows.Add(tableResultsRow);

            TableCell tableResultsCellPrice = new TableCell();
            tableResultsCellPrice.Text = txtPrice.Text;
            tableResultsRow.Cells.Add(tableResultsCellPrice);

            TableCell tableResultsCellStopLossPip = new TableCell();
            tableResultsCellStopLossPip.Text = calculatePipStopLoss();
            tableResultsRow.Cells.Add(tableResultsCellStopLossPip);

            TableCell tableResultsCellStopLoss = new TableCell();
            tableResultsCellStopLoss.Text = calculateStopLoss();
            tableResultsRow.Cells.Add(tableResultsCellStopLoss);

            TableCell tableResultsCellStopLossValue = new TableCell();
            tableResultsCellStopLossValue.Text = calculateStopLossValue();
            tableResultsRow.Cells.Add(tableResultsCellStopLossValue);

            TableCell tableResultsCellTakeProfitPip = new TableCell();
            tableResultsCellTakeProfitPip.Text = calculatePipTakeProfit();
            tableResultsRow.Cells.Add(tableResultsCellTakeProfitPip);

            TableCell tableResultsCellTakeProfit = new TableCell();
            tableResultsCellTakeProfit.Text = calculateTakeProfit();
            tableResultsRow.Cells.Add(tableResultsCellTakeProfit);

            TableCell tableResultsCellTakeProfitValue = new TableCell();
            tableResultsCellTakeProfitValue.Text = calculateTakeProfitValue();
            tableResultsRow.Cells.Add(tableResultsCellTakeProfitValue);
        }

        private string calculatePipStopLoss()
        {
            double stopLossPip = (splitPercent / 100) * lossAcceptable;
            stopLossPip = stopLossPip / lotSize;
            return stopLossPip.ToString();
        }

        private string calculateStopLossValue()
        {
            double stopLossValue = (splitPercent / 100) * lossAcceptable;
            return stopLossValue.ToString();
        }

        private string calculateTakeProfitValue()
        {
            double takeProfitValue = (splitPercent / 100) * profitRequired;
            return takeProfitValue.ToString();
        }

        private string calculatePipTakeProfit()
        {
            double takeProfitPip = (splitPercent / 100) * profitRequired;
            takeProfitPip = takeProfitPip / lotSize;
            return takeProfitPip.ToString();
        }

        private string calculateStopLoss()
        {
            double stopLoss = (splitPercent / 100) * lossAcceptable;
            stopLoss = stopLoss / lotSize;
            if (ddlTradeType.SelectedValue == "Buy")
            {
                stopLoss = priceToBuyOrSell - (stopLoss * 0.00001);
            }
            else
            {
                stopLoss = priceToBuyOrSell + (stopLoss * 0.00001);
            }
            stopLoss = Math.Round(stopLoss, 5);
            //we need to know pip value for the currency pair
            // EUR/ USD = 0.0001
            // GBP/USD = 0.0001
            // USD/ JPY = 0.01
            // USD/ CHF = 0.0001
            // NZD/ USD = 0.0001
            // USD/ CAD = 0.0001
            return String.Format("{0:0.00000}", stopLoss);
        }

        private string calculateTakeProfit()
        {
            double takeProfit = (splitPercent / 100) * profitRequired;
            takeProfit = takeProfit / lotSize;

            if (ddlTradeType.SelectedValue == "Buy")
            {
                takeProfit = priceToBuyOrSell + (takeProfit * 0.00001);
            }
            else
            {
                takeProfit = priceToBuyOrSell - (takeProfit * 0.00001);
            }
            takeProfit = Math.Round(takeProfit, 5);

            return String.Format("{0:0.00000}", takeProfit);
        }
    }
}