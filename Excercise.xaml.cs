using Calculator.Models;
using Calculator.Services;

namespace Calculator;

public partial class Excercise : ContentPage
{
    String option = "11";
    readonly IQuestionRepository _questionRepository = new QuestionService();

    List<Question> questions = new List<Question>();
    int i = 0;
    public Excercise()
	{
		InitializeComponent();
		loadData();
    }
	public async void onChangeQuestion(object sender, EventArgs e)
	{
        i += 1;
        if (i == questions.Count)
        {
            i = 0;
        }
        ques.Text = questions[i].question;
        a.Content = questions[i].a;
        b.Content = questions[i].b;
        c.Content = questions[i].c;
    }
	public async void onSubmit(object sender, EventArgs e)
	{
        if (Double.Parse(option) == questions[i].solution)
        {
            await DisplayAlert("Correct", "you selected the Correct Option", "OK");
            onChangeQuestion(sender, e);
        }
        else
        {
            bool answer = await DisplayAlert("Wrong answer", "Would you like to try it again?", "Yes", "No");
            if (answer == false)
            {
                onChangeQuestion(sender, e);
            }
        }
    }
	public void onCheckResult(object sender, EventArgs e)
	{
        RadioButton button = sender as RadioButton;
        option = button.Content.ToString();
    }
	public async void loadData()
	{
        questions = await _questionRepository.getAllQuestions();
    }
}