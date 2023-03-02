using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Windows.Forms.VisualStyles;

namespace DikeOverflowModel;

public class SettingsView : Control
{
    readonly LogView _logView;
    readonly OverflowGraph _overflowGraph;
    private string _date;
    private double _expGrowth;
    private double _risingSpeed;
    private double _dikeHeight;
    private int _amountOfYears;
    private double _minHeight;
    private double _maxHeight;

    public string Date
    {
        get
        {
            return this._date;
        }
    }

    public double GrowthExponent
    {
        get
        {
            return this._expGrowth;
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

    public int YearAmountGraph
    {
        get
        {
            return this._amountOfYears;
        }
    }

    public double MinHeightGraph
    {
        get
        {
            return this._minHeight;
        }
    }
    
    public double MaxHeightGraph
    {
        get
        {
            return this._maxHeight;
        }
    }

    public DateTime OverflowDate
    {
        get
        {
            return this._overflowGraph.OverflowDate;
        }
    }

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
    private Label _seaSpeed;

    //Sea level fields
    private TextBox _seaExpInput;
    private TextBox _seaRise;

    //Dike data
    private Label _dikeTitle;
    private Label _dikeHeightBox;
    private TextBox _dikeInput;

    //Graph title
    private Label _graphTitle; 

    // Graph data
    private Label _yearAmountLabel;
    private TextBox _yearAmountInput;
    private Label _minHeightLabel;
    private TextBox _minHeightInput;
    private Label _maxHeightLabel;
    private TextBox _maxHeightInput;
    
    //Buttons
    private Button _applyButton;
    private Button _resetButton;

    public SettingsView(LogView lview)
    {
        this._logView = lview;
        
        // Component properties
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
        _seaExp.ClientSize = new Size(300, 30);
        _seaExp.Location = new Point(50, 180);
        _seaExp.BackColor = Color.FromArgb(100, 100, 100);
        _seaExp.ForeColor = Color.LightGray;
        _seaExp.Text = "Speed increase per year (%)";
        _seaExp.Font = new Font("Bahnschrift", 11);

        //Sea level speed title
        _seaSpeed = new Label();
        _seaSpeed.ClientSize = new Size(200, 30);
        _seaSpeed.Location = new Point(50, 210);
        _seaSpeed.BackColor = Color.FromArgb(100, 100, 100);
        _seaSpeed.ForeColor = Color.LightGray;
        _seaSpeed.Text = "Rising speed (cm/decade)";
        _seaSpeed.Font = new Font("Bahnschrift", 11);

        //Sea mode checkbox
        _seaExpInput = new TextBox();
        _seaExpInput.ClientSize = new Size(150, 30);
        _seaExpInput.Location = new Point(400, 180);
        _seaExpInput.BackColor = Color.FromArgb(200, 200, 200);
        _seaExpInput.ForeColor = Color.Black;
        _seaExpInput.Font = new Font("Bahnschrift", 11);
        _seaExpInput.BorderStyle = BorderStyle.None;
        
        //Sea level speed input field
        _seaRise = new TextBox();
        _seaRise.ClientSize = new Size(150, 30);
        _seaRise.Location = new Point(400, 210);
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
        
        // Graph data
        _yearAmountLabel = new Label();
        _yearAmountLabel.ClientSize = new Size(200, 30);
        _yearAmountLabel.Location = new Point(50, 400);
        _yearAmountLabel.BackColor = Color.FromArgb(100, 100, 100);
        _yearAmountLabel.ForeColor = Color.LightGray;
        _yearAmountLabel.Text = "Amount of years";
        _yearAmountLabel.Font = new Font("Bahnschrift", 11);

        _yearAmountInput = new TextBox();
        _yearAmountInput.ClientSize = new Size(150, 30);
        _yearAmountInput.Location = new Point(400, 400);
        _yearAmountInput.BackColor = Color.FromArgb(200, 200, 200);
        _yearAmountInput.ForeColor = Color.Black;
        _yearAmountInput.Font = new Font("Bahnschrift", 11);
        _yearAmountInput.BorderStyle = BorderStyle.None;

        _minHeightLabel = new Label();
        _minHeightLabel.ClientSize = new Size(200, 30);
        _minHeightLabel.Location = new Point(50, 430);
        _minHeightLabel.BackColor = Color.FromArgb(100, 100, 100);
        _minHeightLabel.ForeColor = Color.LightGray;
        _minHeightLabel.Text = "Minimum height";
        _minHeightLabel.Font = new Font("Bahnschrift", 11);
        
        _minHeightInput = new TextBox();
        _minHeightInput.ClientSize = new Size(150, 30);
        _minHeightInput.Location = new Point(400, 430);
        _minHeightInput.BackColor = Color.FromArgb(200, 200, 200);
        _minHeightInput.ForeColor = Color.Black;
        _minHeightInput.Font = new Font("Bahnschrift", 11);
        _minHeightInput.BorderStyle = BorderStyle.None;
        
        _maxHeightLabel = new Label();
        _maxHeightLabel.ClientSize = new Size(200, 30);
        _maxHeightLabel.Location = new Point(50, 460);
        _maxHeightLabel.BackColor = Color.FromArgb(100, 100, 100);
        _maxHeightLabel.ForeColor = Color.LightGray;
        _maxHeightLabel.Text = "Maximum height";
        _maxHeightLabel.Font = new Font("Bahnschrift", 11);
        
        _maxHeightInput = new TextBox();
        _maxHeightInput.ClientSize = new Size(150, 30);
        _maxHeightInput.Location = new Point(400, 460);
        _maxHeightInput.BackColor = Color.FromArgb(200, 200, 200);
        _maxHeightInput.ForeColor = Color.Black;
        _maxHeightInput.Font = new Font("Bahnschrift", 11);
        _maxHeightInput.BorderStyle = BorderStyle.None;

        //Apply button
        _applyButton = new Button();
        _applyButton.ClientSize = new Size(90,45);
        _applyButton.Location = new Point(370, 830);
        _applyButton.BackColor = Color.FromArgb(115, 205, 105);
        _applyButton.ForeColor = Color.White;
        _applyButton.Text = "Apply";
        _applyButton.Font = new Font("Bahnschrift", 10);
        _applyButton.FlatStyle = FlatStyle.Flat;
        _applyButton.FlatAppearance.BorderColor = Color.FromArgb(115, 205, 105);

        //Reset button
        _resetButton = new Button();
        _resetButton.ClientSize = new Size(90, 45);
        _resetButton.Location = new Point(470, 830);
        _resetButton.BackColor = Color.FromArgb(255, 87, 87);
        _resetButton.ForeColor = Color.White;
        _resetButton.Text = "Reset";
        _resetButton.Font = new Font("Bahnschrift", 10);
        _resetButton.FlatStyle = FlatStyle.Flat;
        _resetButton.FlatAppearance.BorderColor = Color.FromArgb(255, 87, 87);

        //Overflow graph 
        _overflowGraph = new OverflowGraph(this); 
        
        // Add all controls
        this.Controls.Add(_yearAmountLabel);
        this.Controls.Add(_yearAmountInput);
        this.Controls.Add(_minHeightLabel);
        this.Controls.Add(_minHeightInput);
        this.Controls.Add(_maxHeightLabel);
        this.Controls.Add(_maxHeightInput);

        this.Controls.Add(_sideBorder);
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);

        this.Controls.Add(_timeTitle);
        this.Controls.Add(_timeLabel); ;
        this.Controls.Add(_timeSelect);

        this.Controls.Add(_seaTitle);
        this.Controls.Add(_seaExp);
        this.Controls.Add(_seaSpeed);

        this.Controls.Add(_seaExpInput);
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
        this._expGrowth = Double.Parse(_seaExpInput.Text);
        this._risingSpeed = Double.Parse(_seaRise.Text);
        this._dikeHeight = Double.Parse(_dikeInput.Text);
        this._amountOfYears = Int32.Parse(_yearAmountInput.Text);
        this._minHeight = Double.Parse(_minHeightInput.Text);
        this._maxHeight = Double.Parse(_maxHeightInput.Text);
        this._overflowGraph.Update(this);
        this._logView.UpdateData(this);
    }

    private void _ResetSettings(object? sender, EventArgs ea)
    {
        this._timeSelect.Value = DateTime.Now;
        this._seaExpInput.Text = "0.0";
        this._seaRise.Text = 2.54.ToString();
        this._dikeInput.Text = 7.ToString();
        this._yearAmountInput.Text = 100.ToString();
        this._minHeightInput.Text = 0.ToString();
        this._maxHeightInput.Text = 10.ToString();
        _ApplyChanges(this, ea);
    }
}