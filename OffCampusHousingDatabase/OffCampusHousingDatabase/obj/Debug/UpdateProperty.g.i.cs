﻿#pragma checksum "..\..\UpdateProperty.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7914E27342DAB19A0734A2BA018A40B0"
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
    /// UpdateProperty
    /// </summary>
    public partial class UpdateProperty : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addPhoto;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button removePhoto;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addressTextbox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptionTextbox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox monthlyRentTextbox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox roomsAvailableTextbox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button submit;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock statusLabel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextImageButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\UpdateProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button prevImageButton;
        
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
            System.Uri resourceLocater = new System.Uri("/OffCampusHousingDatabase;component/updateproperty.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UpdateProperty.xaml"
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
            this.addPhoto = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\UpdateProperty.xaml"
            this.addPhoto.Click += new System.Windows.RoutedEventHandler(this.addPhoto_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.removePhoto = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\UpdateProperty.xaml"
            this.removePhoto.Click += new System.Windows.RoutedEventHandler(this.removePhoto_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.addressTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.descriptionTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.monthlyRentTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.roomsAvailableTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.submit = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\UpdateProperty.xaml"
            this.submit.Click += new System.Windows.RoutedEventHandler(this.submit_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.statusLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            
            #line 21 "..\..\UpdateProperty.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.goBack_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ImageBox = ((System.Windows.Controls.Image)(target));
            
            #line 22 "..\..\UpdateProperty.xaml"
            this.ImageBox.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ImageBox_MouseDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.nextImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\UpdateProperty.xaml"
            this.nextImageButton.Click += new System.Windows.RoutedEventHandler(this.nextImageButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.prevImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\UpdateProperty.xaml"
            this.prevImageButton.Click += new System.Windows.RoutedEventHandler(this.prevImageButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

