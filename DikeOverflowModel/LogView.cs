using System.Windows.Forms.VisualStyles;

namespace DikeOverflowModel;

public class LogView : Control
{
    // Component data
    private const int WIDTH = 1000;
    private const int HEIGHT = 300;

    // UI components
    private Label _simBar;
    private Label _simBarBorder;
    private Label _titleBox;
    
    // Data container
    private Label _logData;
    
    // Log properties
    public double WaterHeight;
    public double WaveReach;
    public double HeightIn50Years;
    public string OverflowDate;
    public double DikeHeight;
    
    public LogView()
    {
        // Property initialization
        this.WaterHeight = 104;
        this.WaveReach = 0;
        this.HeightIn50Years = 0;
        this.OverflowDate = null;
        this.DikeHeight = 0;
        
        // Component properties
        this.BackColor = Color.FromArgb(100, 100, 100);
        this.Location = new Point(0, 600);
        this.ClientSize = new Size(WIDTH, HEIGHT);

        // Top bar
        _simBar = new Label();
        _simBar.ClientSize = new Size(WIDTH, 50);
        _simBar.Location = new Point(0, 0);
        _simBar.BackColor = Color.FromArgb(50, 50, 50);
        
        _simBarBorder = new Label();
        _simBarBorder.ClientSize = new Size(WIDTH, 5);
        _simBarBorder.Location = new Point(0, 50);
        _simBarBorder.BackColor = Color.FromArgb(40, 40, 40);
        
        // Title
        _titleBox = new Label();
        _titleBox.ClientSize = new Size(200, 30);
        _titleBox.Location = new Point(10, 10);
        _titleBox.BackColor = Color.FromArgb(50, 50, 50);
        _titleBox.ForeColor = Color.White;
        _titleBox.Text = "Log";
        _titleBox.Font = new Font("Bahnschrift", 15);
        
        // Logdata container
        _logData = new Label();
        _logData.ClientSize = new Size(WIDTH, HEIGHT - 30);
        _logData.Location = new Point(10, 70);
        _logData.ForeColor = Color.White;
        _logData.Font = new Font("Bahnschrift", 12);

        // Add all controls
        this.Controls.Add(_logData);
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);
    }

    /// <summary>
    /// Updates the data in the log view
    /// TODO: Add variables for tracking
    /// </summary>
    public void UpdateData(double dikeHeight)
    {
        this.DikeHeight = dikeHeight;
        
        this._logData.Text = "";
        this._logData.Text += $"Water height: {this.WaterHeight}cm\n";
        this._logData.Text += $"Wave reach: {this.WaveReach}\n";
        this._logData.Text += $"Height in 50 years: {this.HeightIn50Years}\n";
        this._logData.Text += $"Overflow date: {this.OverflowDate}\n";
        this._logData.Text += $"Dike height: {this.DikeHeight}\n";
    }
}