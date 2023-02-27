namespace DikeOverflowModel;

public class OverflowGraph : Control
{
    //Component data
    private const int WIDTH = 425;
    private const int HEIGHT = 225;

    //UI components 
    private Label _graphBorder;
    private Label _yParameter;
    private Label _xParameter;

    public OverflowGraph()
    {
        this.ClientSize = new Size(WIDTH, HEIGHT);
        this.Location = new Point(80, 470);
        this.BackColor = Color.White;  

        //Add all Controls
        this.Controls.Add(_graphBorder);
    }
}