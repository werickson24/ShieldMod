using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShieldMod.Buffs
{
    public class Protect : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Protected");
            Description.SetDefault("The shield guards you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }
        int timed = 0;
        public override void Update(Player player, ref int buffIndex)
        {
            player.immune = true;
            player.immuneAlpha = 0;
            if (timed++ == 55)
            {
                timed = 0;
                for (int loop = 0; loop < 30; loop++)
                {
                    float num1562 = (float)loop / 30f * 6.28318548f;
                    Vector2 vector268 = player.Center + num1562.ToRotationVector2() * (20);
                    Vector2 vector269 = (num1562 - 1f).ToRotationVector2();// * (1f);
                    Dust dust82 = Dust.NewDustPerfect(vector268, mod.DustType("Ripple"), vector269, 0, Color.White, 1f);
                    dust82.customData = player;
                    dust82.scale = 0.9f;
                    dust82.fadeIn = 1.15f;
                    dust82.noGravity = true;
                    dust82.noLight = true;
                }
            }
        }
    }
}
