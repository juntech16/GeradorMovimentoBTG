using GeradorMovimentoBTG.Models;
using GeradorMovimentoBTG.ViewModels;

namespace GeradorMovimentoBTG.Views;

public partial class BrownianMotionView : ContentPage
{
    private readonly BrownianMotionDrawable _drawable;
    private readonly BrownianMotionViewModel _viewModel;

    public BrownianMotionView()
    {
        InitializeComponent();
        
        _viewModel = (BrownianMotionViewModel)BindingContext;
        _drawable = (BrownianMotionDrawable)Resources["BrownianMotionDrawable"];
        
        _viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(BrownianMotionViewModel.SimulationResults))
            {
                UpdateGraph();
            }
        };
    }

    private void UpdateGraph()
    {
        if (_viewModel.SimulationResults != null)
        {
            _drawable.UpdateData(_viewModel.SimulationResults, _viewModel.LineColors.ToList());
            brownianMotionGraph.Invalidate(); 
        }
    }
}
