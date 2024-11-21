namespace GalacticTitans.Models.AstralBodies
{
    public class AstralBodyImageViewModel
    {
        public Guid ImageID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? AstralBodyID { get; set; }
    }
}
