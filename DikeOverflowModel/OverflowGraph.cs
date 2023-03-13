using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace DikeOverflowModel;

public class OverflowGraph : Panel
{
    private SettingsView _settings;
    public const double SEA_LEVEL = 0.0;
    
    // Component data
    private const double WIDTH = 540;
    private const double HEIGHT = 300;

    // UI components 
    private Label _yIndicatorLabel;
    private Label _xIndicatorLabel;

    public double YearsUntilOverflow
    {
        get
        {
            return this.CalcIntersectionPoint().ToTuple().Item1;
        }
    }

    public DateTime OverflowDate
    {
        get
        {
            double time = this.CalcIntersectionPoint().ToTuple().Item1;
            return DateTime.Now.AddDays(time * 365);
        }
    }

    public double HeightIn50Years
    {
        get
        {
            return this.CalcSeaLevel(50);
        }
    }
    
    public OverflowGraph(SettingsView settings)
    {
        this._settings = settings;

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
    }

    /// <summary>
    /// Update graph representation
    /// </summary>
    /// <param name="settings"></param>
    public void Update(SettingsView settings)
    {
        this._settings = settings;
        this.Invalidate();
    }
    
    private void PaintEvent(object? sender, PaintEventArgs ea)
    {
        Graphics gr = ea.Graphics;
        this.DrawGraphLines(gr);
        this.DrawCoords(gr);
        this.DrawIntersectionPoint(gr);
    }

    /// <summary>
    /// Draw the contents of the graph.
    /// </summary>
    /// <param name="gr"></param>
    private void DrawGraphLines(Graphics gr)
    {
        Point dikePrev = new Point();
        Point waterPrev = new Point();

        for (int i = 0; i < WIDTH; i++)
        {
            if (i == 0)
            {
                dikePrev = CalcDikeGraphCoords(i);
                waterPrev = CalcWaterGraphCoords(i);
                continue;
            }
            
            // Create new points
            Point newWater = CalcWaterGraphCoords(i);
            Point newDike = CalcDikeGraphCoords(i);

            // Draw lines
            gr.DrawLine(Pens.Green, dikePrev, newDike);
            gr.DrawLine(Pens.Blue, waterPrev, newWater);

            // Set new prev points
            dikePrev = newDike;
            waterPrev = newWater;
        }
    }

    /// <summary>
    /// Draw the intersection point of the sea level and dike height
    /// in the graph.
    /// </summary>
    /// <param name="gr"></param>
    public void DrawIntersectionPoint(Graphics gr)
    {
        (double t, double h) = CalcIntersectionPoint();
        int i = (int)(t / (_settings.YearAmountGraph / WIDTH));
        Point drawPoint = this.CalcWaterGraphCoords(i);        
        gr.FillRectangle(Brushes.DarkRed, drawPoint.X - 2, drawPoint.Y - 2, 5, 5);
    }
    
    /// <summary>
    /// Calculate the coordinates of the dike graph at a specific
    /// time.
    /// </summary>
    /// <param name="i">Specific point (x) in the graph</param>
    /// <returns></returns>
    private Point CalcDikeGraphCoords(int i)
    {
        double factor = (HEIGHT / _settings.MaxHeightGraph);
        
        // Calculate coordinates
        int dikeX = i;
        double dikeY = HEIGHT - (_settings.DikeHeight * factor) + (_settings.MinHeightGraph * factor);

        return new Point(dikeX, (int)dikeY);
    }
    
    /// <summary>
    /// Calculate the coordinates the sea level graph at a specific
    /// time.
    /// </summary>
    /// <param name="i">Specific point (x) in the graph</param>
    /// <returns></returns>
    private Point CalcWaterGraphCoords(int i)
    {
        double maxHeight = _settings.MaxHeightGraph;
        double factor = (HEIGHT / maxHeight);
        int year = (int)((_settings.YearAmountGraph / WIDTH) * i);
        int heightFix = (int)(_settings.MinHeightGraph * factor);
        
        // Calculate coordinates
        int waterX = i;
        int waterY = (int)(HEIGHT - CalcSeaLevel(year) * factor) + heightFix;
        
        return new Point(waterX, waterY);
    }
    
    /// <summary>
    /// Draw coordinates in the graph.
    /// </summary>
    private void DrawCoords(Graphics gr)
    {
        double speedPerYear = _settings.RisingSpeed / 100;
        double yearAmount = _settings.YearAmountGraph;
        double factor = (HEIGHT / _settings.MaxHeightGraph);
        double expGrowth = 1 + (_settings.GrowthExponent / 100);
        double yearsFromNow = (_settings.Date - DateTime.Now).Days / 365.0;
        int dikeYStart = (int)(HEIGHT - (_settings.DikeHeight * factor) + (_settings.MinHeightGraph * factor));
        double waterYStart = (int)(HEIGHT - (SEA_LEVEL + (speedPerYear * Math.Pow(yearsFromNow, expGrowth)) * factor) + (_settings.MinHeightGraph * factor));
        double waterYEnd = HEIGHT - (SEA_LEVEL + (speedPerYear * Math.Pow(yearAmount, expGrowth)) * factor) + (_settings.MinHeightGraph * factor);

        // Dike height
        gr.DrawString(
            $"(0, {_settings.DikeHeight})",
            new Font("Bahnschrift", 10),
            Brushes.Green,
            new Point(5, dikeYStart - 20));

        gr.DrawString(
            $"({yearAmount}, {_settings.DikeHeight})",
            new Font("Bahnschrift", 10),
            Brushes.Green,
            new Point((int)WIDTH - 90, dikeYStart - 20));

        // Sea level
        gr.DrawString(
            $"(0, {SEA_LEVEL + (speedPerYear * Math.Pow(yearsFromNow, expGrowth))})",
            new Font("Bahnschrift", 10),
            Brushes.Blue,
            new Point(5, (int)waterYStart - 20));

        gr.DrawString(
            $"({yearAmount}, {SEA_LEVEL + (speedPerYear * Math.Pow(yearAmount, expGrowth))})",
            new Font("Bahnschrift", 10),
            Brushes.Blue,
            new Point((int)WIDTH - 90, (int)waterYEnd - 20));
        
        // Intersection point
        (double t, double h) = CalcIntersectionPoint();
        int i = (int)(t / (_settings.YearAmountGraph / WIDTH));
        Point drawPoint = this.CalcWaterGraphCoords(i);

        gr.DrawString(
            $"({Math.Round(t, 2)}, {Math.Round(h, 2)})",
            new Font("Bahnschrift", 10),
            Brushes.DarkRed,
            new Point(drawPoint.X, drawPoint.Y + 20));
    }

    /// <summary>
    /// Calculate the sea level of a specific year.
    /// </summary>
    /// <param name="year">Amount of years since the start of the simulation.</param>
    /// <returns></returns>
    private double CalcSeaLevel(int year)
    {
        double expGrowth = 1 + (_settings.GrowthExponent / 100);
        return (SEA_LEVEL + ((_settings.RisingSpeed / 100) * Math.Pow(year, expGrowth))); 
    }

    private double NthRoot(double v, double power)
    {
        return Math.Pow(v, 1.0 / power);
    }
    
    /// <summary>
    /// Calculate the intersection point of the water level with the height
    /// of the dike.
    /// </summary>
    /// <returns></returns>
    public (double time, double height) CalcIntersectionPoint()
    {
        double v = (_settings.DikeHeight - SEA_LEVEL) / (_settings.RisingSpeed / 100);
        double t = NthRoot(v, 1 + (_settings.GrowthExponent / 100));
        double h = CalcSeaLevel((int)t);
        return (t, h);
    }
}