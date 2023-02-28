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

    //Time data
    private Label _timeTitle;
    private Label _timeDay;
    private Label _timeMonth;
    private Label _timeYear;

    //Time input fields
    private TextBox _timeFieldDay;    
    private TextBox _timeFieldMonth; 
    private TextBox _timeFieldYear;

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
    private Label _dikeHeight;
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

        //Time day title
        _timeDay = new Label();
        _timeDay.ClientSize = new Size(200, 30);
        _timeDay.Location = new Point(50, 90);
        _timeDay.BackColor = Color.FromArgb(100, 100, 100);
        _timeDay.ForeColor = Color.LightGray;
        _timeDay.Text = "Day";
        _timeDay.Font = new Font("Bahnschrift", 11);

        //Time month title
        _timeMonth = new Label();
        _timeMonth.ClientSize = new Size(200, 30);
        _timeMonth.Location = new Point(50, 120);
        _timeMonth.BackColor = Color.FromArgb(100, 100, 100);
        _timeMonth.ForeColor = Color.LightGray;
        _timeMonth.Text = "Month";
        _timeMonth.Font = new Font("Bahnschrift", 11);

        //Time year title
        _timeYear = new Label();
        _timeYear.ClientSize = new Size(200, 30);
        _timeYear.Location = new Point(50, 150);
        _timeYear.BackColor = Color.FromArgb(100, 100, 100);
        _timeYear.ForeColor = Color.LightGray;
        _timeYear.Text = "Year";
        _timeYear.Font = new Font("Bahnschrift", 11);

        //Day input field
        _timeFieldDay = new TextBox();
        _timeFieldDay.ClientSize = new Size(150, 30);
        _timeFieldDay.Location = new Point(400, 90);
        _timeFieldDay.BackColor = Color.FromArgb(200, 200, 200);
        _timeFieldDay.ForeColor = Color.Black;
        _timeFieldDay.Font = new Font("Bahnschrift", 11);
        _timeFieldDay.BorderStyle = BorderStyle.None;

        //Month input field
        _timeFieldMonth = new TextBox();
        _timeFieldMonth.ClientSize = new Size(150, 30);
        _timeFieldMonth.Location = new Point(400, 120);
        _timeFieldMonth.BackColor = Color.FromArgb(200, 200, 200);
        _timeFieldMonth.ForeColor = Color.Black;
        _timeFieldMonth.Font = new Font("Bahnschrift", 11);
        _timeFieldMonth.BorderStyle = BorderStyle.None;

        //Year input field
        _timeFieldYear = new TextBox();
        _timeFieldYear.ClientSize = new Size(150, 30);
        _timeFieldYear.Location = new Point(400, 150);
        _timeFieldYear.BackColor = Color.FromArgb(200, 200, 200);
        _timeFieldYear.ForeColor = Color.Black;
        _timeFieldYear.Font = new Font("Bahnschrift", 11);
        _timeFieldYear.BorderStyle = BorderStyle.None;

        //Sea level title
        _seaTitle = new Label();
        _seaTitle.ClientSize = new Size(200, 30);
        _seaTitle.Location = new Point(10, 200);
        _seaTitle.BackColor = Color.FromArgb(100, 100, 100);
        _seaTitle.ForeColor = Color.White;
        _seaTitle.Text = "Sea level";
        _seaTitle.Font = new Font("Bahnschrift", 13);

        //Sea graph mode title
        _seaExp = new Label();
        _seaExp.ClientSize = new Size(200, 30);
        _seaExp.Location = new Point(50, 230);
        _seaExp.BackColor = Color.FromArgb(100, 100, 100);
        _seaExp.ForeColor = Color.LightGray;
        _seaExp.Text = "Exponential";
        _seaExp.Font = new Font("Bahnschrift", 11);

        //Sea level direction title
        _seaPos = new Label();
        _seaPos.ClientSize = new Size(200, 30);
        _seaPos.Location = new Point(50, 260);
        _seaPos.BackColor = Color.FromArgb(100, 100, 100);
        _seaPos.ForeColor = Color.LightGray;
        _seaPos.Text = "Positive";
        _seaPos.Font = new Font("Bahnschrift", 11);

        //Sea level speed title
        _seaSpeed = new Label();
        _seaSpeed.ClientSize = new Size(200, 30);
        _seaSpeed.Location = new Point(50, 290);
        _seaSpeed.BackColor = Color.FromArgb(100, 100, 100);
        _seaSpeed.ForeColor = Color.LightGray;
        _seaSpeed.Text = "Rising speed (m/v)";
        _seaSpeed.Font = new Font("Bahnschrift", 11);

        //Sea mode checkbox
        _seaMode = new CheckBox();
        _seaMode.Location = new Point(535, 230);

        //Sea direction checkbox
        _seaDirection = new CheckBox();
        _seaDirection.Location = new Point(535, 260); 
        
        //Sea level speed input field
        _seaRise = new TextBox();
        _seaRise.ClientSize = new Size(150, 30);
        _seaRise.Location = new Point(400, 290);
        _seaRise.BackColor = Color.FromArgb(200, 200, 200);
        _seaRise.ForeColor = Color.Black;
        _seaRise.Font = new Font("Bahnschrift", 11);
        _seaRise.BorderStyle = BorderStyle.None;

        //Dike title
        _dikeTitle = new Label();
        _dikeTitle.ClientSize = new Size(200, 30);
        _dikeTitle.Location = new Point(10, 340);
        _dikeTitle.BackColor = Color.FromArgb(100, 100, 100);
        _dikeTitle.ForeColor = Color.White;
        _dikeTitle.Text = "Dike";
        _dikeTitle.Font = new Font("Bahnschrift", 13);

        //Dike height title
        _dikeHeight = new Label();
        _dikeHeight.ClientSize = new Size(200, 30);
        _dikeHeight.Location = new Point(50, 370);
        _dikeHeight.BackColor = Color.FromArgb(100, 100, 100);
        _dikeHeight.ForeColor = Color.LightGray;
        _dikeHeight.Text = "Height (m)";
        _dikeHeight.Font = new Font("Bahnschrift", 11);

        //Sea level speed input field
        _dikeInput = new TextBox();
        _dikeInput.ClientSize = new Size(150, 30);
        _dikeInput.Location = new Point(400, 370);
        _dikeInput.BackColor = Color.FromArgb(200, 200, 200);
        _dikeInput.ForeColor = Color.Black;
        _dikeInput.Font = new Font("Bahnschrift", 11);
        _dikeInput.BorderStyle = BorderStyle.None;

        //Graph title
        _graphTitle = new Label();
        _graphTitle.ClientSize = new Size(200, 30);
        _graphTitle.Location = new Point(10, 420);
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
        _overflowGraph = new OverflowGraph(); 
        
        // Add all controls
        this.Controls.Add(_sideBorder);
        this.Controls.Add(_titleBox);
        this.Controls.Add(_simBar);
        this.Controls.Add(_simBarBorder);

        this.Controls.Add(_timeTitle);
        this.Controls.Add(_timeDay);
        this.Controls.Add(_timeMonth);
        this.Controls.Add(_timeYear);

        this.Controls.Add(_timeFieldDay);
        this.Controls.Add(_timeFieldMonth);
        this.Controls.Add(_timeFieldYear);

        this.Controls.Add(_seaTitle);
        this.Controls.Add(_seaExp);
        this.Controls.Add(_seaPos);
        this.Controls.Add(_seaSpeed);

        this.Controls.Add(_seaMode);
        this.Controls.Add(_seaDirection);
        this.Controls.Add(_seaRise);

        this.Controls.Add(_dikeTitle);
        this.Controls.Add(_dikeHeight);
        this.Controls.Add(_dikeInput);

        this.Controls.Add(_graphTitle); 
        this.Controls.Add(_overflowGraph);

        this.Controls.Add(_applyButton);
        this.Controls.Add(_resetButton); 
    }
}