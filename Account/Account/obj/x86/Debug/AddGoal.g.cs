﻿#pragma checksum "C:\Users\Shower\Documents\GitHub\UWP-Project\Account\Account\AddGoal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DCE5DF0F34D9BD282063D7C58A502759"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Account
{
    partial class AddGoal : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.image = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 2:
                {
                    this.name = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3:
                {
                    this.price = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.description = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.dueTime = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 6:
                {
                    this.createBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 29 "..\..\..\AddGoal.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.createBtn).Click += this.createBtn_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.updateBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\AddGoal.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.updateBtn).Click += this.updateBtn_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.cancelBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 31 "..\..\..\AddGoal.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.cancelBtn).Click += this.cancelBtn_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.selectBtn = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 22 "..\..\..\AddGoal.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.selectBtn).Click += this.selectBtn_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

