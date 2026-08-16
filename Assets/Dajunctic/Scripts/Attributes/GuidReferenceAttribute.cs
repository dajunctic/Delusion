using System;
using UnityEngine;

namespace Dajunctic
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class GuidReferenceAttribute: PropertyAttribute
    {
        public Type TargetType {get; }
        public bool ShowPingButton {get; }

        public GuidReferenceAttribute(Type targetType = null, bool showPingButton = true)
        {
            TargetType = targetType ?? typeof(BaseConfig);
            ShowPingButton = showPingButton;
        }
    }
}