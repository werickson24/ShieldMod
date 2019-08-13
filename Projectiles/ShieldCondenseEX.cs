using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShieldMod.Projectiles
{
    public class ShieldCondenseEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma");
        }
        public override void SetDefaults()
        {
            projectile.hide = true;
            projectile.width = 6;
            projectile.height = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item79, (int)projectile.position.X, (int)projectile.position.Y);
            for (int num642 = 0; num642 < 100; num642++)
            {
                int num645 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 6, 6, 226, 0f, 0f, 100, default(Color), 1f);
                Dust killdust = Main.dust[num645];
                float num646 = killdust.velocity.X;
                float y3 = killdust.velocity.Y;
                if (num646 == 0f && y3 == 0f)
                {
                    num646 = 1f;
                }
                float num647 = (float)Math.Sqrt(num646 * num646 + y3 * y3);
                num647 = 4f / num647;
                num646 *= num647;
                y3 *= num647;
                killdust.velocity *= 0.5f;
                killdust.velocity.X += num646;
                killdust.velocity.Y += y3;
                killdust.scale = 1.3f;
                killdust.noGravity = true;
            }
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("ShieldFieldEX"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);

        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.alpha = 128;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Dust dust = Dust.NewDustPerfect(projectile.Center, 226, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
            dust.noGravity = true;
            dust.fadeIn = 1.2f;

        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            double dir = target.Center.X - projectile.Center.X;
            if (dir > 0)
            {
                hitDirection = 1;
            }
            else
            {
                hitDirection = -1;
            }
        }
    }
}
