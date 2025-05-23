using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Fargowiltas.Items.Summons.Mutant
{
    public class DeathBringerFairy : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Death Bringer Fairy");
            /* Tooltip.SetDefault("Summons all pre-hardmode bosses" +
                               "\nCan only be used at night" +
                               "\nCertain bosses will only spawn if you're in their specific biome"); */
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(8, 3));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 0, 2);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<SpawnProj>();
        }

        public override bool CanUseItem(Player player)
        {
            return FargoUtils.ActuallyNight;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 pos = new Vector2((int)player.position.X + Main.rand.Next(-800, 800), (int)player.position.Y + Main.rand.Next(-1000, -250));
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), pos, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0, Main.myPlayer, 1, 2);

            if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.SeveralBossesAwoken"), new Color(175, 75, 255));
            }
            else
            {
                Main.NewText(Language.GetTextValue("Mods.Fargowiltas.MessageInfo.SeveralBossesAwoken"), new Color(175, 75, 255));
            }

            SoundEngine.PlaySound(SoundID.Roar, player.position);

            return false;
        }
    }
}