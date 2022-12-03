namespace Calculator;

public partial class History : ContentPage
{
	public History()
	{
		InitializeComponent();
		BindingContext = App.vm;
	}

	private async void Clear_Clicked(object sender, EventArgs e)
	{
		await App.vm.clearDatabase();
		await Navigation.PopToRootAsync();
	}
}