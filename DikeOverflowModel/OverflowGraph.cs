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
        this.Location = new Point(20, 510);
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
        double seaLevel = 1.04;
        double speedPerYear = _settings.RisingSpeed;
        int yearAmount = _settings.YearAmountGraph; 
        double minHeight = _settings.MinHeightGraph;
        double maxHeight = _settings.MaxHeightGraph;
        double factor = (HEIGHT / maxHeight);
        int X = (yearAmount / WIDTH);

        // Draw dike height
        int dikeY = (int)(HEIGHT - (dikeHeight * factor) + (minHeight * factor));
        gr.DrawLine(Pens.Green, new Point(0, dikeY), new Point(WIDTH, dikeY));        
        
        // Draw sea level
        int y0 = (int)(HEIGHT - (seaLevel * factor) + (minHeight * factor));
        int yEnd = (int)(HEIGHT - (seaLevel + speedPerYear * X * factor) + (minHeight * factor));
        gr.DrawLine(Pens.Blue, new Point(0, y0), new Point(WIDTH, yEnd));
    }
}