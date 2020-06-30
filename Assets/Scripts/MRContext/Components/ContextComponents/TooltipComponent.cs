using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TooltipComponent : AbstractComponent
{
    [Serializable]
    public class ToolTipDetail
    {
        string name;
        string title;

        public ToolTipDetail(string name, string title)
        {
            this.Name = name;
            this.Title = title;
        }

        public string Name { get => name; set => name = value; }
        public string Title { get => title; set => title = value; }

        public override bool Equals(object obj)
        {
            var detail = obj as ToolTipDetail;
            return detail != null &&
                   Name == detail.Name &&
                   Title == detail.Title;
        }

        public override int GetHashCode()
        {
            var hashCode = -1374651169;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            return hashCode;
        }
    }

    private HashSet<ToolTipDetail> tooltips = new HashSet<ToolTipDetail>();

    public HashSet<ToolTipDetail> Tooltips { get => tooltips; set => tooltips = value; }

    public TooltipComponent(string name, HashSet<ToolTipDetail> tooltips) : base((int) EComponent.TOOLTIP, name)
    {
        this.Tooltips = tooltips;
    }

    public override Type getType()
    {
        return typeof(MRTooltip);
    }

    public override void updateInfomation(Component component)
    {
        MRTooltip tooltip = component as MRTooltip;
        tooltip.UpdateTooltip(Tooltips);
    }
}
