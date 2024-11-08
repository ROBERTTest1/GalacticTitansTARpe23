namespace GalacticTitans.Models.Titans
{
    public class TitanDetailsViewModel
    {
        public Guid? ID { get; set; }
        public string TitanName { get; set; }
        public int TitanHealth { get; set; }
        public int TitanXP { get; set; }
        public int TitanXPNextLevel { get; set; }
        public int TitanLevel { get; set; }
        public TitanType TitanType { get; set; }
        public TitanStatus TitanStatus { get; set; }
        public int PrimaryAttackPower { get; set; }
        public string PrimaryAttackName { get; set; }
        public int SecondaryAttackPower { get; set; }
        public string SecondaryAttackName { get; set; }
        public int SpecialAttackPower { get; set; }
        public string SpecialAttackName { get; set; }
        //public List<IFormFile> Files { get; set; }
        public List<TitanImageViewModel> Image { get; set; } = new List<TitanImageViewModel>();
    }
}
