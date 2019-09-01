using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ShieldMod.Dusts
{
    public class Ripple : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 0;
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 10, 10, 10);
        }
        public override bool Update(Dust dust)
        {
            dust.scale -= 0.01f;
            if (dust.scale <= 0.2f)
            {
                dust.active = false;
            }
            dust.velocity *= 0.96f;
            if (dust.customData != null && dust.customData is Player player)
            {
                dust.position += player.velocity;
            }
            dust.position += dust.velocity;
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), dust.scale * 0.2f, dust.scale * 0.7f, dust.scale * 1f);
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            lightColor = Color.Lerp(lightColor, Color.White, 0.8f);
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}