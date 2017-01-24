namespace QuasarLight.UI.Infrastructure
{
    public static class StringExtensions
    {
        public static string Pluralize(this string lang)
        {
            switch (lang)
            {
                case "Английский":
                    return "английского";
                case "Французский":
                    return "французского";
                case "Немецкий":
                    return "немецкого";
                case "Итальянский":
                    return "итальянского";
                case "Испанский":
                    return "испанского";
                case "Португальский":
                    return "португальского";
            }
            return null;
        }
    }
}