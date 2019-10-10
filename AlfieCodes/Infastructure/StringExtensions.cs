namespace AlfieCodes.Infastructure
{
    public static class StringExtensions
    {
        public static string Slugify(this string operand)
        {
            return operand.ToLower().Replace( " ", "-" );
            
        }
    }
}
