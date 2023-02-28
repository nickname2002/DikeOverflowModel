namespace DikeOverflowModel;

public class SettingsView : Control
{
    private OverflowGraph _overflowGraph;

    public string Date
    {
        get
        {
            return this._date;
        }
    }

    public double RisingSpeed
    {
        get
        {
            return _risingSpeed;
        }
    }
    
    public double DikeHeight
    {
        get
        {
            return this._dikeHeight;
        }
    }

    private string _date;
    private bool _expChecked;
    private bool _posChecked;
    private double _risingSpeed;
    private double _dikeHeight;
    
    // Component data
    private const int WIDTH = 600;
    private const int HEIGHT = 900;

    // UI components
    private Label _simBar;
    private Label _simBarBorder;
    private Label _sideBorder;
    private Label _titleBox;

    //Time data
    private Label _timeTitle;
    private Label _timeLabel;
    private Label _timeMonth;
    private Label _timeYear;

    //Time input fields
    private DateTimePicker _timeSelect;    

    //Sea level data
    private Label _seaTitle;
    private Label _seaExp;
    private Label _seaPos;
    private Label _seaSpeed;

    //Sea level fields
    private CheckBox _seaMode;
    private CheckBox _seaDirection;
    private TextBox _seaRise;

    //Dike data
    private Label _dikeTitle;
    private Label _dikeHeightBox;
    private TextBox _dikeInput;

    //Graph title
    private Label _graphTitle; 

    //Buttons
    private Button _applyButton;
    private Button _resetButton;

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

        //Time title
        _timeTitle = new Label();
        _timeTitle.ClientSize = new Size(200, 30);
        _timeTitle.Location = new Point(10, 60);
        _timeTitle.BackColor = Color.FromArgb(100, 100, 100);
        _timeTitle.ForeColor = Color.White;
        _timeTitle.Text = "Time";
        _timeTitle.Font = new Font("Bahnschrift", 13);

        //Time select title
        _timeLabel = new Label();
        _timeLabel.ClientSize = new Size(200, 30);
        _timeLabel.Location = new Point(50, 90);
        _timeLabel.BackColor = Color.FromArgb(100, 100, 100);
        _timeLabel.ForeColor = Color.LightGray;
        _timeLabel.Text = "Time";
        _timeLabel.Font = new Font("Bahnschrift", 11);

        // Time input field
        _timeSelect = new DateTimePicker();
        _timeSelect.ClientSize = new Size(275, 30);
        _timeSelect.Location = new Point(275, 90);
        _timeSelect.BackColor = Color.FromArgb(200, 200, 200);
        _timeSelect.ForeColor = Color.Black;
        _timeSelect.Font = new Font("Bahnschrift", 11);

        //Sea level title
        _seaTitle = new Label();
        _seaTitle.ClientSize = new Size(200, 30);
        _seaTitle.Location = new Point(10, 150);
        _seaTitle.BackColor = Color.FromArgb(100, 100, 100);
        _seaTitle.ForeColor = Color.White;
        _seaTitle.Text = "Sea level";
        _seaTitle.Font = new Font("Bahnschrift", 13);

        //Sea graph mode title
        _seaExp = new Label();
        _seaExp.ClientSize = new Size(200, 30);
        _seaExp.Location = new Point(50, 180);
        _seaExp.BackColor = Color.FromArgb(100, 100, 100);
        _seaExp.ForeColor = Color.LightGray;
        _seaExp.Text = "Exponential";
        _seaExp.Font = new Font("Bahnschrift", 11);

        //Sea level direction title
        _seaPos = new Label();
        _seaPos.ClientSize = new Size(200, 30);
        _seaPos.Location = new Point(50, 210);
        _seaPos.BackColor = Color.FromArgb(100, 100, 100);
        _seaPos.ForeColor = Color.LightGray;
        _seaPos.Text = "Positive";
        _seaPos.Font = new Font("Bahnschrift", 11);

        //Sea level speed title
        _seaSpeed = new Label();
        _seaSpeed.ClientSize = new Size(200, 30);
        _seaSpeed.Location = new Point(50, 240);
        _seaSpeed.BackColor = Color.FromArgb(100, 100, 100);
        _seaSpeed.ForeColor = Color.LightGray;
        _seaSpeed.Text = "Rising speed (cm/decade)";
        _seaSpeed.Font = new Font("Bahnschrift", 11);

        //Sea mode checkbox
        _seaMode = new CheckBox();
        _seaMode.Location = new Point(535, 180);

        //Sea direction checkbox
        _seaDirection = new CheckBox();
        _seaDirection.Location = new Point(535, 210); 
        
        //Sea level speed input field
        _seaRise = new TextBox();
        _seaRise.ClientSize = new Size(150, 30);
        _seaRise.Location = new Point(400, 240);
        _seaRise.BackColor = Color.FromArgb(200, 200, 200);
        _seaRise.ForeColor = Color.Black;
        _seaRise.Font = new Font("Bahnschrift", 11);
        _seaRise.BorderStyle = BorderStyle.None;

        //Dike title
        _dikeTitle = new Label();
        _dikeTitle.ClientSize = new Size(200, 30);
        _dikeTitle.Location = new Point(10, 290);
        _dikeTitle.BackColor = Color.FromArgb(100, 100, 100);
        _dikeTitle.ForeColor = Color.White;
        _dikeTitle.Text = "Dike";
        _dikeTitle.Font = new Font("Bahnschrift", 13);

        //Dike height title
        _dikeHeightBox = new Label();
        _dikeHeightBox.ClientSize = new Size(200, 30);
        _dikeHeightBox.Location = new Point(50, 320);
        _dikeHeightBox.BackColor = Color.FromArgb(100, 100, 100);
        _dikeHeightBox.ForeColor = Color.LightGray;
        _dikeHeightBox.Text = "Height (m)";
        _dikeHeightBox.Font = new Font("Bahnschrift", 11);

        //Sea level speed input field
        _dikeInput = new TextBox();
        _dikeInput.ClientSize = new Size(150, 30);
        _dikeInput.Location = new Point(400, 320);
        _dikeInput.BackColor = Color.FromArgb(200, 200, 200);
        _dikeInput.ForeColor = Color.Black;
        _dikeInput.Font = new Font("Bahnschrift", 11);
        _dikeInput.BorderStyle = BorderStyle.None;

        //Graph title
        _graphTitle = new Label();
        _graphTitle.ClientSize = new Size(200, 30);
        _graphTitle.Location = new Point(10, 370);
        _graphTitle.BackColor = Color.FromArgb(100, 100, 100);
        _graphTitle.ForeColor = Color.White;
        _graphTitle.Text = "Overflow graph";
        _graphTitle.Font = new Font("Bahnschrift", 13);

        //Apply button
        _applyButton = new Button();
        _applyButton.ClientSize = new Size(90,45);
        _applyButton.Location = new Point(370, 790);
        _applyButton.BackColor = Color.FromArgb(115, 205, 105);
        _applyButton.ForeColor = Color.White;
        _applyButton.Text = "Apply";
        _applyButton.Font = new Font("Bahnschrift", 10);
        _applyButton.FlatStyle = FlatStyle.Flat;
        _applyButton.FlatAppearance.BorderColor = Color.FromArgb(115, 205, 105);

        //Reset button
        _resetButton = new Button();
        _resetButton.ClientSize = new Size(90, 45);
        _resetButton.Location = new Point(470, 790);
        _resetButton.BackColor = Color.FromArgb(255, 87, 87);
        _resetButton.ForeColor = Color.White;
        _resetButton.Text = "Reset";
        _resetButton.Font = new Font("Bahnschrift", 10);
        _resetButton.FlatStyle = FlatStyle.Flat;
        _resetButton.FlatAppearance.BorderColor = Color.FromArgb(255, 87, 87);

        //Overflow graph 
        _overflowGraph = new OverflowGraph(this); 
        
        // Add all controls
        this.Controls.Add(_sideBorder);
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);

        this.Controls.Add(_timeTitle);
        this.Controls.Add(_timeLabel); ;
        this.Controls.Add(_timeSelect);

        this.Controls.Add(_seaTitle);
        this.Controls.Add(_seaExp);
        this.Controls.Add(_seaPos);
        this.Controls.Add(_seaSpeed);

        this.Controls.Add(_seaMode);
        this.Controls.Add(_seaDirection);
        this.Controls.Add(_seaRise);

        this.Controls.Add(_dikeTitle);
        this.Controls.Add(_dikeHeightBox);
        this.Controls.Add(_dikeInput);

        this.Controls.Add(_graphTitle); 
        this.Controls.Add(_overflowGraph);

        this.Controls.Add(_applyButton);
        this.Controls.Add(_resetButton); 
        
        // Events
        this._applyButton.Click += _ApplyChanges;
        this._resetButton.Click += _ResetSettings;
        this._ResetSettings(null, null);
    }
    
    private void _ApplyChanges(object? sender, EventArgs ea)
    {
        this._date = _timeSelect.Value.ToShortDateString();
        this._expChecked = _seaMode.Checked;
        this._posChecked = _seaDirection.Checked;
        this._risingSpeed = Double.Parse(_seaRise.Text);
        this._dikeHeight = Double.Parse(_dikeInput.Text);
        this._overflowGraph.Update(this);
    }

    private void _ResetSettings(object? sender, EventArgs ea)
    {
        this._timeSelect.Value = DateTime.Now;
        this._seaMode.Checked = false;
        this._seaDirection.Checked = true;
        this._seaRise.Text = 2.54.ToString();
        this._dikeInput.Text = 7.ToString();
        _ApplyChanges(this, ea);
    }
}