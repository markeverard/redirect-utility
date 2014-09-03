namespace MarieCurie.RedirectUtility
{
    public class Redirect
    {
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }

        public bool IsEmpty { get { return string.IsNullOrEmpty(OldUrl) || string.IsNullOrEmpty(NewUrl); } }
    }
}
