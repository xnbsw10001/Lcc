﻿namespace LccModel
{
    public abstract class APanelView<T> : AViewBase<T> where T : ViewModelBase
    {
        public virtual void ClosePanel()
        {
            PanelType type = GetType().Name.ToPanelType();
            PanelManager.Instance.ClosePanelAsync(type).Continue();
        }
        public virtual void ClearPanel()
        {
            PanelType type = GetType().Name.ToPanelType();
            PanelManager.Instance.ClearPanel(type);
        }
    }
}