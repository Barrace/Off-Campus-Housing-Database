<<<<<<< HEAD
﻿#pragma checksum "..\..\PropertyDescription.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B61D50C1B0F32E9AE181772298598066"
=======
﻿#pragma checksum "..\..\PropertyDescription.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B4C6E2FB32AA28B1AADF7A677726FF16"
>>>>>>> origin/master
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace OffCampusHousingDatabase {
    
    
    /// <summary>
    /// PropertyDescription
    /// </summary>
    public partial class PropertyDescription : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AddrTextBlock;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RentTextBlock;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView commentListView;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TypeCommentBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button leaveCommentButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button rightImageButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button leftImageButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AverageRating;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AverageRatingBlock;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\PropertyDescription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Rate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OffCampusHousingDatabase;component/propertydescription.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PropertyDescription.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AddrTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.DesTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.RentTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 16 "..\..\PropertyDescription.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.commentListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.TypeCommentBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.leaveCommentButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.rightImageButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.leftImageButton = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.AverageRating = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.AverageRatingBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.Rate = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

