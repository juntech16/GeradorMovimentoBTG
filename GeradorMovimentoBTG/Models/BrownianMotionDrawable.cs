using Microsoft.Maui.Graphics;

namespace GeradorMovimentoBTG.Models
{
    public class BrownianMotionDrawable : IDrawable
    {
        private double[][] _simulationData;
        private List<Color> _lineColors;
        private float _padding = 40;
        private bool _showGrid = true;

        public BrownianMotionDrawable()
        {
            _simulationData = new double[0][];
            _lineColors = new List<Color>();
        }

        public void UpdateData(double[][] data, List<Color> colors)
        {
            _simulationData = data;
            _lineColors = colors;
        }

        public bool ShowGrid
        {
            get => _showGrid;
            set => _showGrid = value;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (_simulationData == null || _simulationData.Length == 0 || _simulationData[0].Length == 0)
                return;

            float width = dirtyRect.Width;
            float height = dirtyRect.Height;
            float graphWidth = width - 2 * _padding;
            float graphHeight = height - 2 * _padding;


            double maxValue = double.MinValue;
            double minValue = double.MaxValue;

            foreach (var simulation in _simulationData)
            {
                foreach (var price in simulation)
                {
                    if (price > maxValue) maxValue = price;
                    if (price < minValue) minValue = price;
                }
            }


            double range = maxValue - minValue;
            maxValue += range * 0.1;
            minValue -= range * 0.1;


            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(0, 0, width, height);

            canvas.StrokeColor = Colors.DarkGray;
            canvas.StrokeSize = 1;
            canvas.DrawRectangle(_padding, _padding, graphWidth, graphHeight);


            if (_showGrid)
            {
                DrawGrid(canvas, _padding, _padding, graphWidth, graphHeight, 5, 5, Colors.DarkGray);
            }


            DrawAxes(canvas, _padding, _padding, graphWidth, graphHeight, minValue, maxValue, _simulationData[0].Length);


            for (int simIndex = 0; simIndex < _simulationData.Length; simIndex++)
            {
                if (simIndex < _lineColors.Count)
                {
                    DrawSimulation(canvas, _simulationData[simIndex], _padding, _padding, 
                        graphWidth, graphHeight, minValue, maxValue, _lineColors[simIndex]);
                }
            }
        }

        private void DrawGrid(ICanvas canvas, float x, float y, float width, float height, 
            int horizontalLines, int verticalLines, Color gridColor)
        {
            canvas.StrokeColor = gridColor;
            canvas.StrokeSize = 0.5f;


            float yStep = height / horizontalLines;
            for (int i = 1; i < horizontalLines; i++)
            {
                float yPos = y + i * yStep;
                canvas.DrawLine(x, yPos, x + width, yPos);
            }


            float xStep = width / verticalLines;
            for (int i = 1; i < verticalLines; i++)
            {
                float xPos = x + i * xStep;
                canvas.DrawLine(xPos, y, xPos, y + height);
            }
        }

        private void DrawAxes(ICanvas canvas, float x, float y, float width, float height, 
            double minValue, double maxValue, int dataLength)
        {
            canvas.FontColor = Colors.White;
            canvas.FontSize = 10;


            for (int i = 0; i <= 5; i++)
            {
                float yPos = y + height - (height * i / 5);
                double value = minValue + ((maxValue - minValue) * i / 5);
                

                canvas.DrawLine(x - 5, yPos, x, yPos);
                

                canvas.DrawString($"{value:F2}", x - 35, yPos - 5, HorizontalAlignment.Right);
            }


            for (int i = 0; i <= 5; i++)
            {
                float xPos = x + (width * i / 5);
                int day = (dataLength - 1) * i / 5;
                

                canvas.DrawLine(xPos, y + height, xPos, y + height + 5);
                

                canvas.DrawString($"{day}", xPos - 10, y + height + 10, HorizontalAlignment.Center);
            }


            canvas.DrawString("Preço", x - 35, y - 15, HorizontalAlignment.Left);
            

            canvas.DrawString("Tempo (dias)", x + width / 2, y + height + 25, HorizontalAlignment.Center);
        }

        private void DrawSimulation(ICanvas canvas, double[] data, float x, float y, 
            float width, float height, double minValue, double maxValue, Color lineColor)
        {
            if (data.Length < 2)
                return;

            canvas.StrokeColor = lineColor;
            canvas.StrokeSize = 2;

            PathF path = new PathF();


            float x1 = x;
            float y1 = y + height - (float)((data[0] - minValue) / (maxValue - minValue) * height);
            path.MoveTo(x1, y1);


            float xStep = width / (data.Length - 1);
            for (int i = 1; i < data.Length; i++)
            {
                float x2 = x + i * xStep;
                float y2 = y + height - (float)((data[i] - minValue) / (maxValue - minValue) * height);
                path.LineTo(x2, y2);
            }

            canvas.DrawPath(path);
        }
    }
}
