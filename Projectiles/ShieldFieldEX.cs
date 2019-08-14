using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ShieldMod.Projectiles
{
    public class ShieldFieldEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force Field");
        }
        double angle = 0;
        double tangle = 0;
        List<Projectile> killlist = new List<Projectile>();
        public override void SetDefaults()
        {
            projectile.width = 350;
            projectile.height = 350;
            projectile.damage = 35;
            projectile.knockBack = 60;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.timeLeft = 10800;
            projectile.extraUpdates = 2;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 40;
            projectile.ignoreWater = false;
            projectile.tileCollide = false;
            projectile.hide = true;
        }
        public override void AI()
        {
            //int[] blacklist = new int[]{,};
            //Projectile_Collision_Targeter
            for (int s = 0; s < 1000; s++)
            {
                if (Main.projectile[s].active && Main.projectile[s].hostile)/* && !Array.Exists(blacklist, element => element == Main.projectile[s].type))*/
                {
                    Projectile ProJ = Main.projectile[s];
                    if (Colliding(projectile.Hitbox, ProJ.Hitbox) == true)
                    {
                        if (killlist.Contains(ProJ))
                        {
                            killlist.Remove(ProJ);
                            ProJ.Kill();
                        }
                        else
                        {
                            for (int num641 = 0; num641 < 80; num641++)
                            {
                                int numdust = Dust.NewDust(new Vector2(ProJ.position.X, ProJ.position.Y), 6, 6, 226, 0f, 0f, 100, default(Color), 1f);
                                Dust projdust = Main.dust[numdust];
                                float num646 = projdust.velocity.X;
                                float y3 = projdust.velocity.Y;
                                if (num646 == 0f && y3 == 0f)
                                {
                                    num646 = 1f;
                                }
                                float num647 = (float)Math.Sqrt(num646 * num646 + y3 * y3);
                                num647 = 4f / num647;
                                num646 *= num647;
                                y3 *= num647;
                                projdust.velocity *= 0.5f;
                                projdust.velocity.X += num646;
                                projdust.velocity.Y += y3;
                                projdust.scale = 1.3f;
                                projdust.noGravity = true;
                            }
                            ProJ.velocity = new Vector2(-ProJ.velocity.X, -ProJ.velocity.Y);
                            killlist.Add(ProJ);
                        }
                    }
                }
            }
            foreach (Player pal in Main.player)
            {
                if (pal.active)
                {
                    if ((Main.player[projectile.owner].team == pal.team || pal == Main.player[projectile.owner]) && Colliding(projectile.Hitbox, pal.Hitbox) == true)
                    {
                        pal.AddBuff(mod.BuffType("Protect"), 2, false);
                    }
                }
            }
            if (Main.rand.Next(80) == 0)
            {
                Vector2 ripple = new Vector2(Main.rand.Next(-175, 175) + projectile.Center.X, Main.rand.Next(-175, 175) + projectile.Center.Y);
                if (165 > Vector2.Distance(projectile.Center, ripple) && PosHit(ripple))
                {
                    for (int num42 = 0; num42 < 30; num42++)
                    {
                        int dust = Dust.NewDust(ripple, 6, 6, 255, 0f, 0f, 100, default(Color), 1f);
                        Dust spark = Main.dust[dust];
                        float num646 = spark.velocity.X;
                        float y3 = spark.velocity.Y;
                        if (num646 == 0f && y3 == 0f)
                        {
                            num646 = 1f;
                        }
                        float num647 = (float)Math.Sqrt(num646 * num646 + y3 * y3);
                        num647 = 1 / num647;
                        num646 *= num647;
                        y3 *= num647;
                        spark.velocity *= 0.5f;
                        spark.velocity.X += num646;
                        spark.velocity.Y += y3;
                        spark.scale = 1.3f;
                        spark.noGravity = true;
                    }
                }
            }
            //Edge_Dusts
            angle += 90.5;
            tangle = angle * (Math.PI / 180);
            double x = (Math.Cos(tangle) * 170) + +projectile.Center.X;
            double y = (Math.Sin(tangle) * 170) + +projectile.Center.Y;
            Vector2 dustSp = new Vector2((float)x, (float)y);
            if (Math.Abs(dustSp.X) >= 0.12f)
            {
                if (PosHit(dustSp))
                {
                    if ((projectile.timeLeft + 10) > Main.rand.Next(1, 1000))
                    {
                        Dust dust;
                        dust = Dust.NewDustPerfect(dustSp, 226, new Vector2(0f, 0f), 30, new Color(255, 255, 255), 1.2f);
                        dust.rotation = 50;
                        dust.noGravity = (projectile.timeLeft + 10) > Main.rand.Next(1, 400);
                        dust.fadeIn = 1.15f;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            //Hit_Dusts
            for (int num642 = 0; num642 < 30; num642++)
            {
                float num643 = (target.velocity.X + target.velocity.Y) / 2;
                if (num643 > 10f)
                {
                    num643 = 10;
                }
                else if (num643 < 1.5f)
                {
                    num643 = 1.5f;
                }
                int hitnumdust = Dust.NewDust(new Vector2(target.position.X, target.position.Y), 6, 6, 134, 0f, 0f, 100, default(Color), 1f);
                Dust hitdust = Main.dust[hitnumdust];
                float num646 = hitdust.velocity.X;
                float y3 = hitdust.velocity.Y;
                if (num646 == 0f && y3 == 0f)
                {
                    num646 = 1f;
                }
                float num647 = (float)Math.Sqrt(num646 * num646 + y3 * y3);
                num647 = num643 / num647;
                num646 *= num647;
                y3 *= num647;
                hitdust.velocity *= 0.5f;
                hitdust.velocity.X += num646;
                hitdust.velocity.Y += y3;
                hitdust.scale = 1.3f;
                hitdust.noGravity = true;
            }

        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            bool flag;
            Vector2 clamped = Vector2.Clamp(projectile.Center, targetHitbox.Left(), targetHitbox.Right());
            if (PosHit(clamped))
            {
                flag = Math.Pow(175, 2) > Vector2.DistanceSquared(projectile.Center, clamped);
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        public bool PosHit(Vector2 targetPosition)
        {
            if (WorldGen.SolidTile((int)targetPosition.X / 16, (int)targetPosition.Y / 16))
            {
                return false;
            }
            Vector2 vector = Vector2.Clamp(targetPosition, new Vector2(projectile.Center.X - 16, projectile.Center.Y - 16), new Vector2(projectile.Center.X + 16, projectile.Center.Y + 16));
            bool flag = Collision.CanHitLine(vector, 0, 0, targetPosition, 0, 0);
            if (!flag)
            {
                Vector2 v = targetPosition - vector;
                Vector2 spinningpoint = v.SafeNormalize(Vector2.UnitY);
                Vector2 value = Vector2.Lerp(vector, targetPosition, 0.5f);
                Vector2 vector2 = value + spinningpoint.RotatedBy(1.5707963705062866, default(Vector2)) * v.Length() * 0.15f;
                if (Collision.CanHitLine(vector, 0, 0, vector2, 0, 0) && Collision.CanHitLine(vector2, 0, 0, targetPosition, 0, 0))
                {
                    flag = true;
                }
                if (!flag)
                {
                    Vector2 vector3 = value + spinningpoint.RotatedBy(-1.5707963705062866, default(Vector2)) * v.Length() * 0.15f;
                    if (Collision.CanHitLine(vector, 0, 0, vector3, 0, 0) && Collision.CanHitLine(vector3, 0, 0, targetPosition, 0, 0))
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 60;
            double DirChk = target.Center.X - projectile.Center.X;
            if (DirChk > 0)
            {
                hitDirection = 1;
            }
            else
            {
                hitDirection = -1;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == 46 || target.type == 303 || target.type == 337 || target.type == 540 || target.type == 443)
            {
                return false;
            }
            return null;
        }
    }
}