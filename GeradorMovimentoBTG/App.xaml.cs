using GeradorMovimentoBTG.Views;

namespace GeradorMovimentoBTG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}