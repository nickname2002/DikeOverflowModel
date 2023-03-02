using System.Diagnostics;

namespace DikeOverflowModel;

public class OverflowGraph : Panel
{
    private SettingsView _settings;
    
    // Component data
    private const int WIDTH = 540;
    private const int HEIGHT = 300;

    // UI components 
    private Label _graphBorder;
    private Label _yIndicatorLabel;
    private Label _xIndicatorLabel;

    public OverflowGraph(SettingsView settings)
    {
        this._settings = settings;
        
        // Component data
        this.ClientSize = new Size(WIDTH, HEIGHT);
        this.Location = new Point(20, 510);
        this.BackColor = Color.White;

        // UI components
        this._yIndicatorLabel = new Label
        {
            ClientSize = new Size(100, 25),
            Location = new Point(5, 5),
            ForeColor = Color.DarkGray,
            Text = "Height (m)",
            Font = new Font("Bahnschrift", 10)
        };
        
        this._xIndicatorLabel = new Label
        {
            ClientSize = new Size(100, 25),
            Location = new Point(WIDTH - 65, HEIGHT - 25),
            ForeColor = Color.DarkGray,
            Text = "Time (y)",
            Font = new Font("Bahnschrift", 10)
        };
        
        // Events
        this.Paint += PaintEvent;

        //Add all Controls
        this.Controls.Add(_xIndicatorLabel);
        this.Controls.Add(_yIndicatorLabel);
        this.Controls.Add(_graphBorder);
    }

    public void Update(SettingsView settings)
    {
        this._settings = settings;
        this.Invalidate();
    }
    
    private void PaintEvent(object? sender, PaintEventArgs ea)
    {
        Graphics gr = ea.Graphics;

        // Properties for calculation
        double dikeHeight = _settings.DikeHeight;
        double seaLevel = 1.04;
        double speedPerYear = _settings.RisingSpeed / 100;
        int yearAmount = _settings.YearAmountGraph; 
        double minHeight = _settings.MinHeightGraph;
        double maxHeight = _settings.MaxHeightGraph;
        double factor = (HEIGHT / maxHeight);

        // NOTE: This implementation currently only support linear
        
        // Draw dike height
        int dikeY = (int)(HEIGHT - (dikeHeight * factor) + (minHeight * factor));
        gr.DrawLine(Pens.Green, new Point(0, dikeY), new Point(WIDTH, dikeY));        
        
        // Draw sea level
        int y0 = (int)(HEIGHT - (seaLevel * factor) + (minHeight * factor));
        int yEnd = (int)(HEIGHT - (seaLevel + (speedPerYear * yearAmount) * factor) + (minHeight * factor));
        gr.DrawLine(Pens.Blue, new Point(0, y0), new Point(WIDTH, yEnd));

        // Draw coordinates
        gr.DrawString(
            $"(0, {dikeHeight})", 
            new Font("Bahnschrift", 10), 
            Brushes.Green, 
            new Point(5, dikeY - 20));
        
        gr.DrawString(
            $"(0, {seaLevel})", 
            new Font("Bahnschrift", 10), 
            Brushes.Blue, 
            new Point(5, y0 - 20));

        gr.DrawString(
            $"({yearAmount}, {seaLevel + (speedPerYear * yearAmount)})", 
            new Font("Bahnschrift", 10), 
            Brushes.Blue, 
            new Point(WIDTH - 90, yEnd - 20));
    }
}