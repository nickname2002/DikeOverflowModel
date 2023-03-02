using System.Diagnostics;

namespace DikeOverflowModel;

public class OverflowGraph : Panel
{
    private SettingsView _settings;
    private Point? _intersectionPoint;
    
    // Component data
    private const double WIDTH = 540;
    private const double HEIGHT = 300;

    // UI components 
    private Label _graphBorder;
    private Label _yIndicatorLabel;
    private Label _xIndicatorLabel;

    public double YearsUntilOverflow
    {
        get
        {
            if (this._intersectionPoint.Value.X == -1000)
            {
                return -1000;
            }
            
            return this._intersectionPoint.Value.X;
        }
    }

    public DateTime OverflowDate
    {
        get
        {
            DateTime currentDate = Convert.ToDateTime(_settings.Date); 
            return currentDate.AddDays(this._DaysUntilOverflow());
        }
    }
    
    // TODO: In the graph, take into account the amount the input date for drawing 
    // TODO: Either graph calculation or final result calculation is (slightly) wrong
    // Taking parsing/rounding errors into account
    
    public OverflowGraph(SettingsView settings)
    {
        this._settings = settings;
        this._intersectionPoint = new Point(-1000, -1000);
        
        // Component data
        this.ClientSize = new Size((int)WIDTH, (int)HEIGHT);
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
            Location = new Point((int)WIDTH - 65, (int)HEIGHT - 25),
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
        double expGrowth = 1 + (_settings.GrowthExponent / 100);
        
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
            double dikeY = HEIGHT - (dikeHeight * factor) + (minHeight * factor);

            int waterX = i;
            double waterY = HEIGHT - ((seaLevel + (speedPerYear * Math.Pow(i, expGrowth) * (yearAmount / WIDTH)))) * factor + (minHeight * factor);

            // Create new points
            newDike = new Point(dikeX, (int)dikeY);
            newWater = new Point(waterX, (int)waterY);

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
        double waterYStart = (int)(HEIGHT - (seaLevel * factor) + (minHeight * factor));
        double waterYEnd = HEIGHT - (seaLevel + (speedPerYear * Math.Pow(yearAmount, expGrowth)) * factor) + (minHeight * factor);
        
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
            new Point((int)WIDTH - 90, dikeYStart - 20));
        
        gr.DrawString(
            $"(0, {seaLevel})", 
            new Font("Bahnschrift", 10), 
            Brushes.Blue, 
            new Point(5, (int)waterYStart - 20));

        gr.DrawString(
            $"({yearAmount}, {seaLevel + (speedPerYear * Math.Pow(yearAmount, expGrowth))})", 
            new Font("Bahnschrift", 10), 
            Brushes.Blue, 
            new Point((int)WIDTH - 90, (int)waterYEnd - 20));
    }

    private double _DiffBetweenPoints(Point p1, Point p2)
    {
        double xSquared = Math.Pow((p2.X - p1.X), 2);
        double ySquared = Math.Pow((p2.Y - p1.Y), 2);
        return Math.Sqrt(xSquared + ySquared);
    }

    private double _DaysUntilOverflow()
    {
        if (this.YearsUntilOverflow == -1000)
        {
            return -1000;
        }

        return this.YearsUntilOverflow * 365;
    }
}