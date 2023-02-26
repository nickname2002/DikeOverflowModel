namespace DikeOverflowModel;

public class SettingsView : Control
{
    private OverflowGraph _overflowGraph;
    
    // Component data
    private const int WIDTH = 600;
    private const int HEIGHT = 900;

    // UI components
    private Label _simBar;
    private Label _simBarBorder;
    private Label _sideBorder;
    private Label _titleBox;

    public SettingsView()
    {
        this.BackColor = Color.FromArgb(100, 100, 100);
        this.Location = new Point(1000, 0);
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
        
        // Side border
        _sideBorder = new Label();
        _sideBorder.ClientSize = new Size(3, HEIGHT);
        _sideBorder.Location = new Point(0, 0);
        _sideBorder.BackColor = Color.FromArgb(40, 40, 40);

        // Title
        _titleBox = new Label();
        _titleBox.ClientSize = new Size(200, 30);
        _titleBox.Location = new Point(10, 10);
        _titleBox.BackColor = Color.FromArgb(50, 50, 50);
        _titleBox.ForeColor = Color.White;
        _titleBox.Text = "Settings";
        _titleBox.Font = new Font("Bahnschrift", 15);
        
        // Add all controls
        this.Controls.Add(_sideBorder);
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);
    }
}