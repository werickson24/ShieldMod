using Terraria.ModLoader;

namespace ShieldMod
{
    class ShieldMod : Mod
    {
        public ShieldMod()
        {
        }
        public static bool Protect = false;
    }
    /*class GodModeModPlayer : ModPlayer
    {
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref Terraria.DataStructures.PlayerDeathReason damageSource)
        {
            if (ShieldMod.Protect)
            {
                return false;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
    }*/
}