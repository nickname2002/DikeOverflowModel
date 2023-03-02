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
        double yearAmount = _settings.YearAmountGraph; 
        double minHeight = _settings.MinHeightGraph;
        double maxHeight = _settings.MaxHeightGraph;
        double factor = (HEIGHT / maxHeight);

        // NOTE: This implementation currently only support linear

        Point dikePrev = new Point();
        Point waterPrev = new Point();
        
        for (int i = 0; i < WIDTH; i++)
        {
            if (i == 0)
            {
                dikePrev = new Point(i, (int)(HEIGHT - (dikeHeight * factor) + (minHeight * factor) + (minHeight * factor)));
                waterPrev = new Point(i, (int)(HEIGHT - (seaLevel * factor) + (minHeight * factor)));
                continue;
            }
            
            Point newDike;
            Point newWater;
            
            int dikeX = i;
            int dikeY = (int)(HEIGHT - (dikeHeight * factor) + (minHeight * factor));

            int waterX = i;
            int waterY = (int)(HEIGHT - ((seaLevel + (speedPerYear * i * (yearAmount / WIDTH))) * factor) + (minHeight * factor));

            // Create new points
            newDike = new Point(dikeX, dikeY);
            newWater = new Point(waterX, waterY);
            
            // Draw lines
            gr.DrawLine(Pens.Green, dikePrev, newDike);
            gr.DrawLine(Pens.Blue, waterPrev, newWater);
            
            // Set new prev points
            dikePrev = newDike;
            waterPrev = newWater;
        }
        
        // Dike height
        int dikeYStart = (int)(HEIGHT - (dikeHeight * factor) + (minHeight * factor));
        
        // Sea level
        int waterYStart = (int)(HEIGHT - (seaLevel * factor) + (minHeight * factor));
        int waterYEnd = (int)(HEIGHT - (seaLevel + (speedPerYear * yearAmount) * factor) + (minHeight * factor));
        
        // Draw coordinates
        gr.DrawString(
            $"(0, {dikeHeight})", 
            new Font("Bahnschrift", 10), 
            Brushes.Green, 
            new Point(5, dikeYStart - 20));
        
        gr.DrawString(
            $"({yearAmount}, {dikeHeight})", 
            new Font("Bahnschrift", 10), 
            Brushes.Green, 
            new Point(WIDTH - 90, dikeYStart - 20));
        
        gr.DrawString(
            $"(0, {seaLevel})", 
            new Font("Bahnschrift", 10), 
            Brushes.Blue, 
            new Point(5, waterYStart - 20));

        gr.DrawString(
            $"({yearAmount}, {seaLevel + (speedPerYear * yearAmount)})", 
            new Font("Bahnschrift", 10), 
            Brushes.Blue, 
            new Point(WIDTH - 90, waterYEnd - 20));
    }
}