using System.Data;

namespace Calculator;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        OnClear(this, null);

    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string expressionString = "";
    string decimalFormat = "N0";

    public void moveToAbout(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AboutPage());
    }
    public void moveToHistory(object sender, EventArgs e)
    {
        Navigation.PushAsync(new History());
    }
    public void moveToExcerise(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Excercise());
    }
    public void changeToDark(object sender, EventArgs e)
    {
        Resources["Primary"] = Colors.White;
        Resources["Light"] = Colors.White;
        Resources["Dark"] = Colors.Black;
    }
    public void changeToLight(object sender, EventArgs e)
    {
        Resources["Primary"] = Colors.Black;
        Resources["Light"] = Colors.Black;
        Resources["Dark"] = Colors.White;
    }
    public void changeToGreen(object sender, EventArgs e)
    {
        Resources["Primary"] = Colors.Green;
        Resources["Light"] = Colors.Black;
        Resources["Dark"] = Colors.LightGreen;
    }
    public void changeToPink(object sender, EventArgs e)
    {
        Resources["Primary"] = Colors.Pink;
        Resources["Light"] = Colors.Black;
        Resources["Dark"] = Colors.LightPink;
    }

    void OnSelectNormal(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        String pressed = button.Text;
        if (pressed == "mod")
        {
            expressionString += "%";
        }
        else if (pressed == "×") {
            expressionString += "*";
        }
        else if (pressed == "÷")
        {
            expressionString += "/";
        }
        else if (pressed == "%")
        {
            expressionString += "*0.01";
        }
        else
        {
            expressionString += pressed;
        }
        
        resultText.Text = expressionString;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        if (pressed == "mod")
        {
            mathOperator = "%";
        }
        else
        {
            mathOperator = pressed;
        }
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        expressionString = String.Empty;
        this.resultText.Text = "0";
        currentEntry = string.Empty;
    }

    void OnCalculate(object sender, EventArgs ev)
    {
        DataTable dt = new DataTable();
        var v = dt.Compute(expressionString, "");
        this.resultText.Text = v.ToString();
        currentState = -1;
        App.vm.Add($"{expressionString} = {v}");
        expressionString = v.ToString();
    }

    void OnNegative(object sender, EventArgs e)
    {
        var v = float.Parse(expressionString)*-1;
        this.resultText.Text = v.ToString();
        currentState = -1;
        expressionString = this.resultText.Text;
    }

    void OnSelectSqrt(object sender, EventArgs e)
    {
        var sq = Math.Sqrt(int.Parse(expressionString));
        this.resultText.Text = sq.ToString();
        currentState = -1;
        expressionString = sq.ToString();
    }

}
