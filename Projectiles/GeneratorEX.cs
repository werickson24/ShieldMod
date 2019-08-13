using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShieldMod.Projectiles
{
    public class GeneratorEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        private int Charge = 0;
        private bool Charged = false;
        private bool End = false;
        private int Updatetimer = 0;
        private int sounddelay = 0;
        private int dustIntensity = 0;
        private int level = 2;
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 28;
            projectile.friendly = false;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Charge++;//Efficiency :D
            Updatetimer++;
            bool Update = false;
            if (Updatetimer >= 10)
            {
                Update = true;//update the sprites roation but not so frequently that it looks weird
                Updatetimer = 0;
                sounddelay += 1;
                player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
            }
            if (sounddelay >= 2)
            {
                sounddelay = 0;
                //add to level and start incrementing intensity after two increments of level
                if (level++ > 3)
                {
                    dustIntensity++;
                }
                Main.PlaySound(SoundID.Item15, player.Center);
            }
            if (Charge >= 180)
            {
                Charged = true;//Fully charged after 180 frames or 3 seconds (3 * 60FPS)
                End = true;
            }
            else
            {
                Charged = false;
            }
            if (!player.CheckMana(player.inventory[player.selectedItem].mana, false, false) || !player.channel || player.noItems || player.CCed)
            {
                End = true;
            }
            if (End)
            {
                if (Charged)
                {
                    Main.PlaySound(SoundID.Item42, player.Center);//pew
                    Vector2 center = projectile.Center;
                    Vector2 target = Main.MouseWorld;
                    float shootToX = target.X - projectile.Center.X;
                    float shootToY = target.Y - projectile.Center.Y;
                    float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);//Code that does something important
                    distance = 1f / distance;
                    shootToX *= distance * 5;
                    shootToY *= distance * 5;
                    Projectile.NewProjectile(center.X, center.Y, shootToX, shootToY, mod.ProjectileType("ShieldCondenseEX"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                    projectile.Kill();
                    projectile.netUpdate = true;//I guess this updates multiplayer or something?
                }
                else
                {
                    Main.PlaySound(SoundID.Item14, player.Center);
                    float scaleFactor4 = 20f;
                    Vector2 vector14 = Vector2.Normalize(projectile.velocity) * scaleFactor4;
                    if (float.IsNaN(vector14.X) || float.IsNaN(vector14.Y))
                    {
                        vector14 = -Vector2.UnitY;
                    }
                    Projectile.NewProjectile(projectile.Center.X - 40, projectile.Center.Y - 40, vector14.X, vector14.Y, mod.ProjectileType("ShieldCondense"), projectile.damage, projectile.knockBack, Main.myPlayer, level, 1f);//these two values at the end here are very important to the new projectile
                    projectile.Kill();
                    projectile.netUpdate = true;
                }
            }
            //Projectile velocity angle setter
            float scaleFactor3 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
            Vector2 vector9 = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 value5 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector9;
            if (player.gravDir == -1f)
            {
                value5.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector9.Y;
            }
            Vector2 vector10 = Vector2.Normalize(value5);
            if (float.IsNaN(vector10.X) || float.IsNaN(vector10.Y))
            {
                vector10 = -Vector2.UnitY;
            }
            vector10 *= scaleFactor3;
            if (vector10.X != projectile.velocity.X || vector10.Y != projectile.velocity.Y)
            {
                projectile.netUpdate = true;
            }
            if (Update)
            {
                projectile.velocity = vector10;
            }
            //little charging dusts
            Vector2 spinningpoint3 = Vector2.UnitX * 19f;
            spinningpoint3 = spinningpoint3.RotatedBy(projectile.rotation - 1.57079637f, default(Vector2));
            Vector2 value6 = projectile.Center + spinningpoint3;
            for (int k = 0; k < dustIntensity + 1; k++)
            {
                float num19 = 0.4f;
                if (k % 2 == 1)
                {
                    num19 = 0.65f;
                }
                Vector2 vector11 = value6 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - dustIntensity * 2);
                int num20 = Dust.NewDust(vector11 - Vector2.One * 8f, 16, 16, 226, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
                Main.dust[num20].velocity = Vector2.Normalize(value6 - vector11) * 1.5f * (10f - dustIntensity * 2f) / 10f;
                Main.dust[num20].noGravity = true;
                Main.dust[num20].scale = num19;
                Main.dust[num20].customData = player;
            }
            //Sprite velocity to roation and player orientation stuff
            projectile.position = player.RotatedRelativePoint(player.MountedCenter) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            Vector2 vector33 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector33.X = player.bodyFrame.Width - vector33.X;
            }
            if (player.gravDir != 1f)
            {
                vector33.Y = player.bodyFrame.Height - vector33.Y;
            }
            vector33 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            projectile.Center = player.RotatedRelativePoint(player.position + vector33, true) - projectile.velocity;
        }
    }
}
