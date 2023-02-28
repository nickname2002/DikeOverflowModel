using System.Diagnostics;

namespace DikeOverflowModel;

public class OverflowGraph : Panel
{
    private SettingsView _settings;
    
    //Component data
    private const int WIDTH = 540;
    private const int HEIGHT = 300;

    //UI components 
    private Label _graphBorder;
    private Label _yParameter;
    private Label _xParameter;

    public OverflowGraph(SettingsView settings)
    {
        this._settings = settings;
        
        // Component data
        this.ClientSize = new Size(WIDTH, HEIGHT);
        this.Location = new Point(20, 410);
        this.BackColor = Color.White;

        // Events
        this.Paint += PaintEvent;

        //Add all Controls
        this.Controls.Add(_graphBorder);
    }

    public void Update(SettingsView settings)
    {
        this._settings = settings;
        this.Invalidate();
    }
    
    // TODO: add multiple scales for drawing the chart
    private void PaintEvent(object? sender, PaintEventArgs ea)
    {
        Graphics gr = ea.Graphics;

        // Properties for calculation
        double dikeHeight = _settings.DikeHeight;
        double seaLevel = 1.04; // TODO: Might be smart to make this variable
        double speedPerYear = _settings.RisingSpeed;
        int factor = 10; // TODO: Make this variable
        int yearAmount = 100; // TODO: Make this variable

        // Draw dike height
        int dikeY = (int)(HEIGHT - dikeHeight * factor);
        gr.DrawLine(Pens.Green, new Point(0, dikeY), new Point(WIDTH, dikeY));        
        
        // Draw sea level
        int y0 = (int)(HEIGHT - (seaLevel * factor));
        int yEnd = (int)(HEIGHT - (seaLevel + speedPerYear * (WIDTH / yearAmount) * factor));
        gr.DrawLine(Pens.Blue, new Point(0, y0), new Point(WIDTH, yEnd));
    }
}