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
    private Label _dateBox;
    private Button _playButton;
    private Button _pauseButton;
    
    // Simulation panel
    private Panel _sim;

    public SimulationView()
    {
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
        
        // Date
        _dateBox = new Label();
        _dateBox.ClientSize = new Size(100, 25);
        _dateBox.Location = new Point(0, 60);
        _dateBox.ForeColor = Color.White;
        _dateBox.BackColor = Color.Black;
        _dateBox.TextAlign = ContentAlignment.MiddleCenter;
        _dateBox.Text = DateTime.Now.ToShortDateString();
        _dateBox.Font = new Font("Bahnschrift", 12);

        // Simulation panel
        this._sim = new Panel();
        this._sim.ClientSize = new Size(SIMULATION_WIDTH, SIMULATION_HEIGHT);
        this._sim.Location = new Point(0, 8);
        this._sim.BackColor = Color.Black; // NOTE: Backcolor needs to be changed (maybe rect grid?)
        
        // Play button
        _playButton = new Button();
        _playButton.ClientSize = new Size(60,45);
        _playButton.Location = new Point(860, 5);
        _playButton.BackColor = Color.FromArgb(115, 205, 105);
        _playButton.ForeColor = Color.White;
        _playButton.Text = "Play";
        _playButton.Font = new Font("Bahnschrift", 10);
        _playButton.FlatStyle = FlatStyle.Flat;
        _playButton.FlatAppearance.BorderColor = Color.FromArgb(115, 205, 105);

        // Pause button
        _pauseButton = new Button();
        _pauseButton.ClientSize = new Size(70, 45);
        _pauseButton.Location = new Point(930, 5);
        _pauseButton.BackColor = Color.FromArgb(87, 87, 255);
        _pauseButton.ForeColor = Color.White;
        _pauseButton.Text = "Pause";
        _pauseButton.Font = new Font("Bahnschrift", 10);
        _pauseButton.FlatStyle = FlatStyle.Flat;
        _pauseButton.TextAlign = ContentAlignment.MiddleCenter;
        _pauseButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 255);
        
        // UI components 
        this._logView = new LogView();
        this._settingsView = new SettingsView(_logView);

        // Add all controls
        this.Controls.Add(_dateBox);
        this.Controls.Add(_playButton);
        this.Controls.Add(_pauseButton);
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);
        this.Controls.Add(_logView);
        this.Controls.Add(_settingsView);
        this.Controls.Add(_sim);
        
        this._logView.UpdateData(this._settingsView);
    }
}