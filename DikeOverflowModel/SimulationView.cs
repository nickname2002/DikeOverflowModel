namespace DikeOverflowModel;

public class SimulationView : Control
{
    // Simulation components
    private SettingsView _settingsView;
    private LogView _logView;

    // Component data
    private const int SIMULATION_WIDTH = 1000;
    private const int SIMULATION_HEIGHT = 592;

    // UI components
    private Label _simBar;
    private Label _simBarBorder;
    private Label _titleBox;
    private Button _applyButton;
    private Button _resetButton;
    
    // Simulation panel
    private Panel _sim;

    public SimulationView()
    {
        // UI components 
        this._logView = new LogView();
        this._settingsView = new SettingsView();
        
        // Component properties
        this.Location = new Point(0, 0);
        this.ClientSize = new Size(1600, 900);
        
        // Top bar
        _simBar = new Label();
        _simBar.ClientSize = new Size(SIMULATION_WIDTH, 50);
        _simBar.Location = new Point(0, 0);
        _simBar.BackColor = Color.FromArgb(50, 50, 50);
        
        _simBarBorder = new Label();
        _simBarBorder.ClientSize = new Size(1600, 5);
        _simBarBorder.Location = new Point(0, 50);
        _simBarBorder.BackColor = Color.FromArgb(40, 40, 40);
        
        // Title
        _titleBox = new Label();
        _titleBox.ClientSize = new Size(200, 30);
        _titleBox.Location = new Point(10, 10);
        _titleBox.BackColor = Color.FromArgb(50, 50, 50);
        _titleBox.ForeColor = Color.White;
        _titleBox.Text = "Simulation";
        _titleBox.Font = new Font("Bahnschrift", 15);
        
        // Simulation panel
        this._sim = new Panel();
        this._sim.ClientSize = new Size(SIMULATION_WIDTH, SIMULATION_HEIGHT);
        this._sim.Location = new Point(0, 8);
        this._sim.BackColor = Color.Black; // NOTE: Backcolor needs to be changed (maybe rect grid?)
        
        // Add all controls
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);
        this.Controls.Add(_logView);
        this.Controls.Add(_settingsView);
        this.Controls.Add(_sim);
    }
}