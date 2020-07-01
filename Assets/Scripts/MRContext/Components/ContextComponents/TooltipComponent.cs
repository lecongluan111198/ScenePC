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
        List<double> position;

        public ToolTipDetail(string name, string title, List<double> position)
        {
            this.Name = name;
            this.Title = title;
            this.Position = position;
        }

        public string Name { get => name; set => name = value; }
        public string Title { get => title; set => title = value; }
        public List<double> Position { get => position; set => position = value; }

        public override bool Equals(object obj)
        {
            var detail = obj as ToolTipDetail;
            return detail != null &&
                   name == detail.name &&
                   title == detail.title &&
                   EqualityComparer<List<double>>.Default.Equals(position, detail.position);
        }

        public override int GetHashCode()
        {
            var hashCode = 510941377;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(position);
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
