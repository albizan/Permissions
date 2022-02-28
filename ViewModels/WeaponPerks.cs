namespace Permissions.ViewModels
{
    public class WeaponPerks
    {
        public int WeaponId { get; set; }
        public List<PerkIsSelectedVM> Perks { get; set; }
    }

    public class PerkIsSelectedVM
    {
        public int PerkId { get; set; }
        public string PerkName { get; set; }
        public bool PerkIsSelected { get; set; }
    }
}
