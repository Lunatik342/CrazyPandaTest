namespace RedPanda.Project.UI.Misc
{
    public class StringFormatter
    {
        public static string ToCostString(int cost)
        {
            return $"x{cost.ToString()}";
        }
    }
}