using System.Windows.Forms;

public static class DataGridViewExtensions
{
    public static void EnableDoubleBuffering(this DataGridView dgv)
    {
        typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null, dgv, new object[] { true });
    }
}

