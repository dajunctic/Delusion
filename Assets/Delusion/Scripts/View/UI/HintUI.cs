using TMPro;
using UnityEngine;

namespace Dajunctic
{
    public class HintUI: BaseMono
    {
        [SerializeField] GameObject toggleUI;
        [SerializeField] TMP_Text decs;

        bool canShow;
        string hintDecs;

        public override void DoEnable()
        {
            base.DoEnable();
            toggleUI.SetActive(false);
        }

        public override void ListenEvents()
        {
            base.ListenEvents();
            this.RegisterListener<ShowHintUI>(OnToggleHintUI);
        }

        public override void StopListenEvents()
        {
            base.StopListenEvents();
            this.RemoveListener<ShowHintUI>(OnToggleHintUI);
        }

        public override void Tick()
        {
            base.Tick();

            toggleUI.SetActive(canShow);
            decs.text = hintDecs;
        }

        void OnToggleHintUI(ShowHintUI @event)
        {
            canShow = @event.Value;
            hintDecs = @event.Decs;
        }
    }

    public struct ShowHintUI: IEvent
    {
        public bool Value;
        public string Decs;
        public ShowHintUI(bool value, string decs)
        {
            Value = value;
            Decs = decs;
        }
    }
}