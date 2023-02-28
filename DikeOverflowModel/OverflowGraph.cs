namespace DikeOverflowModel;

public class OverflowGraph : Panel
{
    //Component data
    private const int WIDTH = 540;
    private const int HEIGHT = 300;

    //UI components 
    private Label _graphBorder;
    private Label _yParameter;
    private Label _xParameter;

    public OverflowGraph()
    {
        this.ClientSize = new Size(WIDTH, HEIGHT);
        this.Location = new Point(20, 460);
        this.BackColor = Color.White;  

        //Add all Controls
        this.Controls.Add(_graphBorder);
    }
}