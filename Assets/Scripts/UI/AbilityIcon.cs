using Abilities.AbilitySO;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityIcon : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image art;
        [SerializeField] private Text text;
        [SerializeField] private IconAndText iconAndText;

        [SerializeField] private Sprite coinIcon;
        [SerializeField] private Sprite manaIcon;
        
        public void SetArt(AbilitySo ability)
        {
            background.sprite = ability.stats.art;
            art.sprite = ability.stats.art;
            SetCooldownFillAmount(ability);
            SetTextAndIcon(ability);
        }

        public void Refresh(AbilitySo ability)
        {
            if(ability.IsOnCooldown)
             SetCooldownFillAmount(ability);
        }

        private void SetCooldownFillAmount(AbilitySo ability)
        {
            art.fillAmount = ability.CooldownTimeLeft / ability.stats.baseCooldown;
        }

        private void SetTextAndIcon(AbilitySo ability)
        {

            if (ability is SpellAbility)
            {
                iconAndText.SetIcon(manaIcon);
                iconAndText.SetText(ability.stats.manaCost.ToString());
            }
            else
            {
                iconAndText.SetIcon(coinIcon);
                iconAndText.SetText(ability.stats.goldCost.ToString());
            }
        }
    }
}
