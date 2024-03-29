﻿using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace DikeOverflowModel;

public class OverflowGraph : Panel, IObservable
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
        this.BackgroundImage = Properties.Resources.grid_backdrop_graph;

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
        gr.SmoothingMode = SmoothingMode.AntiAlias;
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
        int dikeYStart = (int)(HEIGHT - (_settings.DikeHeight * factor) + (_settings.MinHeightGraph * factor));
        double waterYStart = (int)(HEIGHT - (SEA_LEVEL + (speedPerYear * Math.Pow(0, expGrowth)) * factor) + (_settings.MinHeightGraph * factor));
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
            $"(0, {Math.Round(SEA_LEVEL + (speedPerYear * Math.Pow(0, expGrowth)), 2)})",
            new Font("Bahnschrift", 10),
            Brushes.Blue,
            new Point(5, (int)waterYStart - 20));

        gr.DrawString(
            $"({yearAmount}, {Math.Round(SEA_LEVEL + (speedPerYear * Math.Pow(yearAmount, expGrowth)), 2)})",
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
    /// <param name="t">Amount of years since the start of the simulation.</param>
    /// <returns></returns>
    public double CalcSeaLevel(int t)
    {
        double l = SEA_LEVEL;
        
        // the formula for the sea level h given t time
        // h = l + e^(t/tau) * s * t
        return l + RisingSpeed(t) * t;
    }

    /// <summary>
    /// Calculate the rising speed factor for a specific time.
    /// </summary>
    /// <param name="t">Amount of years since the start of the simulation.</param>
    /// <returns></returns>
    public double RisingSpeed(int t)
    {
        double s = _settings.RisingSpeed / 100; // current speed in meters
        double i = _settings.GrowthExponent; // 0.01 = 1% growth per year

        // to calculate tau from i we use the following formula
        // tau = 1 / ln(1 + (i/100))
        // this will find what tau needs to be in order to achieve the growth factor of +i% per year
        double tau = 1 / Math.Log(1 + (i / 100));

        return Math.Pow(Math.E, t / tau) * s;
    }

    /// <summary>
    /// Lambert W function for solving equations in the form of ye^y=x from https://stackoverflow.com/a/60211022
    /// </summary>
    /// <param name="x">x from ye^y=x</param>
    /// <returns>y from ye^y=x</returns>
    public static double LambertW(double x)
    {
        // LambertW is not defined in this section
        if (x < -Math.Exp(-1))
            throw new Exception("The LambertW-function is not defined for " + x + ".");

        // computes the first branch for real values only

        // amount of iterations (empirically found)
        int amountOfIterations = Math.Max(4, (int)Math.Ceiling(Math.Log10(x) / 3));

        // initial guess is based on 0 < ln(a) < 3
        double w = 3 * Math.Log(x + 1) / 4;

        // Halley's method via eqn (5.9) in Corless et al (1996)
        for (int i = 0; i < amountOfIterations; i++)
            w = w - (w * Math.Exp(w) - x) / (Math.Exp(w) * (w + 1) - (w + 2) * (w * Math.Exp(w) - x) / (2 * w + 2));

        return w;
    }

    /// <summary>
    /// Calculate the intersection point of the water level with the height
    /// of the dike.
    /// </summary>
    /// <returns></returns>
    public (double time, double height) CalcIntersectionPoint()
    {

        double v = _settings.DikeHeight - SEA_LEVEL;
        double l = SEA_LEVEL;
        double s = (_settings.RisingSpeed / 100); // RisingSpeed from cm to m
        double i = _settings.GrowthExponent; // percentage of growth per year
        double tau = 1 / Math.Log(1 + (i / 100));

        double t; // time of intersection

        // if tau is infinite (meaning i = 0,0), e^(t/tau) becomes 1, so we can use a simpler formula.
        // if tau is non infinite, we use LambertW to solve the h = l + e^(x/tau)*s*x because it is in the form xe^x
        // so t = tau * Wn( (h-l) / (s*tau) )
        if (double.IsInfinity(tau))
            t = (v - l) / s;
        else
            t = tau * LambertW(v / (s * tau));

        double h = CalcSeaLevel((int)t); // height of intersection
        return (t, h);
    }
}