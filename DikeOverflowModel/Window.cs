namespace DikeOverflowModel;

public class Window : Form
{
    private SimulationView _simulationView;
    
    public Window()
    {
        this.ClientSize = new Size(1600, 900);
        this.Text = "DikeSim";
        this.DoubleBuffered = true;
        this._simulationView = new SimulationView();

        // Disable resizable window
        this.MinimumSize = this.ClientSize;
        this.MaximumSize = this.ClientSize;
        this.MinimizeBox = false;
        this.MaximizeBox = false;
        
        // Add all controls
        this.Controls.Add(_simulationView);
    }
}