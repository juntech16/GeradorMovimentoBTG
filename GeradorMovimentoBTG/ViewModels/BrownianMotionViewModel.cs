using GeradorMovimentoBTG.Models;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GeradorMovimentoBTG.ViewModels
{
    public class BrownianMotionViewModel : BaseViewModel
    {
        private double _initialPrice = 100;
        private double _volatility = 20;
        private double _meanReturn = 1;
        private int _timePeriod = 252;
        private int _numSimulations = 1;
        private bool _isSimulating;
        private ObservableCollection<Color> _lineColors;
        
        public double[][] SimulationResults { get; private set; }
        
        public double InitialPrice
        {
            get => _initialPrice;
            set => SetProperty(ref _initialPrice, value);
        }

        public double Volatility
        {
            get => _volatility;
            set => SetProperty(ref _volatility, value);
        }

        public double MeanReturn
        {
            get => _meanReturn;
            set => SetProperty(ref _meanReturn, value);
        }

        public int TimePeriod
        {
            get => _timePeriod;
            set => SetProperty(ref _timePeriod, value);
        }

        public int NumSimulations
        {
            get => _numSimulations;
            set 
            { 
                if (value < 1) value = 1;
                if (value > 10) value = 10;
                SetProperty(ref _numSimulations, value);
                InitializeColors();
            }
        }

        public bool IsSimulating
        {
            get => _isSimulating;
            set => SetProperty(ref _isSimulating, value);
        }

        public ObservableCollection<Color> LineColors
        {
            get => _lineColors;
            set => SetProperty(ref _lineColors, value);
        }

        public ICommand GenerateSimulationCommand { get; }
        public ICommand IncrementSimulationsCommand { get; }
        public ICommand DecrementSimulationsCommand { get; }

        public BrownianMotionViewModel()
        {
            GenerateSimulationCommand = new Command(ExecuteGenerateSimulation);
            IncrementSimulationsCommand = new Command(ExecuteIncrementSimulations);
            DecrementSimulationsCommand = new Command(ExecuteDecrementSimulations);
            
            InitializeColors();
        }
        
        private void InitializeColors()
        {
            var colors = new List<Color>
            {
                Colors.Blue,
                Colors.Red,
                Colors.Green,
                Colors.Purple,
                Colors.Orange,
                Colors.Teal,
                Colors.Pink,
                Colors.Brown,
                Colors.Cyan,
                Colors.Magenta
            };

            LineColors = new ObservableCollection<Color>(colors.Take(NumSimulations));
        }

        private void ExecuteGenerateSimulation()
        {
            if (IsSimulating)
                return;

            IsSimulating = true;


            double sigma = Volatility / 100.0;
            double mean = MeanReturn / 100.0;
            

            sigma = sigma / Math.Sqrt(252);
            mean = mean / 252;

            try
            {
                SimulationResults = BrownianMotionModel.GenerateMultipleBrownianMotions(
                    sigma,
                    mean,
                    InitialPrice,
                    TimePeriod,
                    NumSimulations);
                
                OnPropertyChanged(nameof(SimulationResults));
            }
            finally
            {
                IsSimulating = false;
            }
        }

        private void ExecuteIncrementSimulations()
        {
            if (NumSimulations < 10)
            {
                NumSimulations++;
            }
        }

        private void ExecuteDecrementSimulations()
        {
            if (NumSimulations > 1)
            {
                NumSimulations--;
            }
        }
    }
}
