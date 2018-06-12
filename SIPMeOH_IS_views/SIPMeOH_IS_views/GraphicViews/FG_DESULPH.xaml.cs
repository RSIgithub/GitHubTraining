using System; 
using System.Collections.Generic; 
using System.Windows.Controls; 
using System.ComponentModel.Composition; 
namespace SIPMeOH_IS_views 
{ 
    [ExportSynoptic("FG_DESULPH")] 
    [PartCreationPolicy(CreationPolicy.NonShared)] 
    public partial class FG_DESULPH
    { 
        public FG_DESULPH() 
        { 
            this.InitializeComponent(); 
        } 
    } 
} 
