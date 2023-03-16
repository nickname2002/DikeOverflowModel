namespace DikeOverflowModel;

public class Window : Form
{
    private SimulationView _simulationView;
    

    public Window()
    {
        this.ClientSize = new Size(1600, 930);
        this.Text = "OverflowSim";
        this.DoubleBuffered = true;
        this._simulationView = new SimulationView();
        this.Icon = Properties.Resources.overflow_sim_icon;

        // Disable resizable window
        this.MinimumSize = this.ClientSize;
        this.MaximumSize = this.ClientSize;
        this.MinimizeBox = true;
        this.MaximizeBox = false;
        
        // Add all controls
        this.Controls.Add(_simulationView);
    }
}