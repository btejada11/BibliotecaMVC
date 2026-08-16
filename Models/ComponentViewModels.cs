namespace EjercicioPractica_S1.Models
{
    public class FeatureViewModel
    {
        public string IconClass { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string BorderColor { get; set; } // Opcional (ej: #ffc107 o "gold")
    }

    public class BannerViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string BackgroundImage { get; set; } // Opcional
    }
}
