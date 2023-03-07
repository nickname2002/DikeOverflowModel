using System.Diagnostics;

namespace DikeOverflowModel;

public class OverflowGraph : Panel
{
    private SettingsView _settings;
    public const double SEA_LEVEL = 1.04;
    public double DRAW_FACTOR;
    private Point? _intersectionPoint;
    
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
            DateTime currentDate = _settings.Date; 
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
        this.DRAW_FACTOR = (HEIGHT / _settings.MaxHeightGraph);

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
    }

    private void DrawGraphLines(Graphics gr)
    {
        double dikeHeight = _settings.DikeHeight;
        double speedPerYear = _settings.RisingSpeed / 100;
        double yearAmount = _settings.YearAmountGraph;
        double minHeight = _settings.MinHeightGraph;
        double maxHeight = _settings.MaxHeightGraph;
        double factor = (HEIGHT / maxHeight);
        double expGrowth = 1 + (_settings.GrowthExponent / 100);
        double yearsFromNow = (_settings.Date - DateTime.Now).Days / 365.0;

        // NOTE: This implementation currently only support linear

        Point dikePrev = new Point();
        Point waterPrev = new Point();

        for (int i = 0; i < WIDTH; i++)
        {
            if (i == 0)
            {
                dikePrev = new Point(i, (int)(HEIGHT - (dikeHeight * factor) + (minHeight * factor) + (minHeight * factor)));
                waterPrev = new Point(i, (int)(HEIGHT - ((SEA_LEVEL + (speedPerYear * (Math.Pow(yearsFromNow + i, expGrowth)) * (yearAmount / WIDTH)))) * factor + (minHeight * factor)));
                continue;
            }

            int dikeX = i;
            double dikeY = HEIGHT - (dikeHeight * factor) + (minHeight * factor);

            int waterX = i;
            double waterY = HEIGHT - ((SEA_LEVEL + (speedPerYear * (Math.Pow(i * (_settings.YearAmountGraph / WIDTH), expGrowth) + Math.Pow(yearsFromNow, expGrowth)) * (yearAmount / WIDTH)))) * factor + (minHeight * factor);

            // Create new points
            Point newWater = new Point(waterX, (int)waterY);
            Point newDike = new Point(dikeX, (int)dikeY);

            // Draw lines
            gr.DrawLine(Pens.Green, dikePrev, newDike);
            gr.DrawLine(Pens.Blue, waterPrev, newWater);

            // Set new prev points
            dikePrev = newDike;
            waterPrev = newWater;
        }
    }

    private Point CalcWaterGraphCoords(int i)
    {
        int year = (int)((_settings.YearAmountGraph / WIDTH) * i);
        int heightFix = (int)(_settings.MinHeightGraph * DRAW_FACTOR);
        int waterX = i;
        int waterY = (int)(HEIGHT - (float)(CalcSeaLevel(year) * DRAW_FACTOR) + heightFix);
        Console.WriteLine(waterY);
        return new Point(waterX, waterY);
    }

    /// <summary>
    /// Draw coordinates in the graph
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

        // Draw coordinates
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
    }

    // NOTE: For some reason, this causes overflow exception
    private double CalcSeaLevel(int year)
    {
        double expGrowth = 1 + (_settings.GrowthExponent / 100);
        return (SEA_LEVEL + (_settings.RisingSpeed * Math.Pow(year, _settings.GrowthExponent))); 
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