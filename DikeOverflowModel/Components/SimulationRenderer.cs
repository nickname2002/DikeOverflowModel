using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DikeOverflowModel
{
    public class SimulationRenderer : Panel, IObservable
    {
        private const int SIMULATION_WIDTH = 1000;
        private const int SIMULATION_HEIGHT = 592;

        private float _scale;
        private double _dikeHeight;
        private double _waterHeight;

        public SimulationRenderer()
        {
            this.Size = new Size(SIMULATION_WIDTH, SIMULATION_HEIGHT);
            this._scale = 100f;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            this.BackgroundImage = Properties.Resources.grid_backdrop;

            // Event handlers
            this.Paint += PaintEvent;
            this.MouseWheel += ScrollEvent;
        }

        /// <summary>
        /// Update the render with updated settings.
        /// </summary>
        /// <param name="s">Object containing render settings.</param>
        public void Update(SettingsView s)
        {
            this._dikeHeight = s.DikeHeight;
            this._waterHeight = s.SeaLevel; // TODO: use dynamic property
            this.Invalidate();
        }

        private void PaintEvent(object sender, PaintEventArgs ea)
        {
            Graphics g = ea.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.DrawDike(g);
            this.DrawWater(g);
            this.DrawInfo(g);
        }

        private void ScrollEvent(object sender, MouseEventArgs ea)
        {
            if (ea.Delta < 0 && this._scale > 0)
            {
                this._scale -= 5;
            }
            else if (this._scale < 300)
            {
                this._scale += 5;
            }

            this.Invalidate();
        }

        private void DrawDike(Graphics gr)
        {
            double dikeHeight = _dikeHeight * _scale;
            double dikeWidth = 1 * _scale;
            gr.FillRectangle(Brushes.SlateGray, 700, (int)(this.Height - dikeHeight), (int)dikeWidth, (int)dikeHeight);
        }

        private void DrawWater(Graphics gr)
        {
            double waterHeight = this._waterHeight * _scale;
            double waterWidth = 700;
            gr.FillRectangle(Brushes.DodgerBlue, 0, (int)(this.Height - waterHeight), (int)waterWidth, (int)waterHeight);
        }

        private void DrawInfo(Graphics g)
        {
            // Water height
            g.DrawString(
                $"{Math.Round(this._waterHeight, 2)}m", 
                new Font("Bahnschrift", 12), 
                Brushes.DarkBlue, 
                new Point(350, (int)(this.Height - _waterHeight * _scale + 20)));

            // Dike height
            g.DrawString(
                $"{Math.Round(this._dikeHeight, 2)}m", 
                new Font("Bahnschrift", 12), 
                Brushes.Black, 
                new Point((int)(700 + _scale / 3), (int)(this.Height - _dikeHeight * _scale) + 20));
        }
    }
}
