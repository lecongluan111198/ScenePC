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
        string title;
        List<double> anchorPos;
        List<double> pivotPos;

        public ToolTipDetail()
        {
        }

        public ToolTipDetail(string title)
        {
            this.Title = title;
            anchorPos = new List<double>() { 0, 0, 0 };
            pivotPos = new List<double>() { 0, 0, 0 };
        }

        public ToolTipDetail(string title, List<double> anchorPos, List<double> pivotPos)
        {
            this.Title = title;
            this.anchorPos = anchorPos;
            this.pivotPos = pivotPos;
        }

        public string Title { get => title; set => title = value; }
        public List<double> AnchorPos { get => anchorPos; set => anchorPos = value; }
        public List<double> PivotPos { get => pivotPos; set => pivotPos = value; }

        public override bool Equals(object obj)
        {
            var detail = obj as ToolTipDetail;
            return detail != null &&
                   title == detail.title &&
                   EqualityComparer<List<double>>.Default.Equals(anchorPos, detail.anchorPos) &&
                   EqualityComparer<List<double>>.Default.Equals(pivotPos, detail.pivotPos);
        }

        public override int GetHashCode()
        {
            var hashCode = 1911401108;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(anchorPos);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(pivotPos);
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
