














using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDoControls.Controls
{
    
    
    
    public interface IPageControl
    {
        
        
        
        event PageControlEventHandler ShowSourceChanged;
        
        
        
        
        List<object> DataSource { get; set; }
        
        
        
        
        int PageSize { get; set; }
        
        
        
        
        int StartIndex { get; set; }
        
        
        
        void FirstPage();
        
        
        
        void PreviousPage();
        
        
        
        void NextPage();
        
        
        
        void EndPage();
        
        
        
        void Reload();
        
        
        
        
        List<object> GetCurrentSource();
        
        
        
        
        int PageCount { get; set; }
        
        
        
        
        int PageIndex { get; set; }
    }
}
